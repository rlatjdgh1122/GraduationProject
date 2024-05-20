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
            if (isOn) { return 335.0f; }
            return -335.0f;
        }
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangePhase);

    }
    private void OnEnable()
    {
        SignalHub.OnBattlePhaseEndEvent += OnOffButton;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= OnOffButton;
    }

    public void ChangePhase()
    {
        if (ArmyManager.Instance.CheckEmpty())
        {
            UIManager.Instance.ShowWarningUI("군단에 펭귄을 넣어주세요!");
        }
        else
        {
            WaveManager.Instance.BattlePhaseStartEventHandler();
            SoundManager.Play2DSound(SoundName.StartFight);
            NoiseManager.Instance.SaveNoise();

            //IsBattlePhase가 true일때만 ChangedArmy를 실행할 수 잇는데
            //코드보니깐 전투 시작하고 0.1초 뒤에 true가되길래 0.15초뒤에 실행되게함

            //이렇게말고 우선순위 배틀 스타트에서 IsBattlePhase를 true로 만들어도 되긴함
            CoroutineUtil.CallWaitForSeconds(0.15f, null, () => ArmyManager.Instance.ChangedCurrentArmy());


            OnOffButton();
        }
    }
    private void OnOffButton()
    {
        isOn = !isOn;

        if (isOn) _button.interactable = false;
        else _button.interactable = true;

        gameObject.DOAnchorPos(gameObject.rectTransform().anchoredPosition + new Vector2(moveXValue, 0f), 0.5f).SetEase(Ease.InOutBack);
    }

}
