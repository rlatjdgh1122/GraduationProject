using Define.CamDefine;
using Define.Resources;
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

public enum SoundName : uint
{
    MeleeAttack = 0,
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
    WoodCut,
    FallDown,
    GorillaPunch,
    MeleeHit,
    ArrowHit,

    #region Buildings

    MortarFire,
    MortarExplosion,
    RopeBurning,
    BearTrap,

    #endregion
    Credit,

    #region Skill

    SkillLance,
    SkillPolice,
    SkillKatana,
    SkillEndKatana,

    #endregion

    #region Ultimate

    UltimateLance,
    UltimateLanceExplosion,
    UltimatePolice,
    UltimateKatana,

    #endregion

    TutorialStart,
    Glitch,
    TutorialBGM,
    None = 100,
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private Dictionary<SoundName, AudioSource> bgmContainer = new();
    private AudioMixerGroup bgmMixer;
    private AudioMixerGroup sfxMixer;
    private AudioMixer mainMixer;

    private Dictionary<SoundName, float> soundCooldowns = new(); // 사운드 재생 쿨다운을 관리할 딕셔너리
    private float cooldownTime = 0.1f; // 사운드 중복 재생 방지 시간


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        GameObject obj = new GameObject("SoundManager");
        instance = obj.AddComponent<SoundManager>();
        instance.InitInstance();

    }

    private void InitInstance()
    {
        mainMixer = VResources.Load<AudioMixer>("Audio/MainMixer");

        sfxMixer = mainMixer.FindMatchingGroups("SFX")[0];
        bgmMixer = mainMixer.FindMatchingGroups("BGM")[0];

        DontDestroyOnLoad(gameObject);
    }

    public static void Play2DSound(SoundName name, SoundType type = SoundType.SFX)
    {
        if (instance == null || name == SoundName.None) return;

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
    float minFOV = 20, float maxFOV = 60, float maxDistance = 55,
    SoundType type = SoundType.SFX,
    AudioRolloffMode rolloffMode = AudioRolloffMode.Linear)
    {
        if (instance == null) return;

        if (clipName == SoundName.None) return;

        instance.Play3D(clipName, position, minFOV, maxFOV, maxDistance, type, rolloffMode);
    }

    private void Play2D(SoundName clipName, SoundType type)
    {
        if (soundCooldowns.ContainsKey(clipName) && Time.time < soundCooldowns[clipName] + cooldownTime)
        {
            return; // 사운드 쿨다운 시간 내에 중복 재생을 방지
        }

        soundCooldowns[clipName] = Time.time; // 사운드 재생 시간 기록

        GameObject obj = new GameObject();
        var source = obj.AddComponent<AudioSource>();

        AudioClip clip = VResources.Load<AudioClip>($"Sound/{clipName.ToString()}");

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

        AudioClip clip = VResources.Load<AudioClip>($"Sound/{clipName.ToString()}");

        source.clip = clip;
        source.spatialBlend = .8f;
        source.rolloffMode = rolloffMode;

        float fovNormalization = 1 - (float)(Cam.MainCam.fieldOfView - minFOV) / (maxFOV - minFOV);
        fovNormalization = Mathf.Clamp(fovNormalization, 0.1f, 1);
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

        if (bgmContainer.TryGetValue(clipName, out AudioSource so))
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
    public static void ResetSound()
    {
        instance.SoundClear();
    }

    private void SoundClear()
    {
        bgmContainer.TryClear();
    }

    private void OnDestroy()
    {
        VResources.UnloadAssetsAll();
    }

}