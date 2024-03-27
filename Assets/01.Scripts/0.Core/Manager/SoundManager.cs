using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    SFX,
    BGM
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private Dictionary<string, AudioSource> bgmContainer = new();
    private AudioMixerGroup bgmMixer;
    private AudioMixerGroup sfxMixer;
    private AudioMixer mainMixer;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {

        GameObject obj = new GameObject("SoundManager");
        instance = obj.AddComponent<SoundManager>();
        instance.InitInstance();

    }

    private void InitInstance()
    {

        mainMixer = Resources.Load<AudioMixer>("Audio/MainMixer");
        sfxMixer = mainMixer.FindMatchingGroups("SFX")[0];
        bgmMixer = mainMixer.FindMatchingGroups("BGM")[0];

        DontDestroyOnLoad(gameObject);

    }

    public static void Play2DSound(string clipName, SoundType type = SoundType.SFX)
    {

        if (instance == null) return;

        instance.Play2D(clipName, type);

    }

    /// <summary>
    /// 함수만 실행하면 오디오소스가 알아서 만들어짐(오디오 소스 만들지 마셈)
    /// </summary>
    /// <param name="clipName">오디오 클립 이름</param>
    /// <param name="position">오디오 소스 생성 위치</param>
    /// <param name="minDistance">오디오 들리는 최소 거리</param>
    /// <param name="maxDistance">오디오 들리는 최대 거리</param>
    /// <param name="type">SFX이니? BGM이니?</param>
    /// <param name="rolloffMode">이건 그냥 건들지 마셈</param>
    public static void Play3DSound(string clipName, Vector3 position,
    float minDistance = 1, float maxDistance = 500,
    SoundType type = SoundType.SFX,
    AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic)
    {

        if (instance == null) return;

        instance.Play3D(clipName, position, minDistance, maxDistance, type, rolloffMode);

    }

    private void Play2D(string clipName, SoundType type)
    {

        GameObject obj = new GameObject();
        var source = obj.AddComponent<AudioSource>();

        AudioClip clip = Resources.Load<AudioClip>($"Sound/{clipName}");

        source.clip = clip;
        source.spatialBlend = 0;
        source.outputAudioMixerGroup = type switch
        {

            SoundType.SFX => instance.sfxMixer,
            SoundType.BGM => instance.bgmMixer,
            _ => null

        };


        if (type == SoundType.BGM)
        {

            source.loop = true;


            if (!bgmContainer.ContainsKey(clipName))
            {

                bgmContainer.Add(clipName, source);

            }


        }
        else
        {

            StartCoroutine(SFXDestroyCo(clip.length, obj));

        }

        source.Play();

    }

    private void Play3D(string clipName, Vector3 position,
        float minDistance = 1, float maxDistance = 500,
        SoundType type = SoundType.SFX,
        AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic)
    {

        GameObject obj = new GameObject();
        obj.transform.position = position;
        var source = obj.AddComponent<AudioSource>();

        AudioClip clip = Resources.Load<AudioClip>($"Sound/{clipName}");

        source.clip = clip;
        source.spatialBlend = 1;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.rolloffMode = rolloffMode;

        source.outputAudioMixerGroup = type switch
        {

            SoundType.SFX => instance.sfxMixer,
            SoundType.BGM => instance.bgmMixer,
            _ => null

        };

        if (type == SoundType.BGM)
        {

            source.loop = true;

            if (!bgmContainer.ContainsKey(clipName))
            {

                bgmContainer.Add(clipName, source);

            }


        }
        else
        {

            StartCoroutine(SFXDestroyCo(clip.length, obj));

        }

        source.Play();

    }

    public static void StopBGM(string clipName)
    {

        if (instance == null) return;
        instance.StopBGMSound(clipName);

    }

    private void StopBGMSound(string clipName)
    {

        if(bgmContainer.TryGetValue(clipName, out var so))
        {

            so.Stop();
            Destroy(so.gameObject);
            bgmContainer.Remove(clipName);

        }

    }

    public static void SettingBgm(float value)
    {
        if (value <= -40f) value = -80f;
        instance.mainMixer.SetFloat("BGM", value);
    }

    public static void SettingSfx(float value)
    {
        if (value <= -40f) value = -80f;

        instance.mainMixer.SetFloat("SFX", value);
    }

    private static IEnumerator SFXDestroyCo(float lenght, GameObject obj)
    {

        yield return new WaitForSeconds(lenght + 0.1f);

        if (obj != null)
        {

            Destroy(obj);

        }

    }

}