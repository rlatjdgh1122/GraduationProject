using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialInfoUI : MonoBehaviour
{
    [SerializeField] private TutorialSlot _slotPrefab;
    [SerializeField] private Transform _slotParent;

    private Dictionary<string, TutorialSlot> _slotData = new();

    public void Init()
    {
        // 모든 슬롯을 안전하게 삭제
        List<string> keysToRemove = new List<string>();

        foreach (var slot in _slotData)
        {
            if (slot.Value != null && slot.Value.gameObject != null)
            {
                Destroy(slot.Value.gameObject);
                keysToRemove.Add(slot.Key);
            }
        }

        // 슬롯 데이터를 안전하게 제거
        foreach (var key in keysToRemove)
        {
            _slotData.Remove(key);
        }

    }

    public void CreateSlot(string text)
    {
        if (!_slotData.ContainsKey(text))
        {
            TutorialSlot slot = Instantiate(_slotPrefab, _slotParent);
            slot.SetText(text);
            _slotData.Add(text, slot);
        }
        else
        {
            Debug.LogWarning($"Slot with text '{text}' already exists.");
        }
    }

    public void CompleteSlot(string text)
    {
        if (_slotData.TryGetValue(text, out TutorialSlot slot))
        {
            SoundManager.Play2DSound(SoundName.QuestEnd);

            if (slot != null)
            {
                slot.TutorialComplete();
            }
            else
            {
                Debug.LogWarning($"Slot with text '{text}' is null.");
            }
        }
        else
        {
            Debug.LogWarning($"No slot found with text '{text}'.");
        }
    }
}