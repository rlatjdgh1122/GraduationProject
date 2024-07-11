using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SettingArmyPostion : MonoBehaviour
{
    public List<Transform> Transforms = new();

    private List<Vector3> _savePostions = new();

    [Range(0f, 3f)]
    public float Distance = 0f;

    private Vector3 TargetPosition = Vector3.zero;

    private void OnValidate()
    {
        UpdatePositions();
    }

    private void UpdatePositions()
    {
        if (Transforms == null || Transforms.Count == 0)
            return;

        for (int i = 0; i < Transforms.Count; ++i)
        {
            Transform trm = Transforms[i];
            Vector3 savePos = _savePostions[i];

            Vector3 dir = (TargetPosition - trm.position).normalized;
            float dis = Vector3.Distance(savePos, TargetPosition);

            trm.position = TargetPosition - dir * (dis + Distance);
        }
    }

    [ContextMenu("포지션 저장")]
    public void SavePostions()
    {
        List<Vector3> positionList = Transforms.Select(x => x.position).ToList();
        _savePostions = positionList;
    }
}
