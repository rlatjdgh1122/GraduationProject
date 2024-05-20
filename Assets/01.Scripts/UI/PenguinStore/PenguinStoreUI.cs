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

    public void UnlockSlot(PenguinTypeEnum unLockType) //��� ��� ����
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

    public void OnEnableStorePanel() //����� �г� Ȱ��ȭ
    {
        UIManager.Instance.ShowPanel("StorePanel");
    }

    public void OnDisableStorePanel()//����� �г� ��Ȱ��ȭ
    {
        UIManager.Instance.HidePanel("StorePanel");
    }

    public void OnEnableBuyPanel() //���� �г� Ȱ��ȭ
    {
        UIManager.Instance.ShowPanel("BuyPanel", true);
    }

    public void OnDisableBuyPanel()//���� �г� ��Ȱ��ȭ
    {
        UIManager.Instance.HidePanel("BuyPanel");
    }

    public void OnEnablePenguinInfo() //��� ���� Ȱ��ȭ
    {
        UIManager.Instance.ShowPanel("DetailInfoPanel", true);
    }

    public void OnDisablePenguinInfo() //��� ���� ��Ȱ��ȭ
    {
        UIManager.Instance.HidePanel("DetailInfoPanel");
    }
}
