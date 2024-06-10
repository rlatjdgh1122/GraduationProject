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
            UIManager.Instance.ShowWarningUI("군단에 펭귄을 추가해 주세요!");
        }
        else
        {
            WaveManager.Instance.BattlePhaseStartEventHandler();
            SoundManager.Play2DSound(SoundName.StartFight);
            NoiseManager.Instance.SaveNoise();

            //IsBattlePhase�� true�϶��� ChangedArmy�� ������ �� �մµ�
            //�ڵ庸�ϱ� ���� �����ϰ� 0.1�� �ڿ� true���Ǳ淡 0.15�ʵڿ� ����ǰ���

            //�̷��Ը��� �켱���� ��Ʋ ��ŸƮ���� IsBattlePhase�� true�� ���� �Ǳ���
            CoroutineUtil.CallWaitForSeconds(0.15f, () => ArmyManager.Instance.ChangedCurrentArmy());


            OnOffButton();

            UIManager.Instance.HideAllPanel();
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
