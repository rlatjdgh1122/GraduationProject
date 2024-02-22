using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildingItemInfo))]
public class CustomBuildingInspectorEditor : Editor
{
    SerializedProperty BuildingTypeEnum;

    SerializedProperty Setting_InnerDistance_Prop;
    SerializedProperty Setting__TargetLayer_Prop;
    SerializedProperty Setting__DefaultBuffValue_Prop;

    void OnEnable()
    {
        BuildingTypeEnum = serializedObject.FindProperty("BuildingTypeEnum");
        Setting_InnerDistance_Prop = serializedObject.FindProperty("InnerDistance");
        Setting__TargetLayer_Prop = serializedObject.FindProperty("TargetLayer");
        Setting__DefaultBuffValue_Prop = serializedObject.FindProperty("DefaultBuffValue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(BuildingTypeEnum);

        // Building ������Ƽ �ʵ忡 ��������� �ִٸ�
        if (EditorGUI.EndChangeCheck())
        {
            // BuildingType�� �°� ��ü���ش�.
            ChangeBuildingInfoInspector();
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void ChangeBuildingInfoInspector()
    {
        BuildingType selectedEnum = (BuildingType)BuildingTypeEnum.enumValueIndex;
        Debug.Log(selectedEnum);
        switch (selectedEnum)
        {
            case BuildingType.BuffBuilding:
                EditorGUILayout.PropertyField(Setting_InnerDistance_Prop);
                EditorGUILayout.PropertyField(Setting__TargetLayer_Prop);
                EditorGUILayout.PropertyField(Setting__DefaultBuffValue_Prop);
                break;
            case BuildingType.DefenseBuilding:
                break;
            case BuildingType.ResourceBuilding:
                break;
        }

    }
}
