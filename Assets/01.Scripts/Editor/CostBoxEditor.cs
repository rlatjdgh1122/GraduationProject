using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CostBox))]
public class CostBoxEditor : Editor
{
    private CostBox _costBox;

    private void OnEnable()
    {
        _costBox = (CostBox)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BoxEvent inspectorObj = _costBox.InspectorCustomBox;

        if(inspectorObj.Type == BoxType.Golden)
        {
            inspectorObj.Duration     = EditorGUILayout.FloatField ("Openning Duration", inspectorObj.Duration);
            inspectorObj.OpenLidAngle = EditorGUILayout.FloatField ("Open Lid Angle", inspectorObj.OpenLidAngle);
            inspectorObj.Lid          = EditorGUILayout.ObjectField("Lid Transform", inspectorObj.Lid, typeof(Transform), true) as Transform;
        }
    }
}
