using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Passive
{
    //��� ����������
    [Header("�� �� ����������")]
    public bool IsAttackEvent = false;
    public int AttackCount = 3;

    //��� ����������
    [Header("�� �� ����")]
    public bool IsSecondEvent = false;
    public float EverySecond = 10f;

    //�ڿ��� ������
    [Header("�ڿ��� ������")]
    public bool IsBackAttack = false;

    //���� �ȿ� �ֺ��� ���� ����ΰ�
    [Header("�ֺ� �� ���")]
    public bool IsAroundEnemyCountEventEvent = false;
    public float AroundRadius = 3;
    public int AroundEnemyCount = 3;

    /// <summary>
    /// �� �ʸ��� �нú� Ȱ��ȭ Ȯ�� ����
    /// </summary>
    /// <returns> ���</returns>
    public bool CheckAttackEventPassive() => IsAttackEvent;

    /// <summary>
    /// �� �븶�� �нú� Ȱ��ȭ Ȯ�� ����
    /// </summary>
    /// <returns> ���</returns>
    public bool CheckSecondEventPassive() => IsSecondEvent;

    /// <summary>
    /// ��ġ�� �нú� Ȱ��ȭ Ȯ�� ����
    /// </summary>
    /// <returns> ���</returns>
    public bool CheckBackAttackEventPassive() => IsAttackEvent;

    /// <summary>
    /// �ֺ��� �� �� ��� �нú� Ȱ��ȭ Ȯ�� ����
    /// </summary>
    /// <returns> ���</returns>
    public bool CheckAroundEnemyCountEventPassive() => IsAttackEvent;
}


[CustomEditor(typeof(PassiveDataSO))]
[CanEditMultipleObjects]
public class CustomPassiveInspectorEditor : Editor
{

    [Header("�нú� üũ")]
    SerializedProperty Passive_IsAttackEvent_Prop;
    SerializedProperty Passive_IsSecondEvent_Prop;
    SerializedProperty Passive_IsBackAttack_Prop;
    SerializedProperty Passive_IsAroundEnemyCountEventEvent_Prop;

    [Header("�нú� ����")]
    SerializedProperty Passive_AttackCount_Prop;
    SerializedProperty Passive_EverySecond_Prop;
    SerializedProperty Passive_AroundRadius_Prop;
    SerializedProperty Passive_AroundEnemyCount_Prop;
    SerializedProperty Passive_CheckTarget_Prop;

    bool AttackEventGroup, SecondEventGroup, BackAttackGroup, AroundEnemyCountEventEventGroup = false;
    private void Awake()
    {
        Passive_IsAttackEvent_Prop = serializedObject.FindProperty("IsAttackEvent");
        Passive_IsSecondEvent_Prop = serializedObject.FindProperty("IsSecondEvent");
        Passive_IsBackAttack_Prop = serializedObject.FindProperty("IsBackAttack");
        Passive_IsAroundEnemyCountEventEvent_Prop = serializedObject.FindProperty("IsAroundEnemyCountEventEvent");

        Passive_AttackCount_Prop = serializedObject.FindProperty("AttackCount");
        Passive_EverySecond_Prop = serializedObject.FindProperty("EverySecond");
        Passive_AroundRadius_Prop = serializedObject.FindProperty("AroundRadius");
        Passive_AroundEnemyCount_Prop = serializedObject.FindProperty("AroundEnemyCount");
        Passive_CheckTarget_Prop = serializedObject.FindProperty("CheckTarget");
    }

    public override void OnInspectorGUI()
    {

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
    }
}

