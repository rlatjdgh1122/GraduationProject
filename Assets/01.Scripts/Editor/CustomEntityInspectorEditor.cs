using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Penguin), true)]
public class CustomEntityInspectorEditor : Editor //잠시대기
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

    [Header("공격횟수 패시브")]
    SerializedProperty Passive_AttackCount_Prop;
    private void Awake()
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
        Passive_AttackCount_Prop = serializedObject.FindProperty("EveryAttackCount");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(Option_Prop);

        EditorGUILayout.PropertyField(Setting_Stat_Prop);

        EditorGUILayout.PropertyField(Setting_InnerDistance_Prop);
        EditorGUILayout.PropertyField(Setting_AttackDistance_Prop);
        EditorGUILayout.PropertyField(Setting_MoveSpeed_Prop);
        EditorGUILayout.PropertyField(Setting_AttackSpeed_Prop);
        EditorGUILayout.PropertyField(Setting_MaxDetectedCount_Prop);
        EditorGUILayout.PropertyField(Setting_ProvokeRange_Prop);

        PenguinEntityType selectedType = (PenguinEntityType)Option_Prop.enumValueIndex;
        switch (selectedType)
        {
            case PenguinEntityType.Basic:
                

                break;
            case PenguinEntityType.Archer:
                EditorGUILayout.PropertyField(Archer_ArrowPrefab_Prop);
                EditorGUILayout.PropertyField(Archer_FirePos_Prop);
                break;
            case PenguinEntityType.Shield:
                break;
            case PenguinEntityType.Mop:

                EditorGUILayout.PropertyField(Passive_AttackCount_Prop);
                break;

            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
