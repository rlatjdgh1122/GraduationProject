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


            EditorGUILayout.Space(20);

            if (GUILayout.Button("���� ��������", GUILayout.Width(500), GUILayout.Height(50)))
            {
                _target.SpawnFeedback<KnockbackFeedback>(FeedbackEnumType.Knockback);
                _target.SpawnFeedback<StunFeedback, StunEffectFeedback>(FeedbackEnumType.Stun);

                _target.SpawnFeedback<EvasionEffectFeedback>(FeedbackEnumType.Evasion);
                _target.SpawnFeedback<ProvokedEffectFeedback>(FeedbackEnumType.Provoked);
                _target.SpawnFeedback<HitEffectFeedback>(FeedbackEnumType.Hit);
                _target.SpawnFeedback<DashEffectFeedback>(FeedbackEnumType.Dash);

                _target.SpawnSoundFeedback(SoundFeedbackEnumType.Attack, SoundName.MeleeAttack);
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.Hit, SoundName.PenguinHit);
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.Dead, SoundName.Dead);
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.WaterFall, SoundName.WaterFall);
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
                _target.SpawnFeedback<KnockbackFeedback>(FeedbackEnumType.Knockback);
            }
            if (GUILayout.Button("����", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<StunFeedback, StunEffectFeedback>(FeedbackEnumType.Stun);
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
                _target.SpawnFeedback<EvasionEffectFeedback>(FeedbackEnumType.Evasion);
            }
            if (GUILayout.Button("����", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<ProvokedEffectFeedback>(FeedbackEnumType.Provoked);
            }
            if (GUILayout.Button("�±�", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<HitEffectFeedback>(FeedbackEnumType.Hit);
            }
            if (GUILayout.Button("�뽬", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<DashEffectFeedback>(FeedbackEnumType.Dash);
            }

            EditorGUILayout.EndHorizontal();
            #endregion

            EditorGUILayout.Space(15);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(3);
            GUILayout.Label("���� ����", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);

            #region ���� ����

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("����", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.Attack, SoundName.MeleeAttack);
            }
            if (GUILayout.Button("�±�", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.Hit, SoundName.PenguinHit);
            }
            if (GUILayout.Button("����", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.Dead, SoundName.Dead);
            }
            if (GUILayout.Button("���� ����", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.WaterFall, SoundName.WaterFall);
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

