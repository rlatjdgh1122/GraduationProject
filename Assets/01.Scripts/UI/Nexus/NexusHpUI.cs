using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define.CamDefine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class NexusHpUI : MonoBehaviour
{
    #region ÄÄÆ÷³ÍÆ®
    private NexusBase _nexus;

    private Slider _hpSlider;
    #endregion

    private void Awake()
    {
        _nexus = GameObject.Find("Nexus").GetComponent<NexusBase>();
        _hpSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        _hpSlider.maxValue = _nexus.HealthCompo.currentHealth;
        _hpSlider.minValue = 0;
        _hpSlider.value = _nexus.HealthCompo.currentHealth;
    }

    private void Update()
    {
        transform.LookAt(-Cam.MainCam.transform.position);
    }

    public void UpdateHealthBar(int value)
    {
        _hpSlider.value -= value;
    }
}
