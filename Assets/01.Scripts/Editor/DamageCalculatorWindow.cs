using StatOperator;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DamageCalculatorWindow : EditorWindow
{
    [SerializeField] int hpStat = 0; // ü��
    [SerializeField] int armorStat = 0; // ����
    [SerializeField] int damage = 0; // ���� ����
    [SerializeField] List<int> damageList = new List<int>(); //�߰� ������

    private int finalDamage = 0;
    private int finalDamageReceived = 0;
    private int currentHp = 0;
    private int attackCount = 0;

    DamageCalculatorEditorDrawer editor;
    Vector2 scrollPosition;

    [MenuItem("����/���������� %F2")] // Ctrl + F1 ����Ű ����  
    public static void ShowWindow()
    {
        GetWindow<DamageCalculatorWindow>("������ ����");
    }

    private void OnEnable()
    {
        if (!editor)
        {
            editor = Editor.CreateEditor(this) as DamageCalculatorEditorDrawer;
        }
    }

    void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);

        GUIStyle largeLabelStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 30,
            alignment = TextAnchor.MiddleCenter
        };

        GUILayout.Label("[������ ����]", largeLabelStyle, GUILayout.Height(40));

        GUILayout.Space(20);
        hpStat = EditorGUILayout.IntField("ü��", hpStat);
        armorStat = EditorGUILayout.IntField("����", armorStat);
        damage = EditorGUILayout.IntField("���� ������", damage);

        // �߰� ������
        if (editor)
        {
            editor.OnInspectorGUI();
        }

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

        EditorGUILayout.LabelField("���� ü��", $"{currentHp}", resultLabelSize);
        EditorGUILayout.LabelField("���� ������", $"{finalDamageReceived}", resultLabelSize);
        EditorGUILayout.LabelField("���� ����", $"{finalDamageReceived}", resultLabelSize);
        EditorGUILayout.LabelField("�������", $"{attackCount}�� ����", resultLabelSize);

        GUILayout.EndScrollView();
    }

    private void Result()
    {
        finalDamage = damage;
        for (int i = 0; i < damageList.Count; ++i)
        {
            finalDamage += damageList[i];
        }

        finalDamageReceived = StatCalculator.GetDamage(finalDamage, armorStat);
        currentHp = hpStat - finalDamageReceived;

        if (finalDamageReceived > 0)
        {
            int share = (hpStat / finalDamageReceived);  
            int remainer = (hpStat % finalDamageReceived);  
            attackCount = (remainer > 0) ? share + 1 : share;
        }
    }
}

[CustomEditor(typeof(DamageCalculatorWindow), true)]
public class DamageCalculatorEditorDrawer : Editor
{
    SerializedProperty DamageList;

    private void OnEnable()
    {
        DamageList = serializedObject.FindProperty("damageList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // ��Ʈ ������ ���� �߰� ������
        EditorGUILayout.PropertyField(DamageList, new GUIContent("�߰� ������"), true);

        serializedObject.ApplyModifiedProperties();
    }
}
