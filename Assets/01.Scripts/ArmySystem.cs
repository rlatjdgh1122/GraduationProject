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
        for (int i = 0; i < armyTrms.Count; i++)
        {
            var entity = armyTrms[i] as Entity;
            entity.idx = i;
        }
    }
    public void SetArmyMovePostiton(Vector3 startPos, int idx)
    {
        a.Clear();
        var trms = Algorithm.AlignmentRule.GetPostionListAround(startPos, new float[] { 3f, 7f, 10f }, new int[] { armyTrms.Count / 3, armyTrms.Count / 2, armyTrms.Count / 1 });
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
}
