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
        _curLegionNumberTex.text = $"{text} ����";
    }

    public void SelectLegionNumber(int number)
    {
        if (LegionInventory.Instance.LegionList[number - 1].Locked)
        {

        }
        else
        {
            string text = $"{number}";

            ChangeCurrentLegionNumber(text);

            SelectButton();
        }
    }

    public void ChangeButton() //���� �ٲٴ� ��ư
    {
        _backPanel.DOFade(0.5f, _changeTime);

        for (int i = 0; i < LegionInventory.Instance.LegionList.Count; i++)
        {
            _legionButtons[i].blocksRaycasts = true;

            _legionButtons[i].DOFade(1, _changeTime);

            Image obj = _legionButtons[i].transform.Find("Locked").GetComponent<Image>(); //LegionBtn ������Ʈ �ؿ� �ִ�
            //Locked ������Ʈ ã��

            if (LegionInventory.Instance.LegionList[i].Locked) //������ ���������
            {
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

    private void SelectButton() //���� ���� ��ư
    {
        _backPanel.DOFade(0f, _changeTime);

        for (int i = _legionButtons.Length - 1; i >= 0; i--)
        {
            _legionButtons[i].DOFade(0, _changeTime);
            _legionButtons[i].blocksRaycasts = false;
        }
    }

    //private void 
}