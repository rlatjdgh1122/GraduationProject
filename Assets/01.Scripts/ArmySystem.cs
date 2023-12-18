using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define.Algorithem;

[System.Serializable]
public class Army
{
    public int Legion;
    public bool IsMoving;
    public List<Penguin> Soldiers = new();
}

public class ArmySystem : MonoBehaviour
{
    public static ArmySystem Instace;

    [SerializeField] private GameObject _crown;
    [SerializeField] private InputReader _inputReader;
    public ParticleSystem ClickParticle;

    [SerializeField] private List<Army> armies = new();
    public List<Army> Armies { get { return armies; } }

    private int curLegion = 0;
    public int CurLegion => curLegion;

    private void Awake()
    {
        Instace = this;

        ClickParticle = GameObject.Find("ClickParticle").GetComponent<ParticleSystem>();
        _inputReader.ClickEvent += SetClickMovement;
    }

    private void Start()
    {
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
    }

    public void ChangeArmy(int index)
    {
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
                SetArmyMovePostiton(hit.point, curLegion);

                ClickParticle.transform.position = hit.point + new Vector3(0, 0.1f, 0);
                ClickParticle.Play();
            }
        }
    }

    public void SetArmyMovePostiton(Vector3 startPos, int legion) //���콺 ��ġ, ��� idx, ��� ���� �̸�
    {
        var soldiers = armies[legion].Soldiers;
        var trms = Algorithm.AlignmentRule.GetPostionListAround(startPos, 2f, soldiers.Count);

        for (int i = 0; i < soldiers.Count; i++)
        {
            soldiers[i].SetTarget(trms[i]);
        }
    }

    public void SetSoldersIdx(int legion)
    {
        var soldiers = armies[legion].Soldiers;

        Debug.Log(armies[legion].Soldiers.Count);
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

    public void JoinArmy(int legion, Penguin obj) //들어가고 싶은 군단, 
    {
        if (armies.Find(p => p.Legion == legion) == null)
        {
            Debug.Log("그런 군단 이름은 없습니다.");
            return;
        }

        armies[legion].Soldiers.Add(obj);
        SetSoldersIdx(legion);
    }

    private void OnDestroy()
    {
        _inputReader.ClickEvent -= SetClickMovement;
    }
}
