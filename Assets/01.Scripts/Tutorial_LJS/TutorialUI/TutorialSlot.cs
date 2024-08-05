using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSlot : MonoBehaviour
{
    [SerializeField] private Sprite _completeIcon;   
    [SerializeField] private Sprite _proceedingIcon;

    [SerializeField] private Color _completeColor;
    [SerializeField] private Color _proceedingColor;

    private Image _slotIcon;
    private Image _slotPanel;
    private TextMeshProUGUI _tutorialText;

    public bool IsCompleted { get; private set; }

    private void Awake()
    {
        _slotIcon = transform.Find("Icon").GetComponent<Image>();
        _slotPanel = transform.Find("Panel").GetComponent<Image>();
        _tutorialText = transform.Find("TutorialText").GetComponent<TextMeshProUGUI>();

        TutorialProceeding();
    }

    public void SetText(string text)
    {
        _tutorialText.text = text;
    }

    public void TutorialComplete()
    {
        _slotIcon.sprite = _completeIcon;
        _slotPanel.color = _completeColor;
        _slotIcon.color = Color.white;

        IsCompleted = true;
    }

    public void TutorialProceeding()
    {
        _slotIcon.sprite = _proceedingIcon;
        _slotPanel.color = _proceedingColor;
        _slotIcon.color = Color.black;
    }
} 