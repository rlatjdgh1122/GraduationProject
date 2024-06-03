using StatOperator;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DamageCalculatorWindow : EditorWindow
{
    [SerializeField] int hpStat = 0; // 체력
    [SerializeField] int armorStat = 0; // 방어력
    [SerializeField] int damage = 0; // 받은 피해
    [SerializeField] List<int> damageList = new List<int>(); //추가 데미지

    private int finalDamage = 0;
    private int finalDamageReceived = 0;
    private int currentHp = 0;
    private int attackCount = 0;

    DamageCalculatorEditorDrawer editor;
    Vector2 scrollPosition;

    [MenuItem("계산기/데미지계산기 %F2")] // Ctrl + F1 단축키 설정  
    public static void ShowWindow()
    {
        GetWindow<DamageCalculatorWindow>("데미지 계산기");
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

        GUILayout.Label("[데미지 계산기]", largeLabelStyle, GUILayout.Height(40));

        GUILayout.Space(20);
        hpStat = EditorGUILayout.IntField("체력", hpStat);
        armorStat = EditorGUILayout.IntField("방어력", armorStat);
        damage = EditorGUILayout.IntField("받은 데미지", damage);

        // 추가 데미지
        if (editor)
        {
            editor.OnInspectorGUI();
        }

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

        EditorGUILayout.LabelField("남은 체력", $"{currentHp}", resultLabelSize);
        EditorGUILayout.LabelField("최종 데미지", $"{finalDamageReceived}", resultLabelSize);
        EditorGUILayout.LabelField("받을 피해", $"{finalDamageReceived}", resultLabelSize);
        EditorGUILayout.LabelField("사망까지", $"{attackCount}대 남음", resultLabelSize);

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

        // 도트 데미지 같은 추가 데미지
        EditorGUILayout.PropertyField(DamageList, new GUIContent("추가 데미지"), true);

        serializedObject.ApplyModifiedProperties();
    }
}
