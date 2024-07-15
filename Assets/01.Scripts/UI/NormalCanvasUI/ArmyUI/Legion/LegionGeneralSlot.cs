using AssetKits.ParticleImage;
using DG.Tweening;
using SynergySystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LegionGeneralSlot : MonoBehaviour
{
    private Image _icon;
    public List<ParticleImage> _particleImg;
    public TextMeshProUGUI _text;
    public GameObject _panel;

    private GeneralInfoDataSO _infoData;

    private void Awake()
    {
        _icon = transform.Find("Icon").GetComponent<Image>();
        _icon.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ArmyManager.Instance.OnSynergyEnableEvent += StartParticles;
    }

    private void OnDisable()
    {
        ArmyManager.Instance.OnSynergyEnableEvent -= StartParticles;
    }

    public void SetSlot(GeneralInfoDataSO info)
    {
        _infoData = info;

        _icon.gameObject.SetActive(true);
        _icon.sprite = info.PenguinIcon;
    }

    public void StartParticles(SynergyType type)
    {
        for (int i = 0; i < _particleImg.Count; i++)
        {
            _particleImg[i].Play();
        }

        StartCoroutine(PanelControl(type));
    }

    IEnumerator PanelControl(SynergyType type)
    {
        _panel.SetActive(true);
        yield return new WaitForSeconds(5f);

        if (_infoData != null && _infoData.SynergyType == type)
        {
            _text.DOFade(1f, 2f);
        }
        _panel.SetActive(false);
    }
}
