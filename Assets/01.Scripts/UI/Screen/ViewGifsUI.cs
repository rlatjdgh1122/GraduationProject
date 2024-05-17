using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewGifsUI : PopupUI, ICreateSlotUI
{
    [Header("Init View Gif")]
    [SerializeField] 
    private GifViewButton _gifButtonPrefab;

    [SerializeField]
    private Transform _buttonParent;

    [SerializeField]
    protected GifScreenDataSO gifDataSO;

    public override void Awake()
    {
        base.Awake();

        CreateSlot();
    }

    public void CreateSlot()
    {
        foreach (var gifData in gifDataSO.GifScreenList)
        {
            GifViewButton button = Instantiate(_gifButtonPrefab);
            button.Init(gifData);
            button.transform.SetParent(_buttonParent);
        }
    }

    public void HideViewGifUI()
    {
        UIManager.Instance.HidePanel("ViewGifsUI");
    }
}
