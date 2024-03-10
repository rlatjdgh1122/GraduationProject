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

        //// Building ������Ƽ �ʵ忡 ��������� �ִٸ�
        //if (EditorGUI.EndChangeCheck())
        //{
        //    // BuildingType�� �°� ��ü���ش�.
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
