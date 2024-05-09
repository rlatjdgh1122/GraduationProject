using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FeedbackController))]
public class CustomFeedbackEditor : Editor
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

            if (GUILayout.Button("전부 가져오기", GUILayout.Width(500), GUILayout.Height(50)))
            {
                _target.SpawnFeedback<KnockbackFeedback>(FeedbackEnumType.Knockback);
                _target.SpawnFeedback<StunFeedback, StunEffectFeedback>(FeedbackEnumType.Stun);

                _target.SpawnFeedback<EvasionEffectFeedback>(FeedbackEnumType.Evasion);
                _target.SpawnFeedback<ProvokedEffectFeedback>(FeedbackEnumType.Provoked);
                _target.SpawnFeedback<HitEffectFeedback>(FeedbackEnumType.Hit);
                _target.SpawnFeedback<EffectFeedback>(FeedbackEnumType.Dash);
                _target.SpawnFeedback<EffectFeedback>(FeedbackEnumType.Vigilance);

                _target.SpawnSoundFeedback(SoundFeedbackEnumType.Attack, SoundName.MeleeAttack);
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.Hit, SoundName.PenguinHit);
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.Dead, SoundName.Dead);
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.WaterFall, SoundName.WaterFall);

                _target.SpawnHitSoundFeedback(HitSoundFeedbackEnumType.MeleeHit, SoundName.MeleeHit);
                _target.SpawnHitSoundFeedback(HitSoundFeedbackEnumType.MopHit, SoundName.MeleeHit);
                _target.SpawnHitSoundFeedback(HitSoundFeedbackEnumType.ArrowHit, SoundName.MeleeHit);
            }
            EditorGUILayout.Space(15);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(3);
            GUILayout.Label("공격 관련", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);

            #region 공격 관련


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("넉 백", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<KnockbackFeedback>(FeedbackEnumType.Knockback);
            }
            if (GUILayout.Button("스턴", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<StunFeedback, StunEffectFeedback>(FeedbackEnumType.Stun);
            }

            EditorGUILayout.EndHorizontal();
            #endregion

            EditorGUILayout.Space(15);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(3);
            GUILayout.Label("이펙트 관련", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);

            #region 이펙트 관련

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("회피", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<EvasionEffectFeedback>(FeedbackEnumType.Evasion);
            }
            if (GUILayout.Button("도발", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<ProvokedEffectFeedback>(FeedbackEnumType.Provoked);
            }
            if (GUILayout.Button("맞기", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<HitEffectFeedback>(FeedbackEnumType.Hit);
            }
            if (GUILayout.Button("대쉬", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<DashEffectFeedback>(FeedbackEnumType.Dash);
            }
            if (GUILayout.Button("각성", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnFeedback<VigilanceEffectFeedback>(FeedbackEnumType.Vigilance);
            }

            EditorGUILayout.EndHorizontal();
            #endregion

            EditorGUILayout.Space(15);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(3);
            GUILayout.Label("사운드 관련", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);

            #region 사운드 관련

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("공격", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.Attack, SoundName.MeleeAttack);
            }
            if (GUILayout.Button("맞기", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.Hit, SoundName.PenguinHit);
            }
            if (GUILayout.Button("죽음", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.Dead, SoundName.Dead);
            }
            if (GUILayout.Button("물에 빠짐", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnSoundFeedback(SoundFeedbackEnumType.WaterFall, SoundName.WaterFall);
            }

            EditorGUILayout.EndHorizontal();

            #endregion

            EditorGUILayout.Space(15);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(3);
            GUILayout.Label("맞는 소리 관련", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);

            #region 맞는 소리 관련

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("일반 공격", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnHitSoundFeedback(HitSoundFeedbackEnumType.MeleeHit, SoundName.MeleeHit);
            }
            if (GUILayout.Button("활 공격", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnHitSoundFeedback(HitSoundFeedbackEnumType.ArrowHit, SoundName.MeleeHit);
            }
            if (GUILayout.Button("대걸레 공격", GUILayout.Width(120), GUILayout.Height(30)))
            {
                _target.SpawnHitSoundFeedback(HitSoundFeedbackEnumType.MopHit, SoundName.Dead);
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

