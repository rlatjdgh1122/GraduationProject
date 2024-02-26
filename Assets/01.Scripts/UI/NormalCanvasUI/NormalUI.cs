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

    public virtual void EnableUI(float time, object obj)//UI를 띄울 때 사용하는 함수. DOFade로 서서히 활성화 하고 time으로 몇초동안 활성화할건지 정해준다.
    {
        _panel.raycastTarget = true;
        if (_cvg != null)
        {
            _cvg.interactable = true;
            _cvg.blocksRaycasts = true;
        }
    }
    
    public abstract void DisableUI(float time, Action action); //UI를 끌 때 사용하는 함수. DOFade로 서서히 비활성화 하고 마찬가지로 time으로 비활성화 시간 정해주는데, Action도 매개변수로 넘겨 비활성화 이후에 무엇을 할것인지 정해줄 수 있다.
}
