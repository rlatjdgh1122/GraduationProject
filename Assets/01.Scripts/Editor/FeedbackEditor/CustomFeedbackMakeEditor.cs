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
        if (IsClick)
        {
            if (GUILayout.Button(Off))
            {
                IsClick = !IsClick;
            }

            EditorGUILayout.Space(15);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(3);
            GUILayout.Label("���� ����", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);

            #region ���� ����


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("�� ��", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<KnockbackFeedback>();
            }

            EditorGUILayout.EndHorizontal();
            #endregion

            EditorGUILayout.Space(15);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(3);
            GUILayout.Label("����Ʈ ����", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);

            #region ����Ʈ ����

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("ȸ��", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<EvasionFeedback>();
            }
            if (GUILayout.Button("����", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<ProvokedFeedback>();
            }
            if (GUILayout.Button("����", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<StunFeedback>();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical(); //3�� á�ٸ� ���� ������
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("�±�", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<HitEffectFeedback>();
            }
            if (GUILayout.Button("�뽬", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<DashEffectFeedback>();
            }
            EditorGUILayout.EndHorizontal();

            #endregion
        }
        else
        {
            if (GUILayout.Button(AddFeedback))
            {
                IsClick = !IsClick;


            }
        }
    }
}

