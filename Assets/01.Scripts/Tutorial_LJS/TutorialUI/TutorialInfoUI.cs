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
        foreach (var slot in _slotData)
        {
            if(slot.Value.IsCompleted)
            {
                Destroy(slot.Value.gameObject);
            }
        }
    }

    public void CreateSlot(string text)
    {
        TutorialSlot slot = Instantiate(_slotPrefab, _slotParent);
        slot.SetText(text);

        _slotData.Add(text, slot);
    }

    public void CompleteSlot(string text)
    {
        if (_slotData.TryGetValue(text, out TutorialSlot slot))
        {
            slot.TutorialComplete();
        }
    }
}