using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDummyPengiun : DummyPenguin
{
    public GeneralStat Stat { get; set; } = null;

    private void OnMouseDown()
    {
        if (UIManager.Instance.CheckShowAble(UIType.Info)
            && !UIManager.Instance.GifController.CanShow
            && !LegionInventoryManager.Instance.CanUI
            && !NexusManager.Instance.CanClick
            && !TutorialManager.Instance.ShowTutorialQuest)
        {
            if (WaveManager.Instance.CurrentWaveCount <= 5)
            {
                UIManager.Instance.ShowWarningUI("튜토리얼이 진행되지 않았습니다");
                return;
            }

            PenguinManager.Instance.ShowGeneralInfoUI(this);
            OutlineCompo.enabled = true;
        }
    }
}