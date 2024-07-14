using AssetKits.ParticleImage;
using SynergySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegionGeneralSlot : MonoBehaviour
{
    private Image _icon;
    public List<ParticleImage> _particleImg;
    public GameObject _panel;

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
        _icon.gameObject.SetActive(true);
        _icon.sprite = info.PenguinIcon;
    }

    public void StartParticles(SynergyType synergyType)
    {
        for (int i = 0; i < _particleImg.Count; i++)
        {
            _particleImg[i].Play();
        }
        StartCoroutine(PanelControl());
    }

    IEnumerator PanelControl()
    {
        _panel.SetActive(true);
        yield return new WaitForSeconds(8f);
        _panel.SetActive(false);
    }
}
