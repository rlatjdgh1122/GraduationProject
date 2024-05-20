using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

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
    CheckQuest
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

    public Sequence HudTextSequence;

    public void InitializHudTextSequence()
    {
        HudTextSequence?.Kill(); // ���� �������� �ߴ�
        HudTextSequence = DOTween.Sequence(); // ���ο� ������ �Ҵ�
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
                Debug.LogWarning($"�ߺ� Ű : {popupUI.name}");
            }
        }
    }

    #region popUI Logics
    public void ShowPanel(string uiName, bool isOverlap = false)
    {
        popupUIDictionary.TryGetValue(uiName, out PopupUI popupUI);

        if (!isOverlap)
        {
            if (!CheckShowAble(popupUI.UIGroup)) return;

            for (int i = 0; i < currentPopupUI.Count; i++)
            {
                PopupUI ui = currentPopupUI.Peek();

                if (ui.UIGroup != popupUI.UIGroup && popupUI.UIGroup != UIType.Gif)
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
        else
        {
            popupUI.ShowPanel();
            //_currentUI = popupUI;
        }
    }

    public bool CheckShowAble(UIType type)
    {
        try
        {
            Debug.Log($"_currentUI: {_currentUI}");
            Debug.Log($"_currentUI.Transferable.Contains(type): {_currentUI.Transferable.Contains(type)}");
            Debug.Log(type.ToString());
        }
        catch
        {

        }
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
            //    string name = currentPopupUI.Peek().name;

            //    bool isNotBattleResult = name != "DefeatUI" && name != "VictoryUI" &&
            //        name != "GifScreen" && name != "Masking";

            //    if (isNotBattleResult) //�¸� �� UI�� �й� �� UI�� ���� �� ���� ����
            //    {
            //        bool isbuildingPannel = name == "BuildingPanel" || name == "NexusPanel" || // ���߿� �����
            //            name == "Buff" || name == "Resource" || name == "Defense" || name == "Trap";

            //        if (isbuildingPannel)
            //        {
            //            HidePanel("NexusUI");
            //            ChangeCurrentUI();
            //            return;
            //        }

            //        currentPopupUI.Peek().HidePanel();
            //        ChangeCurrentUI();
            //    }
            //}
            if (currentPopupUI.Count <= 0)
            {
                if (popupUIDictionary.ContainsKey("EscUI"))
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
}
