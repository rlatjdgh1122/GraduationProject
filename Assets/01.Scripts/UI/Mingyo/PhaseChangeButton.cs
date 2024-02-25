using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseChangeButton : MonoBehaviour
{
    private Button _button;

    private float moveXValue
    {
        get
        {
            if (gameObject.rectTransform().anchoredPosition.x == 1.0) { return -1.0f; }
            else if (gameObject.rectTransform().anchoredPosition.x < 300.0) { return 300.0f; }
            return -300.0f;
        }
    }

    private void Start()
    {
        WaveManager.Instance.OnBattlePhaseEndEvent += () => OnOffButton();
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => ChangePhase());
    }

    private void ChangePhase()
    {
        WaveManager.Instance.BattlePhaseStartEventHandler();
        OnOffButton();
    }

    private void OnOffButton()
    {
        gameObject.DOAnchorPos(gameObject.rectTransform().anchoredPosition + new Vector2(moveXValue, 0f), 0.5f).SetEase(Ease.InOutBack);
    }
}
