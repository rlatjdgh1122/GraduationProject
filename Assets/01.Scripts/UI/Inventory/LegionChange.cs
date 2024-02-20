using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LegionChange : MonoBehaviour
{
    [SerializeField] private float _changeTime = 0.2f;

    [SerializeField] private TextMeshProUGUI _curLegionNumberTex;
        
    [SerializeField] private Image _backPanel;
    [SerializeField] private Transform _legionNumberTrm;

    private CanvasGroup[] _legionButtons;

    private void Start()
    {
        _legionButtons = _legionNumberTrm.GetComponentsInChildren<CanvasGroup>();
    }

    public void ChangeCurrentLegionNumber(string text)
    {
        _curLegionNumberTex.text = $"{text} 군단";
    }

    public void SelectLegionNumber(int number)
    {
        string text = $"{number}";

        ChangeCurrentLegionNumber(text);

        SelectButton();
    }

    public void ChangeButton() //군단 바꾸는 버튼
    {
        UIManager.Instance.InitializeWarningTextSequence();

        _backPanel.DOFade(0.5f, _changeTime);

        for(int i = 0; i < LegionInventory.Instance.LegionList.Count; i++)
        {
            UIManager.Instance.WarningTextSequence
            .Append(_legionButtons[i].DOFade(1, _changeTime));

            Image obj = _legionButtons[i].transform.Find("Locked").GetComponent<Image>(); //LegionBtn 오브젝트 밑에 있는
            //Locked 오브젝트 찾기

            if (LegionInventory.Instance.LegionList[i].Locked) //군단이 잠겨있으면
            {
                _legionButtons[i].blocksRaycasts = false;


                obj.gameObject.SetActive(true);
            }
            else
            {
                _legionButtons[i].blocksRaycasts = true;

                obj.gameObject.SetActive(false);
            }
        }
    }

    private void SelectButton() //군단 지정 버튼
    {
        UIManager.Instance.InitializeWarningTextSequence();

        _backPanel.DOFade(0f, _changeTime);

        for (int i = _legionButtons.Length - 1; i >= 0; i--)
        {
            UIManager.Instance.WarningTextSequence
                .Append(_legionButtons[i].DOFade(0, _changeTime));
            _legionButtons[i].blocksRaycasts = false;
        }
    }
}