using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FeedbackController))]
public class CustomFeedbackMakeEditor : Editor
{
    private readonly string AddFeedback = "Add Feedback";
    private readonly string Off = "Off";

    private FeedbackController _target;
    private bool IsClick = false;

    private void OnEnable()
    {
        _target = (FeedbackController)target; 
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

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
                _target.SpawnFeedback<KnockbackFeedback>(CombatFeedbackEnum.Knockback);
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
                _target.SpawnFeedback<EvasionEffectFeedback>(EffectFeedbackEnum.Evasion);
            }
            if (GUILayout.Button("����", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<ProvokedEffectFeedback>(EffectFeedbackEnum.Provoked);
            }
            if (GUILayout.Button("����", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<StunEffectFeedback>(EffectFeedbackEnum.Stun);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical(); //3�� á�ٸ� ���� ������
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("�±�", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<HitEffectFeedback>(EffectFeedbackEnum.Hit);
            }
            if (GUILayout.Button("�뽬", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<DashEffectFeedback>(EffectFeedbackEnum.Dash);
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

