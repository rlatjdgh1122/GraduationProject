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
        _resolutions.AddRange(Screen.resolutions); //사용 가능한 해상도 넣기
        _resolutionDropDown.options.Clear(); //기존 드랍 다운에 있는 옵션 제거

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

    private void GetResolutionIndex(int index) //해상도 받아오기
    {
        _resolutionIndex = index;

        ApplyScreen();
    }

    private void CheckScreenMode(int optionNumber) //화면 모드 받아오기
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

    private void ApplyScreen() //화면에 적용
    {
        Screen.SetResolution(_resolutions[_resolutionIndex].width, _resolutions[_resolutionIndex].height
                    , _screenMode);
    }
}