    /*using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(General), true)]
public class CustomPenguinInspectorEditor : Editor
{
    [Header("기본 세팅")]
    SerializedProperty Setting_InnerDistance_Prop;
    SerializedProperty Setting_AttackDistance_Prop;
    SerializedProperty Setting_MoveSpeed_Prop;
    SerializedProperty Setting_AttackSpeed_Prop;
    SerializedProperty Setting_MaxDetectedCount_Prop;
    SerializedProperty Setting_ProvokeRange_Prop;

    [Header("스탯")]
    SerializedProperty Setting_Stat_Prop;
    [Header("패시브")]
    SerializedProperty Passive_Prop;
    [Header("군단 능력치")]
    SerializedProperty LegionStatType_Prop;

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
        Passive_Prop = serializedObject.FindProperty("passiveData");
        LegionStatType_Prop = serializedObject.FindProperty("ligeonStat");
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
                break;

            case PenguinEntityType.Shield:

                break;
            case PenguinEntityType.General:

                EditorGUILayout.PropertyField(Passive_Prop);
                break;

            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
*/