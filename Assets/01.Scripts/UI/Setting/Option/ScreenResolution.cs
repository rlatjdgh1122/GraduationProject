using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolution : MonoBehaviour
{
    private FullScreenMode _screenMode = FullScreenMode.FullScreenWindow;

    private TMP_Dropdown _screenModeDropDown;
    private TMP_Dropdown _resolutionDropDown;
    private List<Resolution> _resolutions = new();

    private int _resolutionIndex = 0;

    private void Awake()
    {
        _resolutionDropDown = transform.Find("ResolutionDropDown").GetComponent<TMP_Dropdown>();
        _screenModeDropDown = transform.Find("ScreenModeDropDown").GetComponent<TMP_Dropdown>();

        _resolutionDropDown.onValueChanged.RemoveAllListeners();
        _resolutionDropDown.onValueChanged.AddListener((optionNumber) => GetResolutionIndex(optionNumber));

        _screenModeDropDown.onValueChanged.RemoveAllListeners();
        _screenModeDropDown.onValueChanged.AddListener((optionNumber) => CheckScreenMode(optionNumber));

        ResolutionInit();
    }

    private void ResolutionInit()
    {
        _resolutions.AddRange(Screen.resolutions); //��� ������ �ػ� �ֱ�
        _resolutionDropDown.options.Clear(); //���� ��� �ٿ �ִ� �ɼ� ����

        _resolutions.Reverse();
        
        foreach (Resolution resolution in _resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();

            int hz = (int)Math.Round(resolution.refreshRateRatio.value);

            option.text = $"{resolution.width} x {resolution.height} {hz}hz";

            _resolutionDropDown.options.Add(option);
        }


        _resolutionDropDown.value = _resolutionIndex;

        _resolutionDropDown.RefreshShownValue();
    }

    private void GetResolutionIndex(int index) //�ػ� �޾ƿ���
    {
        _resolutionIndex = index;

        ApplyScreen();
    }

    private void CheckScreenMode(int optionNumber) //ȭ�� ��� �޾ƿ���
    {
        switch(optionNumber)
        {
            case 0:
                _screenMode = FullScreenMode.FullScreenWindow;
                break;

            case 1:
                _screenMode = FullScreenMode.Windowed;
                break;
        }

        ApplyScreen();
    }

    private void ApplyScreen() //ȭ�鿡 ����
    {
        Screen.SetResolution(_resolutions[_resolutionIndex].width, _resolutions[_resolutionIndex].height
                    , _screenMode);
    }
}