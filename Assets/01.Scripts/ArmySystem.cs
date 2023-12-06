using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define.Algorithem;
public class ArmySystem : MonoBehaviour
{
    public static ArmySystem Instace;
    [SerializeField] private List<Penguin> armyTrms = new();
    [SerializeField] private List<Vector3> a = new();

    private void Awake()
    {
        Instace = this;
    }
    private void Start()
    {
        SetIdx();
    }
    public void SetArmyMovePostiton(Vector3 startPos, int idx)
    {
        a.Clear();
        var trms = Algorithm.AlignmentRule.GetPostionListAround(startPos, 2f, armyTrms.Count);
        a.AddRange(trms);
        for (int i = 0; i < armyTrms.Count; i++)
        {
            if (idx == i)
            {
                var entity = armyTrms[i] as Entity;
                entity.SetTarget(trms[i]);
            }
        }
    }

    public void SetIdx()
    {
        for (int i = 0; i < armyTrms.Count; i++)
        {
            var entity = armyTrms[i] as Entity;
            entity.idx = i;
        }
    }

    public void Remove(Penguin obj)
    {
        armyTrms.Remove(obj);
        SetIdx();
    }
}
