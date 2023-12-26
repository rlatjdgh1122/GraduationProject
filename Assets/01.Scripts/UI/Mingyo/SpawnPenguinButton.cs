using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SpawnPenguinButton : MonoBehaviour
{
    private Button _btn;

    [SerializeField] private Image _coolingimg;
    [SerializeField] private float cooltime;
    [SerializeField] private Penguin spawnPenguin;
    [SerializeField] private PenguinFactory _penguinFactory;


    protected virtual void Awake()
    {
        _btn = GetComponent<Button>();
    }

    public void SpawnPenguinHandler()
    {
        if (WaveManager.Instance.RemainingPhaseReadyTime >= cooltime)
        {
            Legion.Instance.LegionUIList[0].HeroCnt++;

            ButtonCooldown();
        }
    }

    private void ButtonCooldown()
    {
        _btn.interactable = false;
        _coolingimg.fillAmount = 1f;

        DOTween.To(() => _coolingimg.fillAmount, f => _coolingimg.fillAmount = f, 0f, cooltime).OnComplete(() =>
        {
            _btn.interactable = true;
            _penguinFactory.SpawnPenguinHandler(spawnPenguin);
        });
    }
}
