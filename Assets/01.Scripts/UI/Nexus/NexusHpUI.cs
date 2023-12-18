using UnityEngine;
using UnityEngine.UI;
using Define.CamDefine;

public class NexusHpUI : MonoBehaviour
{
    #region ÄÄÆ÷³ÍÆ®
    private NexusBase _nexus;
    private Slider _hpSlider;
    private Camera _cam;
    #endregion

    private void Awake()
    {
        _nexus = GameObject.Find("Nexus").GetComponent<NexusBase>();
        _hpSlider = GetComponent<Slider>();
        _cam = Cam.MainCam;
    }

    private void Start()
    {
        _hpSlider.maxValue = _nexus.HealthCompo.currentHealth;
        _hpSlider.minValue = 0;
        _hpSlider.value = _nexus.HealthCompo.currentHealth;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }

    public void UpdateHealthBar(int value)
    {
        _hpSlider.value -= value;
    }
}
