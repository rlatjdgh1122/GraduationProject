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
        #region 전시 끝나면 무조건 지워야 하는 하드 코딩
        if (WaveManager.Instance.CurrentWaveCount == 1)
        {
            if (LegionInventoryManager.Instance.BuyBasicPenguinCountIn1Wave < 3)
            {
                UIManager.Instance.ShowWarningUI($"수박바 펭귄을 {3 - LegionInventoryManager.Instance.BuyBasicPenguinCountIn1Wave}마리 더 구매해 주세요");
                return;
            }

            if (LegionInventoryManager.Instance.AddLegionBasicPenguinCountIn1Wave < 3)
            {
                UIManager.Instance.ShowWarningUI($"수박바 펭귄을 군단에 {3 - LegionInventoryManager.Instance.AddLegionBasicPenguinCountIn1Wave}마리 더 추가해 주세요");
                return;
            }
        }

        if (WaveManager.Instance.CurrentWaveCount == 2)
        {
            if(LegionInventoryManager.Instance.BuyArcherPenguinCountIn2Wave < 2)
            {
                UIManager.Instance.ShowWarningUI($"아처 펭귄을 구매해 주세요");
                return;
            }

            if(LegionInventoryManager.Instance.AddLegionArcherPenguinCountIn2Wave < 2)
            {
                UIManager.Instance.ShowWarningUI($"아처 펭귄을 군단에 {2 - LegionInventoryManager.Instance.AddLegionArcherPenguinCountIn2Wave}마리 더 추가해 주세요");
                return;
            }
        }

        if(WaveManager.Instance.CurrentWaveCount == 3 && !WaveManager.Instance.OnBuildBuffTower)
        {
            UIManager.Instance.ShowWarningUI("힘의 조각상을 건설해 주세요");
            return;
        }

        if (WaveManager.Instance.CurrentWaveCount == 4 && !WaveManager.Instance.OnBuildArcherTower)
        {
            UIManager.Instance.ShowWarningUI("아처 타워를 건설해 주세요");
            return;
        }

#endregion

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
