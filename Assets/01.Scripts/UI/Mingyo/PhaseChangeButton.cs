using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseChangeButton : MonoBehaviour
{
    private Button _button;

    private bool isOn = false;

    private float moveXValue
    {
        get
        {
            if (isOn) { return 300.0f; }
            return -300.0f;
        }
    }

    private void Start()
    {
        SignalHub.OnBattlePhaseEndEvent += () => OnOffButton(); // 반드시 바꾸소
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => ChangePhase());
    }

    public void ChangePhase()
    {
        WaveManager.Instance.BattlePhaseStartEventHandler();
        SoundManager.Play2DSound(SoundName.StartFight);
        OnOffButton();
    }

    private void OnOffButton()
    {
        isOn = !isOn;
        gameObject.DOAnchorPos(gameObject.rectTransform().anchoredPosition + new Vector2(moveXValue, 0f), 0.5f).SetEase(Ease.InOutBack);
    }
}
