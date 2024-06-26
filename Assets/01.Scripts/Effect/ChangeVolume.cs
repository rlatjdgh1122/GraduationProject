using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public enum VolumeType
{
    None = 0,
    LunarEclipse,
    Test1,
    Test2,
}

//나중에 타입 종류가 많아지면 SO로 빼자
[Serializable]
public class TypeOfVolumeProfile
{
    public VolumeType Type;
    public Volume Volume;
    public float WaitTime;
    public float ChangingDuration;
}

public class ChangeVolume : MonoBehaviour
{
    [SerializeField]
    private List<TypeOfVolumeProfile> _volumeProfileList = new List<TypeOfVolumeProfile>();

    private Dictionary<VolumeType, Coroutine> _activeCoroutines = new Dictionary<VolumeType, Coroutine>();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeVolumeEffect(VolumeType.LunarEclipse, 3f, true);
        }
    }
    public void ChangeVolumeEffect(VolumeType volumeType, float duration, bool loop = false)
    {
        var profile = _volumeProfileList.FirstOrDefault(p => p.Type == volumeType);

        if (profile != null && profile.Volume != null)
        {
            // 이전에 실행 중인 코루틴이 있으면 중지
            if (_activeCoroutines.ContainsKey(volumeType) && _activeCoroutines[volumeType] != null)
            {
                StopCoroutine(_activeCoroutines[volumeType]);
            }

            // 새로운 코루틴을 시작하고 Dictionary에 저장
            _activeCoroutines[volumeType] = StartCoroutine(ChangeVolumeRoutine(profile.Volume, profile.WaitTime, profile.ChangingDuration, duration, loop));
        }
        else
        {
            Debug.LogError($"VolumeProfile of {volumeType} is NULL or not found!");
        }
    }

    private IEnumerator ChangeVolumeRoutine(Volume volume, float waitTime, float changeDuration, float duration, bool loop)
    {
        yield return new WaitForSeconds(waitTime);

        yield return LerpVolumeWeight(volume, 0, 1, changeDuration);

        if (!loop)
        {
            yield return new WaitForSeconds(duration);
            yield return LerpVolumeWeight(volume, 1, 0, changeDuration);
        }
    }

    private IEnumerator LerpVolumeWeight(Volume volume, float startWeight, float endWeight, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            volume.weight = Mathf.Lerp(startWeight, endWeight, elapsed / duration);
            yield return null;
        }

        volume.weight = endWeight;
    }
}
