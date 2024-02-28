using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeUI : MonoBehaviour
{
    [HideInInspector] public PenguinStat GeneralStat;

    private RectTransform _imageTransform;
    private CanvasGroup _buttonGroup;

    public List<TreeRandomBoxUI> TreeBoxList;

    private void Awake()
    {
        _imageTransform = GetComponent<RectTransform>();
        _buttonGroup = transform.Find("canvasGroup").GetComponent<CanvasGroup>();
    }

    public void SetTree()
    {
        _imageTransform.DOSizeDelta(new Vector2(70, 8.3f), 1f).OnComplete(() =>
        {
            _buttonGroup.DOFade(1, 0.4f);
        });
    }

    public void SetRandom()
    {
        foreach (TreeRandomBoxUI treeRandomBoxUI in TreeBoxList)
        {
            treeRandomBoxUI.button.enabled = true;
            treeRandomBoxUI.SetRandom();
        }
    }

    public void CloseUnselectedBox()
    {
        TreeBoxList[0].button.enabled = false;
        TreeBoxList[0].canvasGroup.interactable = false;
        TreeBoxList[0].canvasGroup.DOFade(0, 0.5f);
    }
}
