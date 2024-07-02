using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUpgradeSlot : MonoBehaviour
{
    private CanvasGroup _lockPanel;

    private void Awake()
    {
        _lockPanel = transform.Find("Panel/LockPanel").GetComponent<CanvasGroup>();
    }

    public void OnUnlock()
    {
        _lockPanel.DOFade(0, 0.5f).OnComplete(() => _lockPanel.gameObject.SetActive(false));
    }
}
