using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPanel : MonoBehaviour
{
    private bool isOn = false;

    private Vector3 _onSpawnUIVec, _offSpawnUIVec;

    private void Start()
    {
        _offSpawnUIVec = gameObject.rectTransform().position;
        _onSpawnUIVec = _offSpawnUIVec + new Vector3(0, 320, 0); //´ëÃæ°ª
    }

    public void OnOffPanel()
    {
        if (isOn)
        {
            StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(gameObject.rectTransform(), _offSpawnUIVec, 0.7f, Ease.OutCubic));
        }
        else
        {
            StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(gameObject.rectTransform(), _onSpawnUIVec, 0.7f, Ease.OutCubic));
        }
        isOn = !isOn;
    }

    public void OnPanel()
    {
        StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(gameObject.rectTransform(), _onSpawnUIVec, 0.7f, Ease.OutCubic));
        isOn = true;
    }

    public void OffPanel()
    {
        StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(gameObject.rectTransform(), _offSpawnUIVec, 0.7f, Ease.OutCubic));
        isOn = false;
    }
}
