using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum UIType
{
    Victory,
    Defeat,
    Resource,
    Nexus,
    General,
    Legion,
    Store,
    Info,
    Quest,
    Setting
}

public class UIManager : Singleton<UIManager>
{
    public Transform canvasTrm;

    private WarningUI _warningUI;

    public Dictionary<string, PopupUI> popupUIDictionary = new();
    //public Dictionary<string, WorldUI> worldUIDictionary = new Dictionary<string, WorldUI>();
    
    public Stack<PopupUI> currentPopupUI = new Stack<PopupUI>();

    //public Vector2 ScreenCenterVec = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    //public Vector2 offVec = new Vector2(Screen.width * 0.5f, -100f);

    private PopupUI _currentUI;

    public Sequence HudTextSequence;

    private Transform _maskingImage;
    private KeyValuePair<Transform, Button> _prevMaskingUiTrms; // trm, 부모

    public void InitializHudTextSequence()
    {
        HudTextSequence?.Kill(); // 기존 시퀀스를 중단
        HudTextSequence = DOTween.Sequence(); // 새로운 시퀀스 할당
    }

    public override void Awake()
    {
        _maskingImage = FindObjectOfType<MaskingImage>().transform;

        canvasTrm = GameObject.Find("Canvas").transform;
        _warningUI = FindObjectOfType<WarningUI>();

        PopupUI[] popupUIs = canvasTrm.GetComponentsInChildren<PopupUI>();
        //WorldUI[] worldUIs = FindObjectsOfType<WorldUI>();

        foreach (PopupUI popupUI in popupUIs)
        {
            if (!popupUIDictionary.ContainsKey(popupUI.name))
            {
                popupUIDictionary.Add(popupUI.name, popupUI);
            }
            else
            {
                Debug.LogWarning($"중복 키 : {popupUI.name}");
            }
        }
        //foreach (WorldUI worldUI in worldUIs)
        //{
        //    if(!worldUIDictionary.ContainsKey(worldUI.name))
        //    {
        //        worldUIDictionary.Add(worldUI.name, worldUI);
        //    }
        //    else
        //    {
        //        Debug.LogWarning("중복 키");
        //    }
        //}
    }

    #region popUI Logics
    public void ShowPanel(string uiName)
    {
        popupUIDictionary.TryGetValue(uiName, out PopupUI popupUI);

        if (!CheckShowAble(popupUI.UIGroup)) return;

        for (int i = 0; i < currentPopupUI.Count; i++)
        {
            PopupUI ui = currentPopupUI.Peek();

            if (ui.UIGroup != popupUI.UIGroup)
            {
                ui.HidePanel();
            }
        }

        if (popupUI != null)
        {
            popupUI.ShowPanel();
            _currentUI = popupUI;
        }
    }

    public bool CheckShowAble(UIType type)
    {
        return _currentUI == null || _currentUI.Transferable.Contains(type);
    }

    public void ChangeCurrentUI()
    {
        if (currentPopupUI.Count <= 0)
        {
            _currentUI = null;
        }
        else
        {
            _currentUI = currentPopupUI.Peek();
        }
    }

    public void HidePanel(string uiName)
    {
        popupUIDictionary.TryGetValue(uiName, out PopupUI popupUI);

        popupUI.HidePanel();

        ChangeCurrentUI();
    }

    public void MovePanel(string uiName, float x, float y, float fadeTime)
    {
        popupUIDictionary.TryGetValue(uiName, out PopupUI popupUI);
        popupUI.MovePanel(x, y, fadeTime);
    }

    public void ShowWarningUI(string text)
    {
        _warningUI.SetValue(text);
        _warningUI.ShowAndHidePanel(_warningUI.IntervalTime);
    }
    #endregion

    #region UI Function
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentPopupUI.Count > 0)
            {
                if (currentPopupUI.Peek().name != "DefeatUI" && currentPopupUI.Peek().name != "VictoryUI") //승리 시 UI와 패배 시 UI는 닫을 수 없게 설정
                {
                    currentPopupUI.Peek().HidePanel();
                    ChangeCurrentUI();
                }
            }
            else
            {
                if(popupUIDictionary.ContainsKey("EscUI"))
                    ShowPanel("EscUI");
            }
        }
    }
    #endregion


    public IEnumerator UIMoveDotCoroutine(RectTransform transform, Vector3 targetVec, float duration,
                                      Ease ease = Ease.Linear, params Action[] actions)
    {
        yield return transform.DOMove(targetVec, duration).SetEase(ease).WaitForCompletion();

        foreach (Action action in actions)
        {
            action?.Invoke();
        }
    }

    public void ChangeTextColorBoolean(TextMeshProUGUI text, bool value, Color beforeColor, Color afterColor, float dureation,
        Ease ease = Ease.Linear, params Action[] actions)
    {
        if (value)
        {
            text.DOColor(beforeColor, dureation).SetEase(ease).OnComplete(() =>
            {
                foreach (Action action in actions)
                {
                    action?.Invoke();
                }
            }); ;
        }
        else
        {
            text.DOColor(afterColor, dureation).SetEase(ease).OnComplete(() =>
            {
                foreach (Action action in actions)
                {
                    action?.Invoke();
                }
            }); ;
        }
    }
    public void ChangeTextColor(TextMeshProUGUI text, Color color, float dureation,
        Ease ease = Ease.Linear, params Action[] actions)
    {
        text.DOColor(color, dureation).SetEase(ease).OnComplete(() =>
        {
            foreach (Action action in actions)
            {
                action?.Invoke();
            }
        }); ;
    }

    public void SpawnHudText(TextMeshProUGUI text)
    {
        InitializHudTextSequence();
        HudTextSequence.Prepend(text.DOFade(1f, 0.5f));
        //.Join(text.rectTransform.DOMoveY(ScreenCenterVec.y, 0.5f))
        //.Append(text.DOFade(0f, 0.5f))
        //.Join(text.rectTransform.DOMoveY(ScreenCenterVec.y - 50f, 0.5f));
    }

    public void SetMaskingImagePos(Vector3 pos, Transform OnUI) // 귀찮으니 일단 이따구로 스레기처럼 함 
    {
        _maskingImage.parent.gameObject.SetActive(true);

        _prevMaskingUiTrms = new KeyValuePair<Transform, Button>(OnUI, OnUI.GetComponent<Button>());

        OnUI.transform.SetParent(_maskingImage.parent.Find("MaskingButtonTrm")); 

        _maskingImage.transform.position = pos;

        _prevMaskingUiTrms.Value.onClick.AddListener(OffMaskingImage);

    }   

    private void OffMaskingImage()
    {
        _prevMaskingUiTrms.Key.SetParent(_prevMaskingUiTrms.Key.parent);

        _prevMaskingUiTrms.Value.onClick.RemoveListener(OffMaskingImage);

        _maskingImage.parent.gameObject.SetActive(false);
    }
}
