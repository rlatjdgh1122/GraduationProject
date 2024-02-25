using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class NormalUI : MonoBehaviour
{
    public UIType UIType;

    #region components
    protected Image _panel;
    protected CanvasGroup _cvg;
    #endregion

    public virtual void Awake()
    {
        _panel = GetComponent<Image>();
        _cvg = GetComponent<CanvasGroup>();

        _panel.raycastTarget = false;
        if (_cvg != null)
        {
            _cvg.interactable = false;
            _cvg.blocksRaycasts = false;
        }
    }

    public virtual void ExitButtonUI(float time = 0.5f)
    {
        DisableUI(time, null);
    }

    public virtual void EnableUI(float time, object obj)//UI�� ��� �� ����ϴ� �Լ�. DOFade�� ������ Ȱ��ȭ �ϰ� time���� ���ʵ��� Ȱ��ȭ�Ұ��� �����ش�.
    {
        _panel.raycastTarget = true;
        if (_cvg != null)
        {
            _cvg.interactable = true;
            _cvg.blocksRaycasts = true;
        }
    }
    
    public abstract void DisableUI(float time, Action action); //UI�� �� �� ����ϴ� �Լ�. DOFade�� ������ ��Ȱ��ȭ �ϰ� ���������� time���� ��Ȱ��ȭ �ð� �����ִµ�, Action�� �Ű������� �Ѱ� ��Ȱ��ȭ ���Ŀ� ������ �Ұ����� ������ �� �ִ�.
}
