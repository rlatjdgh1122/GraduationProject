using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PassiveDataSO))]
public class CustomPassiveInspectorEditor : Editor
{
    private PassiveDataSO comp;

    [Header("�нú� üũ")]
    SerializedProperty Check_IsAttackEvent_Prop;
    SerializedProperty Check_IsSecondEvent_Prop;
    SerializedProperty Check_IsBackAttack_Prop;
    SerializedProperty Check_IsAroundEnemyCountEvent_Prop;
    SerializedProperty Check_IsHealthRatioEvent_Prop;
    SerializedProperty Check_IsHitEvent_Prop;

    [Header("�нú� ����")]
    SerializedProperty Passive_AttackCount_Prop;

    SerializedProperty Passive_EverySecond_Prop;

    SerializedProperty Passive_AroundRadius_Prop;
    SerializedProperty Passive_AroundEnemyCount_Prop;
    SerializedProperty Passive_CheckTarget_Prop;

    SerializedProperty Passive_Ratio_Prop;

    SerializedProperty Passive_HitCount_Prop;

    bool AttackEventGroup = true, SecondEventGroup = true, BackAttackGroup = true, AroundEnemyCountEventEventGroup = true, HealthRatioEventGroup = true, HitEventGroup = true;

    private void OnEnable()
    {
        comp = (PassiveDataSO)target;
    }
    private void Awake()
    {
        Check_IsAttackEvent_Prop = serializedObject.FindProperty("IsAttackEvent");
        Check_IsSecondEvent_Prop = serializedObject.FindProperty("IsSecondEvent");
        Check_IsBackAttack_Prop = serializedObject.FindProperty("IsBackAttack");
        Check_IsAroundEnemyCountEvent_Prop = serializedObject.FindProperty("IsAroundEnemyCountEvent");
        Check_IsHealthRatioEvent_Prop = serializedObject.FindProperty("IsHealthRatioEvent");
        Check_IsHitEvent_Prop = serializedObject.FindProperty("IsHitEvent");

        Passive_AttackCount_Prop = serializedObject.FindProperty("AttackCount");
        Passive_EverySecond_Prop = serializedObject.FindProperty("Second");
        Passive_AroundRadius_Prop = serializedObject.FindProperty("AroundRadius");
        Passive_AroundEnemyCount_Prop = serializedObject.FindProperty("AroundEnemyCount");
        Passive_CheckTarget_Prop = serializedObject.FindProperty("CheckTarget");
        Passive_Ratio_Prop = serializedObject.FindProperty("Ratio");
        Passive_HitCount_Prop = serializedObject.FindProperty("HitCount");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("�нú�");
        EditorGUILayout.Space(5);

        AttackEventGroup = EditorGUILayout.BeginFoldoutHeaderGroup(AttackEventGroup, "�� �� ���� ������");
        if (AttackEventGroup)
        {
            EditorGUILayout.PropertyField(Check_IsAttackEvent_Prop);
            if (Check_IsAttackEvent_Prop.boolValue)
            {
                EditorGUILayout.PropertyField(Passive_AttackCount_Prop);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        SecondEventGroup = EditorGUILayout.BeginFoldoutHeaderGroup(SecondEventGroup, "�� �ʸ���");
        if (SecondEventGroup)
        {
            EditorGUILayout.PropertyField(Check_IsSecondEvent_Prop);
            if (Check_IsSecondEvent_Prop.boolValue)
            {
                EditorGUILayout.PropertyField(Passive_EverySecond_Prop);

            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        BackAttackGroup = EditorGUILayout.BeginFoldoutHeaderGroup(BackAttackGroup, "��ġ��");
        if (BackAttackGroup)
        {
            EditorGUILayout.PropertyField(Check_IsBackAttack_Prop);
            if (Check_IsBackAttack_Prop.boolValue)
            {

            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        AroundEnemyCountEventEventGroup =
        EditorGUILayout.BeginFoldoutHeaderGroup(AroundEnemyCountEventEventGroup, "�ֺ��� �� �� ���");
        if (AroundEnemyCountEventEventGroup)
        {
            EditorGUILayout.PropertyField(Check_IsAroundEnemyCountEvent_Prop);
            if (Check_IsAroundEnemyCountEvent_Prop.boolValue)
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
            EditorGUILayout.PropertyField(Check_IsHealthRatioEvent_Prop);
            if (Check_IsHealthRatioEvent_Prop.boolValue)
            {
                EditorGUILayout.PropertyField(Passive_Ratio_Prop);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        HitEventGroup =
     EditorGUILayout.BeginFoldoutHeaderGroup(HitEventGroup, "�� �� ���� ������");
        if (HitEventGroup)
        {
            EditorGUILayout.PropertyField(Check_IsHitEvent_Prop);
            if (Check_IsHitEvent_Prop.boolValue)
            {
                EditorGUILayout.PropertyField(Passive_HitCount_Prop);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();


        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(comp);
    }
}

