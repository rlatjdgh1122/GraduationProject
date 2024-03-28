using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public enum SoundType
{
    SFX,
    BGM
}

public enum SoundName
{
    MeleeAttack,
    ArrowAttack
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private Dictionary<SoundName, AudioSource> bgmContainer = new();
    private AudioMixerGroup bgmMixer;
    private AudioMixerGroup sfxMixer;
    private AudioMixer mainMixer;

    private Camera mainCamera;
    private float initialVolume;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

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

    public static void Play2DSound(SoundName name, SoundType type = SoundType.SFX)
    {

        if (instance == null) return;

        instance.Play2D(name, type);

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
    public static void Play3DSound(SoundName clipName, Vector3 position,
    float minFOV = 20, float maxFOV = 55, float maxDistance = 50,
    SoundType type = SoundType.SFX,
    AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic)
    {

        if (instance == null) return;

        instance.Play3D(clipName, position, minFOV, maxFOV, maxDistance, type, rolloffMode);

    }

    private void Play2D(SoundName clipName, SoundType type)
    {

        GameObject obj = new GameObject();
        var source = obj.AddComponent<AudioSource>();

        AudioClip clip = Resources.Load<AudioClip>($"Sound/{clipName.ToString()}");

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

    private void Play3D(SoundName clipName, Vector3 position,
        float minFOV = 20, float maxFOV = 55, float maxDistance = 50,
        SoundType type = SoundType.SFX,
        AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic)
    {

        GameObject obj = new GameObject();
        obj.transform.position = position;
        var source = obj.AddComponent<AudioSource>();

        AudioClip clip = Resources.Load<AudioClip>($"Sound/{clipName.ToString()}");

        source.clip = clip;
        source.spatialBlend = 0.8f;
        source.rolloffMode = rolloffMode;

        initialVolume = source.volume;

        float distance = Vector3.Distance(source.transform.position, mainCamera.transform.position);

        if (distance > maxDistance) return;

        float fovNormalization = 1 - (float)(mainCamera.fieldOfView - minFOV) / (maxFOV - minFOV);

        source.volume = fovNormalization;

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

    public static void StopBGM(SoundName clipName)
    {

        if (instance == null) return;
        instance.StopBGMSound(clipName);

    }

    private void StopBGMSound(SoundName clipName)
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