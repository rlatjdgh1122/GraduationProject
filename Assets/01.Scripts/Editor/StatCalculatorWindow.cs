using GluonGui.Dialog;
using StatOperator;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatCalculatorWindow : EditorWindow
{
    [SerializeField] int stat = 0; // �⺻ ����
    [SerializeField] float fewtimes; // �������� ��� �����ߴ°�
    [SerializeField] float result; // ���� ��� ��
    [SerializeField] List<int> increases = new List<int>();
    [SerializeField] List<int> decreases = new List<int>();

    StatCalculatorEditorDrawer editor;
    Vector2 scrollPosition;

    [MenuItem("����/���Ȱ��� %F1")] // Ctrl + F1 ����Ű ����  
    public static void ShowWindow()
    {
        GetWindow<StatCalculatorWindow>("���� ����");
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

        GUILayout.Label("[���� ����]", largeLabelStyle, GUILayout.Height(40));

        GUILayout.Space(20);
        stat = EditorGUILayout.IntField("�⺻ ����", stat);

        if (editor) { editor.OnInspectorGUI(); }

        GUILayout.Space(30);
        if (GUILayout.Button("����ϱ�"))
        {
            Result();
        }

        GUILayout.Space(20);

        GUIStyle resultLabelSize = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 15
        };

        EditorGUILayout.LabelField("�⺻ ������ ", $"{fewtimes.ToString()}�� ����", resultLabelSize);
        EditorGUILayout.LabelField("���� ���� : ", result.ToString(), resultLabelSize);

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

        EditorGUILayout.PropertyField(InList, new GUIContent("��� ����"), true);
        EditorGUILayout.PropertyField(Delist, new GUIContent("���� ����"), true);

        serializedObject.ApplyModifiedProperties();
    }
}
