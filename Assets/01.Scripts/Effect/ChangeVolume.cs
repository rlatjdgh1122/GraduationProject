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

//���߿� Ÿ�� ������ �������� SO�� ����
[Serializable]
public class TypeOfVolumeProfile
{
    public VolumeType Type;
    public Volume Volume;
    public float ChangingDuration;
}

public class ChangeVolume : MonoBehaviour
{
    [SerializeField]
    private List<TypeOfVolumeProfile> _volumeProfileList = new List<TypeOfVolumeProfile>();

    private Dictionary<VolumeType, Coroutine> _activeCoroutines = new Dictionary<VolumeType, Coroutine>();

#if UNITY_EDITOR
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("����׿��Դϴ糪�߿������ּ���");
            ChangeVolumeEffect(VolumeType.LunarEclipse, 3f);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("����׿��Դϴ糪�߿������ּ���");
            ChangeVolumeEffect(VolumeType.Test1, 3f, true);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("����׿��Դϴ糪�߿������ּ���");
            ChangeVolumeEffect(VolumeType.Test2, 5f);
        }
    }
#endif

    public void ChangeVolumeEffect(VolumeType volumeType, float duration, bool loop = false)
    {
        var profile = _volumeProfileList.FirstOrDefault(p => p.Type == volumeType);

        if (profile != null && profile.Volume != null)
        {
            // ������ ���� ���� �ڷ�ƾ�� ������ ����
            if (_activeCoroutines.ContainsKey(volumeType) && _activeCoroutines[volumeType] != null)
            {
                StopCoroutine(_activeCoroutines[volumeType]);
            }

            // ���ο� �ڷ�ƾ�� �����ϰ� Dictionary�� ����
            _activeCoroutines[volumeType] = StartCoroutine(ChangeVolumeRoutine(profile.Volume, profile.ChangingDuration, duration, loop));
        }
        else
        {
            Debug.LogError($"VolumeProfile of {volumeType} is NULL or not found!");
        }
    }

    private IEnumerator ChangeVolumeRoutine(Volume volume, float changeDuration, float duration, bool loop)
    {
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
