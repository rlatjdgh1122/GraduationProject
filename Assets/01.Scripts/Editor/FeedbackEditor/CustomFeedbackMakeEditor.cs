using System.CodeDom.Compiler;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MakeFeedbackPlayer))]
public class CustomFeedbackMakeEditor : Editor
{
    private readonly string AddFeedback = "Add Feedback";
    private readonly string Off = "Off";

    private MakeFeedbackPlayer _target;
    private bool IsClick = false;


    private void OnEnable()
    {
        _target = (MakeFeedbackPlayer)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();

        EditorGUILayout.Space(25);

        if (IsClick)
        {
            if (GUILayout.Button(AddFeedback))
            {
                IsClick = !IsClick;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(25);
            if (GUILayout.Button("Knockback", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<KnockbackFeedback>();
            }
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            if (GUILayout.Button(Off, GUILayout.Height(20)))
            {
                IsClick = !IsClick;


            }
        }
    }
}
