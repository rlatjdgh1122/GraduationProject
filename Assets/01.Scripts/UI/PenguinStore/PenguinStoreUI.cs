using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PenguinStoreUI : InitSpawnPenguinUI
{
    public void PenguinInformataion(DummyPenguin dummyPenguin, EntityInfoDataSO infoData, int price)
    {
        BuyPanel.PenguinInformataion(dummyPenguin, infoData, price);
        infoPanel.PenguinInformataion(dummyPenguin, infoData);
    }

    public void UnlockSlot(PenguinTypeEnum unLockType)
    {
        if (lockButtonDicntionary.TryGetValue(unLockType, out SpawnPenguinButton btn))
        {
            btn.UnLockedButton();
        }
    }

    public void OnEnableStorePanel() //스토어 패널 활성화
    {
        UIManager.Instance.ShowPanel("StorePanel");
    }

    public void OnDisableStorePanel()//스토어 패널 비활성화
    {
        UIManager.Instance.HidePanel("StorePanel");
    }

    public void OnEnableBuyPanel() //구매 패널 활성화
    {
        UIManager.Instance.ShowPanel("BuyPanel");
    }

    public void OnDisableBuyPanel()//구매 패널 비활성화
    {
        UIManager.Instance.HidePanel("BuyPanel");
    }

    public void OnEnablePenguinInfo() //펭귄 정보 활성화
    {
        UIManager.Instance.ShowPanel("DetailInfoPanel");
    }

    public void OnDisablePenguinInfo() //펭귄 정보 비활성화
    {
        UIManager.Instance.HidePanel("DetailInfoPanel");
    }
}
