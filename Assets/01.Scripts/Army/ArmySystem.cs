using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define.Algorithem;
using UnityEngine.Rendering;

[System.Serializable]
public struct ArmyInfo //UI부분, 기획이 더 필요
{
    //인원수
    public int totalCount;
    public int basicCount;
    public int archerCount;
    public int shieldCount;

    //스탯이 몇배정도 증가하였는가
    public float basicTimes;
    public float archerTimes;
    public float shieldTimes;
}

[System.Serializable]
public class Army
{
    public int Legion;
    public bool IsMoving;
    public List<Penguin> Soldiers = new();
    public Penguin General; //장군

    public ArmyInfo info;
}

[System.Serializable]
public class SoldierType
{
    public PenguinTypeEnum type;
    public Penguin obj;
}
public class ArmySystem : Singleton<ArmySystem>
{

    [SerializeField] private GameObject _crown;
    [SerializeField] private InputReader _inputReader;
    public ParticleSystem ClickParticle;

    [SerializeField] private List<SoldierType> soldierTypes = new();
    private Dictionary<PenguinTypeEnum, Penguin> soldierTypeDictionary = new();

    [SerializeField] private List<Army> armies = new();
    public List<Army> Armies { get { return armies; } }

    private int curLegion = 0;
    public int CurLegion => curLegion;
    public int ArmyCount => armies.Count;

    public override void Awake()
    {
        ClickParticle = GameObject.Find("ClickParticle").GetComponent<ParticleSystem>();
        _inputReader.ClickEvent += SetClickMovement;
    }

    private void Start()
    {
        foreach (var solider in soldierTypes)
        {
            soldierTypeDictionary.Add(solider.type, solider.obj);
        }

        foreach (var army in armies)
        {
            SetSoldersIdx(army.Legion);
            army.Soldiers.ForEach(s => s.SetOwner(army));
            army.IsMoving = true;
        }

        ChangeArmy(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeArmy(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeArmy(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeArmy(2);
    }

    public Army GetCurArmy() //현재 army 리턴
    {
        return armies[curLegion];
    }
    public void ChangeArmy(int index)
    {
        if (curLegion == index) return;

        curLegion = index;

        for (int i = 0; i < armies.Count; i++)
        {
            if (i == index)
                armies[i].Soldiers.ForEach(s =>
                {
                    s.OutlineCompo.enabled = true;
                    s.HealthCompo.OnUIUpdate?.Invoke(s.HealthCompo.currentHealth, s.HealthCompo.maxHealth);
                });
            else
                armies[i].Soldiers.ForEach(s =>
                {
                    s.OutlineCompo.enabled = false;
                    s.HealthCompo.OffUIUpdate?.Invoke();
                });
        }
    }

    public void SetClickMovement()
    {
        if (armies[curLegion].IsMoving && armies[curLegion].Soldiers.TrueForAll(s => s.NavAgent.enabled))
        {
            RaycastHit hit;

            if (Physics.Raycast(GameManager.Instance.RayPosition(), out hit))
            {
                SetArmyMovePostiton(hit.point);
                ClickParticle.transform.position = hit.point + new Vector3(0, 0.1f, 0);
                ClickParticle.Play();
            }
        }
    }
    public void SetArmyMovePostiton(Vector3 startPos) //배치 시스템 테스트중
    {
        var soldiers = armies[curLegion].Soldiers;

        for (int i = 0; i < soldiers.Count; i++)
        {
            soldiers[i].MoveToMySeat(startPos);
        }
    }

    public void SetSoldersIdx(int legion) //수정
    {
        var soldiers = armies[legion].Soldiers;

        for (int i = 0; i < soldiers.Count; i++)
        {
            var entity = soldiers[i];
            entity.idx = i;
            entity.SetOwner(armies[legion]);
            if (i == 0)  //나는 나요, 너는 선택받은 왕이니 왕관이 쥐어지리
            {
                Instantiate(_crown, entity.transform);
            }
        }
    }

    public void Remove(int legion, Penguin obj)
    {
        var soldiers = armies[legion].Soldiers;
        soldiers.Remove(obj);
        SetSoldersIdx(legion);

        var crown = GameObject.FindGameObjectWithTag("Crown");
        Destroy(crown);
    }

    public void JoinArmyToSoldier(int legion, Penguin obj) //들어가고 싶은 군단, 군인펭귄
    {
        if (armies.Find(p => p.Legion == legion) == null)
        {
            Debug.Log("그런 군단 이름은 없습니다.");
            return;
        }

        armies[legion].Soldiers.Add(obj);
        SetSoldersIdx(legion);
    }
    public void JoinArmyToGeneral(int legion, Penguin obj) //들어가고 싶은 군단, 장군펭귄
    {
        if (armies.Find(p => p.Legion == legion) == null)
        {
            Debug.Log("그런 군단 이름은 없습니다.");
            return;
        }

        if (armies[legion].General != null)
        {
            Debug.Log($"현재 {legion}군단에는 장군이 존재합니다.");
            return;
        }

        armies[legion].General = obj;
    }

    /// <summary>
    /// 팽귄 생성하는 함수
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// Penguin타입만 가능
    /// <param name="SpawnPoint"></param>
    /// 스폰위치
    /// <param name="seatPos"></param>
    /// 마우스 위치 기준으로 배치된 포지션
    /// <returns></returns>

    public T CreateSoldier<T>(PenguinTypeEnum type, Vector3 SpawnPoint, Vector3 seatPos = default) where T : Penguin
    {
        T obj = null;
        var prefab = soldierTypeDictionary[type];

        obj = Instantiate(prefab, SpawnPoint, Quaternion.identity) as T;
        obj.SeatPos = seatPos;
        return obj;
    }
    public void CreateArmy() //군단 추가
    {
        Army newArmy = new Army();
        newArmy.Legion = ArmyCount + 1;
        newArmy.IsMoving = true;
        armies.Add(newArmy);
    }

    private void OnDestroy()
    {
        _inputReader.ClickEvent -= SetClickMovement;
    }
}
