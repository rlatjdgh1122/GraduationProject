using GluonGui.Dialog;
using StatOperator;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatCalculatorWindow : EditorWindow
{
    [SerializeField] int stat = 0; // 기본 스탯
    [SerializeField] float fewtimes; // 기존보다 몇배 증가했는가
    [SerializeField] float result; // 최종 결과 값
    [SerializeField] List<int> increases = new List<int>();
    [SerializeField] List<int> decreases = new List<int>();

    StatCalculatorEditorDrawer editor;
    Vector2 scrollPosition;

    [MenuItem("계산기/스탯계산기 %F1")] // Ctrl + F1 단축키 설정  
    public static void ShowWindow()
    {
        GetWindow<StatCalculatorWindow>("스탯 계산기");
    }

    private void OnEnable()
    {
        if (!editor) { editor = Editor.CreateEditor(this) as StatCalculatorEditorDrawer; }
    }

    void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);

        GUIStyle largeLabelStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 30,
            alignment = TextAnchor.MiddleCenter
        };

        GUILayout.Label("[스탯 계산기]", largeLabelStyle, GUILayout.Height(40));

        GUILayout.Space(20);
        stat = EditorGUILayout.IntField("기본 스탯", stat);

        if (editor) { editor.OnInspectorGUI(); }

        GUILayout.Space(30);
        if (GUILayout.Button("계산하기"))
        {
            Result();
        }

        GUILayout.Space(20);

        GUIStyle resultLabelSize = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 15
        };

        EditorGUILayout.LabelField("기본 스탯의 ", $"{fewtimes.ToString()}배 증가", resultLabelSize);
        EditorGUILayout.LabelField("최종 스탯 : ", result.ToString(), resultLabelSize);

        GUILayout.EndScrollView();
    }

    private void Result()
    {
        int plusValue = StatCalculator.MultiOperValue(stat, increases);
        int minusValue = StatCalculator.SumOperValue(plusValue, decreases);
        result = StatCalculator.GetValue(plusValue, minusValue);
        fewtimes = StatCalculator.OperTimes(result, stat);
    }
}

[CustomEditor(typeof(StatCalculatorWindow), true)]
public class StatCalculatorEditorDrawer : Editor
{
    SerializedProperty InList;
    SerializedProperty Delist;

    private void OnEnable()
    {
        InList = serializedObject.FindProperty("increases");
        Delist = serializedObject.FindProperty("decreases");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(InList, new GUIContent("상승 스탯"), true);
        EditorGUILayout.PropertyField(Delist, new GUIContent("감소 스탯"), true);

        serializedObject.ApplyModifiedProperties();
    }
}
