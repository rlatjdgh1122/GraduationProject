using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/*[CustomEditor(typeof(BaseStat), true), CanEditMultipleObjects]
public class CustomStatInspectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        BaseStat stat = (BaseStat)target;

        var statList = stat.GetComponents<Stat>();

        foreach(var s in statList)
        {
            GUILayout.Label($" [{s.ReturnFewTimes()}]배 증가 ");
            GUILayout.Label($" 최종 스탯 : [{s.ReturnFinalValue()}] ");
        }

    }
}
*/