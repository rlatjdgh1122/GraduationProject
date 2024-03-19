using UnityEngine;
using UnityEngine.UI;
using Define.CamDefine;

public class NexusHpUI : WorldUI
{
    #region ÄÄÆ÷³ÍÆ®
    private NexusBase _nexus;
    private Slider _hpSlider;
    #endregion

    public override void Awake()
    {
        base.Awake();

        _nexus = GameObject.Find("Nexus").GetComponent<NexusBase>();
        _hpSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        _hpSlider.maxValue = _nexus.HealthCompo.currentHealth;
        _hpSlider.minValue = 0;
        _hpSlider.value = _nexus.HealthCompo.currentHealth;
    }

    public override void Update()
    {
        base.Update();

        //transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }

    public void UpdateHealthBar(int value)
    {
        _hpSlider.value -= value;
    }
}
