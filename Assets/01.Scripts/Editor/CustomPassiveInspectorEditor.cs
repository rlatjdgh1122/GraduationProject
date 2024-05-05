using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PassiveDataSO))]
public class CustomPassiveInspectorEditor : Editor
{
    private PassiveDataSO comp;

    [Header("�нú� üũ")]
    SerializedProperty Passive_IsAttackEvent_Prop;
    SerializedProperty Passive_IsSecondEvent_Prop;
    SerializedProperty Passive_IsBackAttack_Prop;
    SerializedProperty Passive_IsAroundEnemyCountEventEvent_Prop;
    SerializedProperty Passive_IsHealthRatioEventEvent_Prop;

    [Header("�нú� ����")]
    SerializedProperty Passive_AttackCount_Prop;
    SerializedProperty Passive_EverySecond_Prop;
    SerializedProperty Passive_AroundRadius_Prop;
    SerializedProperty Passive_AroundEnemyCount_Prop;
    SerializedProperty Passive_CheckTarget_Prop;
    SerializedProperty Passive_Ratio_Prop;

    bool AttackEventGroup = true, SecondEventGroup = true, BackAttackGroup = true, AroundEnemyCountEventEventGroup = true, HealthRatioEventGroup = true;

    private void OnEnable()
    {
        comp = (PassiveDataSO)target;
    }
    private void Awake()
    {
        Passive_IsAttackEvent_Prop = serializedObject.FindProperty("IsAttackEvent");
        Passive_IsSecondEvent_Prop = serializedObject.FindProperty("IsSecondEvent");
        Passive_IsBackAttack_Prop = serializedObject.FindProperty("IsBackAttack");
        Passive_IsAroundEnemyCountEventEvent_Prop = serializedObject.FindProperty("IsAroundEnemyCountEventEvent");
        Passive_IsHealthRatioEventEvent_Prop = serializedObject.FindProperty("IsHealthRatioEventEvent");

        Passive_AttackCount_Prop = serializedObject.FindProperty("AttackCount");
        Passive_EverySecond_Prop = serializedObject.FindProperty("Second");
        Passive_AroundRadius_Prop = serializedObject.FindProperty("AroundRadius");
        Passive_AroundEnemyCount_Prop = serializedObject.FindProperty("AroundEnemyCount");
        Passive_CheckTarget_Prop = serializedObject.FindProperty("CheckTarget");
        Passive_Ratio_Prop = serializedObject.FindProperty("Ratio");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("�нú�");
        EditorGUILayout.Space(5);

        AttackEventGroup = EditorGUILayout.BeginFoldoutHeaderGroup(AttackEventGroup, "�� �� ���� ������");
        if (AttackEventGroup)
        {
            EditorGUILayout.PropertyField(Passive_IsAttackEvent_Prop);
            if (Passive_IsAttackEvent_Prop.boolValue)
            {
                EditorGUILayout.PropertyField(Passive_AttackCount_Prop);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        SecondEventGroup = EditorGUILayout.BeginFoldoutHeaderGroup(SecondEventGroup, "�� �ʸ���");
        if (SecondEventGroup)
        {
            EditorGUILayout.PropertyField(Passive_IsSecondEvent_Prop);
            if (Passive_IsSecondEvent_Prop.boolValue)
            {
                EditorGUILayout.PropertyField(Passive_EverySecond_Prop);

            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        BackAttackGroup = EditorGUILayout.BeginFoldoutHeaderGroup(BackAttackGroup, "��ġ��");
        if (BackAttackGroup)
        {
            EditorGUILayout.PropertyField(Passive_IsBackAttack_Prop);
            if (Passive_IsBackAttack_Prop.boolValue)
            {

            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        AroundEnemyCountEventEventGroup =
        EditorGUILayout.BeginFoldoutHeaderGroup(AroundEnemyCountEventEventGroup, "�ֺ��� �� �� ���");
        if (AroundEnemyCountEventEventGroup)
        {
            EditorGUILayout.PropertyField(Passive_IsAroundEnemyCountEventEvent_Prop);
            if (Passive_IsAroundEnemyCountEventEvent_Prop.boolValue)
            {
                EditorGUILayout.PropertyField(Passive_CheckTarget_Prop);
                EditorGUILayout.PropertyField(Passive_AroundRadius_Prop);
                EditorGUILayout.PropertyField(Passive_AroundEnemyCount_Prop);

            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        HealthRatioEventGroup =
       EditorGUILayout.BeginFoldoutHeaderGroup(HealthRatioEventGroup, "���� ü���� �� �ۼ�Ʈ �� ���");
        if (HealthRatioEventGroup)
        {
            EditorGUILayout.PropertyField(Passive_Ratio_Prop);
            if (Passive_Ratio_Prop.boolValue)
            {
                EditorGUILayout.PropertyField(Passive_Ratio_Prop);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();


        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(comp);
    }
}

