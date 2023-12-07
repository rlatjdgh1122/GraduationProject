using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define.Algorithem;

[System.Serializable]
public class Army
{
    public int Legion;
    public List<Entity> Soldiers = new();
}
public class ArmySystem : MonoBehaviour
{
    public static ArmySystem Instace;

    [SerializeField] private GameObject _crown;

    [SerializeField] private List<Army> armies = new();

    private void Awake()
    {
        Instace = this;
    }
    private void Start()
    {
        foreach (var army in armies)
        {
            SetSoldersIdx(army.Legion);
        }
    }

    
    public void SetArmyMovePostiton(Vector3 startPos, int idx, int legion) //���콺 ��ġ, ��� idx, ��� ���� �̸�
    {
        var soldiers = armies[legion].Soldiers;
        var trms = Algorithm.AlignmentRule.GetPostionListAround(startPos, 2f, soldiers.Count);

        for (int i = 0; i < soldiers.Count; i++)
        {
            if (idx == i)
            {
                var entity = soldiers[i];
                entity.SetTarget(trms[i]);
            }
        }
    }

    public void SetSoldersIdx(int legion)
    {
        var soldiers = armies[legion].Soldiers;

        for (int i = 0; i < soldiers.Count; i++)
        {
            var entity = soldiers[i];
            entity.idx = i;
            if (i == 0)  //나는 나요, 너는 선택받은 왕이니 왕관이 쥐어지리
            {
                Instantiate(_crown, entity.transform);
            }
        }
    }

    public void Remove(int legion, Entity obj)
    {
        var soldiers = armies[legion].Soldiers;
        soldiers.Remove(obj);
        SetSoldersIdx(legion);

        var crown = GameObject.FindGameObjectWithTag("Crown");
        Destroy(crown);

        SetIdx();
    }
}
