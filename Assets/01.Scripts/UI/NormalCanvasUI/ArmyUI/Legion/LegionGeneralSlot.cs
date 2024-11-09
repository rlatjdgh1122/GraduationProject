using ArmySystem;
using AssetKits.ParticleImage;
using Define.Resources;
using DG.Tweening;
using SynergySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LegionGeneralSlot : MonoBehaviour
{
    [SerializeField] private LegionGeneralSelectPanel _generalPanel;

    private Image _icon;
    public List<ParticleImage> _particleImg;
    public TextMeshProUGUI _text;
    public GameObject _panel;
    public Button button;
    private Button _button;

    private GeneralInfoDataSO _infoData;
    private Sprite _skullIcon = null;

    private string _legionName = string.Empty;
    private LegionPanel _parentPanel;

    private void OnEnable()
    {
        _generalPanel.SetGeneralEvent += SetGeneral;
        SignalHub.OnSynergyEnableEvent += StartParticles;
        SignalHub.OnBattlePhaseEndEvent += OnBattleEndEventHandler;
    }

    private void OnDisable()
    {
        _generalPanel.SetGeneralEvent -= SetGeneral;
        SignalHub.OnSynergyEnableEvent -= StartParticles;
        SignalHub.OnBattlePhaseEndEvent -= OnBattleEndEventHandler;
    }
    private void Awake()
    {
        _icon = transform.Find("Icon").GetComponent<Image>();
        _icon.gameObject.SetActive(false);

        button = GetComponent<Button>();
        _parentPanel = ExtensionMethod.FindParent<LegionPanel>(transform.gameObject);

        _button = transform.Find("Button").GetComponent<Button>();
        _button.onClick.AddListener(UpdateSlot);
        _button.gameObject.SetActive(false);
    }

    private void Start()
    {
        _skullIcon = VResources.Load<Sprite>("Skull");
    }

    private void SetGeneral()
    {
        _button.gameObject.SetActive(true);
    }

    private void UpdateSlot()
    {
        if (_generalPanel.GeneralPenguin != null && !_generalPanel.GeneralPenguin.HealthCompo.IsDead) return;

        _parentPanel.Heal(_generalPanel.GeneralPenguin, _infoData, OnBattleEndEventHandler);
    }

    private void OnBattleEndEventHandler()
    {
        if (_infoData)
        {
            Army army = ArmyManager.Instance.GetArmyByLegionName(_legionName);

            if (army.IsGeneral)
            {
                _icon.sprite = _infoData.PenguinIcon;
            }
            else
            {
                _icon.sprite = _skullIcon;
            }
        }
    }


    public void SetSlot(GeneralInfoDataSO info, string legionName)
    {
        _infoData = info;
        _legionName = legionName;

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
