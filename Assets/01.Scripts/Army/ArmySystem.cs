using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define.Algorithem;
using UnityEngine.Rendering;
using System.Linq;

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

    public void AddStat(int value, StatType type, StatMode mode)
    {
        foreach (var solider in Soldiers)
        {
            solider.AddStat(value, type, mode);
        }
    }
    public void RemoveStat(int value, StatType type, StatMode mode)
    {
        foreach (var solider in Soldiers)
        {
            solider.RemoveStat(value, type, mode);
        }
    }
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
        _inputReader.RightClickEvent += SetClickMovement;
    }

    private void Start()
    {
        foreach (var solider in soldierTypes)
        {
            soldierTypeDictionary.Add(solider.type, solider.obj);
        }

        foreach (var army in armies)
        {
            army.Soldiers.ForEach(s => s.SetOwner(army));
            army.IsMoving = true;
        }

        ChangeArmy(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeArmy(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeArmy(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeArmy(3);
    }

    /// <summary>
    /// 현재 선택된 Army를 리던
    /// </summary>
    /// <returns> Army를 리던</returns>
    public Army GetCurArmy() //현재 army 리턴
    {
        return armies[curLegion];
    }

    /// <summary>
    /// 군단 변경
    /// </summary>
    /// <param name="index"> 몇번째 군단</param>
    private void ChangeArmy(int index)
    {
        if (curLegion == index) return;

        curLegion = index;

        armies.IdxExcept(
            index,
            p =>
            p.Soldiers.ForEach(s =>
            {
                s.OutlineCompo.enabled = true;
                s.HealthCompo.OnUIUpdate?.Invoke(s.HealthCompo.currentHealth, s.HealthCompo.maxHealth);
            })

           ,p =>
           p.Soldiers.ForEach(s =>
           {
               s.OutlineCompo.enabled = false;
               s.HealthCompo.OffUIUpdate?.Invoke();
           }) );
       /* for (int i = 0; i < armies.Count; i++)
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
        }*/
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

    /// <summary>
    /// 배치대로 이동
    /// </summary>
    /// <param name="mousePos"> 마우스 위치</param>

    public void SetArmyMovePostiton(Vector3 mousePos)
    {
        var soldiers = armies[curLegion].Soldiers;

        foreach (var soldier in soldiers)
        {
            soldier.MoveToMySeat(mousePos);
        }
    }

    /// <summary>
    /// 펭귄 지우기
    /// </summary>
    /// <param name="legion"> 몇번째 군단 *owner.Legion 입력*</param>
    /// <param name="obj"> Penguin 타입만 가능 *this 입력*</param>

    public void Remove(int legion, Penguin obj)
    {
        var soldiers = armies[legion].Soldiers;//
        soldiers.Remove(obj); //리스트에서 제외
        // 여기서 죽은 펭귄을 다시 push하는 코드가 필요

        //var crown = GameObject.FindGameObjectWithTag("Crown");
        //Destroy(crown);
    }

    /// <summary>
    /// 장군을 제외한 펭귄을 군단에 넣는 함수
    /// </summary>
    /// <param name="legion"> 몇번째 군단</param>
    /// <param name="obj"> Penguin 타입만 가능</param>

    public void JoinArmyToSoldier(int legion, Penguin obj) //들어가고 싶은 군단, 군인펭귄
    {
        if (armies.Find(p => p.Legion == legion) == null)
        {
            Debug.Log("그런 군단 이름은 없습니다.");
            return;
        }

        armies[legion].Soldiers.Add(obj);
    }

    /// <summary>
    /// 장군펭귄을 군단에 넣는 함수
    /// </summary>
    /// <param name="legion"> 몇번째 군단</param>
    /// <param name="obj"> Penguin 타입만 가능</param>
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
    /// <typeparam name="T"> Penguin 상속받은 애들</typeparam>
    /// <param name="SpawnPoint"> 스폰 위치</param>
    /// <param name="seatPos"> 배치 위치 *되도록이면 사용하지 말것*</param>
    /// <returns> 니가 만든 펭귄</returns>

    public T CreateSoldier<T>(PenguinTypeEnum type, Vector3 SpawnPoint, Vector3 seatPos = default) where T : Penguin
    {
        T obj = null;
        var prefab = soldierTypeDictionary[type];

        //obj = Instantiate(prefab, SpawnPoint, Quaternion.identity) as T;
        obj = PoolManager.Instance.Pop(prefab.name) as T;
        obj.SeatPos = seatPos;
        return obj;
    }

    /// <summary>
    /// 새로운 군단 생성
    /// </summary>
    public void CreateArmy()
    {
        Army newArmy = new Army();
        newArmy.Legion = ArmyCount + 1;
        newArmy.IsMoving = true;
        armies.Add(newArmy);
    }

    private void OnDestroy()
    {
        _inputReader.RightClickEvent -= SetClickMovement;
    }
}
