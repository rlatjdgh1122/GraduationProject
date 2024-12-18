using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
    Setting,
    Gif,
    Credit,
    GifInfo,
    CheckQuest,
    GeneralPanel,
    None,
    Army,
}

public class UIManager : Singleton<UIManager>
{
    public Transform canvasTrm;

    [SerializeField]
    private GifScreenController _gifScreenController;
    public GifScreenController GifController => _gifScreenController;

    private WarningUI _warningUI;
    private BossWarningUI _bossWarningUI;

    public Dictionary<string, PopupUI> popupUIDictionary = new();
    //public Dictionary<string, WorldUI> worldUIDictionary = new Dictionary<string, WorldUI>();

    public Stack<PopupUI> currentPopupUI = new Stack<PopupUI>();

    //public Vector2 ScreenCenterVec = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    //public Vector2 offVec = new Vector2(Screen.width * 0.5f, -100f);

    private PopupUI _currentUI;
    public PopupUI CurrentUI => _currentUI;
    public bool IsEmptyUI => _currentUI == null;

    public Sequence HudTextSequence;

    public void InitializHudTextSequence()
    {
        HudTextSequence?.Kill(); // 기존 시퀀스를 중단
        HudTextSequence = DOTween.Sequence(); // 새로운 시퀀스 할당
    }

    public override void Awake()
    {
        canvasTrm = GameObject.Find("Canvas").transform;
        _warningUI = FindObjectOfType<WarningUI>();
        _bossWarningUI = FindObjectOfType<BossWarningUI>();

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
    }

    #region popUI Logics
    public void ShowPanel(string uiName)
    {
        popupUIDictionary.TryGetValue(uiName, out PopupUI popupUI);

        if (!CheckShowAble(popupUI.UIGroup)) return;

        for (int i = 0; i < currentPopupUI.Count; i++)
        {
            PopupUI ui = currentPopupUI.Peek();

            if (ui.UIGroup != popupUI.UIGroup || ui.name == popupUI.name)
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

    public void HideAllPanel()
    {
        if (currentPopupUI.Count <= 0) return;

        for(int i = 0; i < currentPopupUI.Count; i++)
        {
            currentPopupUI.TryPop(out var panel);
            panel.HidePanel();
        }

        ChangeCurrentUI();
    }

    public void ResetPanel()
    {
        HideAllPanel();
        _currentUI = null;
    }

    public T GetPopupUI<T>(string uiName) where T : PopupUI
    {
        if(popupUIDictionary.TryGetValue(uiName, out var popupUI))
        {
            return popupUI as T;
        }

        return null;
    }

    public void HidePanel(string uiName)
    {
        if (popupUIDictionary.TryGetValue(uiName, out PopupUI popupUI))
        {
            popupUI.HidePanel();
        }

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

    public void ShowBossWarningUI(string text)
    {
        _bossWarningUI.SetValue(text);
        _bossWarningUI.Show();
    }
    #endregion

    #region UI Function
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (currentPopupUI.Count > 0)
            //{
            //    if (currentPopupUI.Peek().name != "DefeatUI" && currentPopupUI.Peek().name != "VictoryUI"
            //        && currentPopupUI.Peek().name != "GifScreen") //승리 시 UI와 패배 시 UI는 닫을 수 없게 설정
            //    {
            //        currentPopupUI.Peek().HidePanel();
            //        ChangeCurrentUI();
            //    }
            //}
            //else
            {
                if (currentPopupUI.Count <= 0 && popupUIDictionary.ContainsKey("EscUI"))
                    ShowPanel("EscUI");
            }
        }

        //IsPointerOverUI();

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (currentPopupUI.Count <= 0)
        //    {
        //        if (popupUIDictionary.ContainsKey("EscUI"))
        //            ShowPanel("EscUI");
        //    }
        //    else if (currentPopupUI.Count > 0)
        //    {
        //        foreach (var popuis in currentPopupUI)
        //        {
        //            if (popuis.name == "Masking" || popuis.name == "GifScreen"  || popuis.name == "CreditUI") { return; }
        //        }

        //        string name = currentPopupUI.Peek().name;

        //        bool isNotBattleResult = name != "DefeatUI" && name != "VictoryUI" && name != "GifScreen";

        //        if (isNotBattleResult) //승리 시 UI와 패배 시 UI는 닫을 수 없게 설정
        //        {
        //            currentPopupUI.Peek().HidePanel();
        //            ChangeCurrentUI();
        //        }
        //    }
        //}
    }

    public bool IsPointerOverUI()
    {
        // 마우스 포인터가 UI 위에 있는지 확인
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        Debug.Log("I");

        foreach (var ray in results)
        {
            Debug.Log(ray.gameObject.name);
        }

        return results.Count > 0;
    }
    #endregion

    #region Alt tap
    private void OnApplicationFocus(bool hasFocus)
    {
        //if (isFirst) { isFirst = false; return; } //맨 처음 화면이 켜져있을 때 esc창이 나오는걸 방지
        //
        //if (currentPopupUI.TryPeek(out var result))
        //    if (result.name == "VictoryUI") return;
        //
        //if (hasFocus)
        //{
        //    _escButtonController?.ShowEscPanel();
        //}
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
}
