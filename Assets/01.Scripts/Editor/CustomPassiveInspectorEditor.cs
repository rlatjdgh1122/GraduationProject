using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Passive
{
    //몇대 때릴때마다
    [Header("몇 대 때릴때마다")]
    public bool IsAttackEvent = false;
    public int AttackCount = 3;

    //몇대 때릴때마다
    [Header("몇 초 마다")]
    public bool IsSecondEvent = false;
    public float EverySecond = 10f;

    //뒤에서 때릴때
    [Header("뒤에서 때릴때")]
    public bool IsBackAttack = false;

    //범위 안에 주변의 적이 몇명인가
    [Header("주변 적 비례")]
    public bool IsAroundEnemyCountEventEvent = false;
    public float AroundRadius = 3;
    public int AroundEnemyCount = 3;

    /// <summary>
    /// 몇 초마다 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckAttackEventPassive() => IsAttackEvent;

    /// <summary>
    /// 몇 대마다 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckSecondEventPassive() => IsSecondEvent;

    /// <summary>
    /// 뒤치기 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckBackAttackEventPassive() => IsAttackEvent;

    /// <summary>
    /// 주변의 적 수 비례 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckAroundEnemyCountEventPassive() => IsAttackEvent;
}


[CustomEditor(typeof(PassiveDataSO))]
[CanEditMultipleObjects]
public class CustomPassiveInspectorEditor : Editor
{

    [Header("패시브 체크")]
    SerializedProperty Passive_IsAttackEvent_Prop;
    SerializedProperty Passive_IsSecondEvent_Prop;
    SerializedProperty Passive_IsBackAttack_Prop;
    SerializedProperty Passive_IsAroundEnemyCountEventEvent_Prop;

    [Header("패시브 변수")]
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

        EditorGUILayout.LabelField("패시브");
        EditorGUILayout.Space(5);

        AttackEventGroup = EditorGUILayout.BeginFoldoutHeaderGroup(AttackEventGroup, "몇 대 때릴 때마다");
        if (AttackEventGroup)
        {
            EditorGUILayout.PropertyField(Passive_IsAttackEvent_Prop);
            if (Passive_IsAttackEvent_Prop.boolValue)
            {
                EditorGUILayout.PropertyField(Passive_AttackCount_Prop);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        SecondEventGroup = EditorGUILayout.BeginFoldoutHeaderGroup(SecondEventGroup, "몇 초마다");
        if (SecondEventGroup)
        {
            EditorGUILayout.PropertyField(Passive_IsSecondEvent_Prop);
            if (Passive_IsSecondEvent_Prop.boolValue)
            {
                EditorGUILayout.PropertyField(Passive_EverySecond_Prop);

            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        BackAttackGroup = EditorGUILayout.BeginFoldoutHeaderGroup(BackAttackGroup, "뒤치기");
        if (BackAttackGroup)
        {
            EditorGUILayout.PropertyField(Passive_IsBackAttack_Prop);
            if (Passive_IsBackAttack_Prop.boolValue)
            {

            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        AroundEnemyCountEventEventGroup =
           EditorGUILayout.BeginFoldoutHeaderGroup(AroundEnemyCountEventEventGroup, "주변의 적 수 비례");
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

