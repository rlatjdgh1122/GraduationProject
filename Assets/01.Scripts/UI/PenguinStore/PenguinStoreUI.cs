using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PenguinStoreUI : InitSpawnPenguinUI
{
    private void Start()
    {
        SignalHub.OnLockButtonEvent += LockButton;
    }

    public void LockButton()
    {
        foreach(var slot in spawnButtonList)
        {
            if (slot.InfoData.PenguinType == PenguinTypeEnum.Wizard) continue;

            if (WaveManager.Instance.CurrentWaveCount == 2)
            {
                if (slot.InfoData.PenguinType != PenguinTypeEnum.Archer)
                {
                    slot.IsWaveLock = true;
                }
            }
            else
            {
                slot.IsWaveLock = false;
            }

            slot.LockedButtonInEndWave();
        }
    }

    public void PenguinInformataion(DummyPenguin dummyPenguin, EntityInfoDataSO infoData, int price)
    {
        BuyPanel.PenguinInformataion(dummyPenguin, infoData, price);
        infoPanel.PenguinInformataion(infoData);
    }

    public void UnlockSlot(PenguinTypeEnum unLockType) //펭귄 잠금 해제
    {
        if (lockButtonDicntionary.TryGetValue(unLockType, out SpawnPenguinButton btn))
        {
            btn.UnLockedButton();
            unitInventory.UnLockSlot(unLockType);
            UnLockedPenguin(unLockType);
        }
    }

    private void UnLockedPenguin(PenguinTypeEnum unLockType)
    {
        if (penguinInfoDictionary.TryGetValue(unLockType, out PenguinInfoDataSO data))
        {
            unlockedPenguinPanel.UnLocked(data);
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
        UIManager.Instance.ShowPanel("BuyPanel", true);
    }

    public void OnDisableBuyPanel()//구매 패널 비활성화
    {
        UIManager.Instance.HidePanel("BuyPanel");
    }

    public void OnEnablePenguinInfo() //펭귄 정보 활성화
    {
        UIManager.Instance.ShowPanel("DetailInfoPanel", true);
    }

    public void OnDisablePenguinInfo() //펭귄 정보 비활성화
    {
        UIManager.Instance.HidePanel("DetailInfoPanel");
    }
}
