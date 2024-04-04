using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using Unity.VisualScripting;

[CustomEditor(typeof(QuestDataSO))]
public class QuestDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        QuestDataSO questDataSO = (QuestDataSO)target;

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("QuestDatas"), true);

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Tutorial Quest Settings", EditorStyles.boldLabel);

        EditorGUI.indentLevel++;

        foreach (QuestData questData in questDataSO.QuestDatas)
        {
            if (questData.IsTutorialQuest)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("TutorialQuestIdx"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("TutorialTexts"), true);
                break; // Assuming only one tutorial quest exists
            }
        }

        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }
}
