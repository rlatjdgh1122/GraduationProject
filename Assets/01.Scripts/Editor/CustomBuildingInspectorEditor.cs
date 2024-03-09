using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildingItemInfo))]
public class CustomBuildingInspectorEditor : Editor
{
    SerializedProperty BuildingTypeEnum_Property;
    SerializedProperty BuffItemInfoST_Property;

    void OnEnable()
    {
        BuildingTypeEnum_Property = serializedObject.FindProperty("_buildingTypeEnum");
        BuffItemInfoST_Property = serializedObject.FindProperty("_innderDistance");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        BuildingType selectedEnum = (BuildingType)BuildingTypeEnum_Property.enumValueIndex;
        switch (selectedEnum)
        {
            case BuildingType.Buff:
                EditorGUILayout.PropertyField(BuffItemInfoST_Property);
                break;
            case BuildingType.Defense:
                break;
            case BuildingType.Resource:
                break;
        }
        EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();
        //EditorGUI.BeginChangeCheck();
        //EditorGUILayout.PropertyField(BuildingTypeEnum_Property);

        //// Building 프로퍼티 필드에 변경사항이 있다면
        //if (EditorGUI.EndChangeCheck())
        //{
        //    // BuildingType에 맞게 교체해준다.
        //    ChangeBuildingInfoInspector();
        //}
    }

    private void ChangeBuildingInfoInspector()
    {
        BuildingType selectedEnum = (BuildingType)BuildingTypeEnum_Property.enumValueIndex;
        switch (selectedEnum)
        {
            case BuildingType.Buff:
                EditorGUILayout.PropertyField(BuffItemInfoST_Property);
                break;
            case BuildingType.Defense:
                break;
            case BuildingType.Resource:
                break;
        }
        EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();
    }
}
