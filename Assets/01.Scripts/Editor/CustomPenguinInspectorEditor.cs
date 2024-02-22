using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Penguin), true)]
public class CustomPenguinInspectorEditor : Editor
{
    SerializedProperty Option_Prop;
   
    [Header("기본 세팅")]
    SerializedProperty Setting_InnerDistance_Prop;
    SerializedProperty Setting_AttackDistance_Prop;
    SerializedProperty Setting_MoveSpeed_Prop;
    SerializedProperty Setting_AttackSpeed_Prop;
    SerializedProperty Setting_MaxDetectedCount_Prop;
    SerializedProperty Setting_ProvokeRange_Prop;

    [Header("스탯")]
    SerializedProperty Setting_Stat_Prop;

    [Header("투사체")]
    SerializedProperty Archer_ArrowPrefab_Prop;
    SerializedProperty Archer_FirePos_Prop;


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

    bool AttackEventGroup, SecondEventGroup, BackAttackGroup, AroundEnemyCountEventEventGroup = false;
    private void OnEnable()
    {
        Option_Prop = serializedObject.FindProperty("type");

        Setting_InnerDistance_Prop = serializedObject.FindProperty("innerDistance");
        Setting_AttackDistance_Prop = serializedObject.FindProperty("attackDistance");
        Setting_MoveSpeed_Prop = serializedObject.FindProperty("moveSpeed");
        Setting_AttackSpeed_Prop = serializedObject.FindProperty("attackSpeed");
        Setting_MaxDetectedCount_Prop = serializedObject.FindProperty("maxDetectedCount");
        Setting_ProvokeRange_Prop = serializedObject.FindProperty("provokeRange");

        Setting_Stat_Prop = serializedObject.FindProperty("_characterStat");

        Archer_ArrowPrefab_Prop = serializedObject.FindProperty("_arrowPrefab");
        Archer_FirePos_Prop = serializedObject.FindProperty("_firePos");

        Passive_IsAttackEvent_Prop = serializedObject.FindProperty("IsAttackEvent");
        Passive_IsSecondEvent_Prop = serializedObject.FindProperty("IsSecondEvent");
        Passive_IsBackAttack_Prop = serializedObject.FindProperty("IsBackAttack");
        Passive_IsAroundEnemyCountEventEvent_Prop = serializedObject.FindProperty("IsAroundEnemyCountEventEvent");

        Passive_AttackCount_Prop = serializedObject.FindProperty("AttackCount");
        Passive_EverySecond_Prop = serializedObject.FindProperty("EverySecond");
        Passive_AroundRadius_Prop = serializedObject.FindProperty("AroundRadius");
        Passive_AroundEnemyCount_Prop = serializedObject.FindProperty("AroundEnemyCount");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(Option_Prop);

        EditorGUILayout.LabelField("스탯");
        EditorGUILayout.PropertyField(Setting_Stat_Prop);
        EditorGUILayout.Space(5);


        EditorGUILayout.LabelField("기본 설정");
        EditorGUILayout.PropertyField(Setting_InnerDistance_Prop);
        EditorGUILayout.PropertyField(Setting_AttackDistance_Prop);
        EditorGUILayout.PropertyField(Setting_MoveSpeed_Prop);
        EditorGUILayout.PropertyField(Setting_AttackSpeed_Prop);
        EditorGUILayout.PropertyField(Setting_MaxDetectedCount_Prop);
        EditorGUILayout.PropertyField(Setting_ProvokeRange_Prop);
        EditorGUILayout.Space(5);

        PenguinEntityType selectedType = (PenguinEntityType)Option_Prop.enumValueIndex;
        switch (selectedType)
        {
            case PenguinEntityType.Basic:

                break;

            case PenguinEntityType.Archer:
                OnRangeWeapon();
                break;

            case PenguinEntityType.Shield:

                break;

            case PenguinEntityType.MeleeGeneral:
                OnPassive();

                break;

            case PenguinEntityType.RangeGeneral:
                OnRangeWeapon();

                OnPassive();
                break;

            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

    public void OnRangeWeapon()
    {
        EditorGUILayout.LabelField("원거리 무기");
        EditorGUILayout.PropertyField(Archer_ArrowPrefab_Prop);
        EditorGUILayout.PropertyField(Archer_FirePos_Prop);
        EditorGUILayout.Space(5);
    }

    public void OnPassive()
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
                EditorGUILayout.PropertyField(Passive_AroundRadius_Prop);
                EditorGUILayout.PropertyField(Passive_AroundEnemyCount_Prop);

            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}
