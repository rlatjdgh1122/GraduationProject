using Define.CamDefine;
using DG.Tweening;
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
    ArrowAttack,
    ArrowReload,
    StartFight,
    NormalBGM,
    NormalBattleBGM,
    Dead,
    BiteAttack,
    Explosion,
    SlashAttack,
    Cash,
    PenguinHit,
    Button,
    GroundHit,
    MopAttack,
    WaterFall,
    UI,
    PowerMopAttack,
    Buy,
    Bear,
    DigStone,
    QuestEnd,
    QuestStart,
    Build,
    Win,
    Lose,
    CostBox,
    LevelUp,
    None,
    WoodCut,


    #region Buildings

    MortarFire,
    MortarExplosion,
    RopeBurning,

    #endregion
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
            
    private Dictionary<SoundName, AudioSource> bgmContainer = new();
    private AudioMixerGroup bgmMixer;
    private AudioMixerGroup sfxMixer; 
    private AudioMixer mainMixer;

    private Camera mainCamera;

    private void Awake()
    {
        //�ƴ� �̰� �� �ʵ�??????????
        mainCamera = Cam.MainCam;
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

        if(name == SoundName.None) return;

        instance.Play2D(name, type);

    }

    /// <summary>
    /// �Լ��� �����ϸ� ������ҽ��� �˾Ƽ� �������(����� �ҽ� ������ ����)
    /// </summary>
    /// <param name="clipName">����� Ŭ�� �̸�</param>
    /// <param name="position">����� �ҽ� ���� ��ġ</param>
    /// <param name="minDistance">����� �鸮�� �ּ� �Ÿ�</param>
    /// <param name="maxDistance">����� �鸮�� �ִ� �Ÿ�</param>
    /// <param name="type">SFX�̴�? BGM�̴�?</param>
    /// <param name="rolloffMode">�̰� �׳� �ǵ��� ����</param>
    public static void Play3DSound(SoundName clipName, Vector3 position,
    float minFOV = 20, float maxFOV = 55, float maxDistance = 50,
    SoundType type = SoundType.SFX,
    AudioRolloffMode rolloffMode = AudioRolloffMode.Linear)
    {

        if (instance == null) return;

        if (clipName == SoundName.None) return;

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
        AudioRolloffMode rolloffMode = AudioRolloffMode.Linear)
    {

        float distance = Vector3.Distance(position, Cam.MainCam.transform.position);

        if (distance > maxDistance) return;

        GameObject obj = new GameObject();
        obj.transform.position = position;
        var source = obj.AddComponent<AudioSource>();

        AudioClip clip = Resources.Load<AudioClip>($"Sound/{clipName.ToString()}");

        source.clip = clip;
        source.spatialBlend = .8f;
        source.rolloffMode = rolloffMode;

        float fovNormalization = 1 - (float)(Cam.MainCam.fieldOfView - minFOV) / (maxFOV - minFOV);
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

        if(bgmContainer.TryGetValue(clipName, out AudioSource so))
        {
            StartCoroutine(FadeInVolume(so, 0, 0.7f));
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

    private IEnumerator FadeInVolume(AudioSource audioSource, float targetVolume, float duration)
    {
        float currentTime = 0;
        float startVolume = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
        Destroy(audioSource.gameObject);
    }

    private static IEnumerator SFXDestroyCo(float lenght, GameObject obj)
    {

        yield return new WaitForSeconds(lenght + 0.1f);

        if (obj != null)
        {
            //Debug.Log(obj);
            Destroy(obj);

        }

    }

}