using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossWarningUI : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private CanvasGroup _panel;

    private Coroutine corou = null;

    private void Awake()
    {
        _text = transform.Find("_WarningText").GetComponent<TextMeshProUGUI>();
        _panel = GetComponent<CanvasGroup>();
    }

    private float time = 0.35f;

    public void SetValue(string value)
    {
        _text.text = value;
    }

    public void Show()
    {
        if (corou != null)
        {
            StopCoroutine(corou);
        }
        corou = StartCoroutine(C());
    }

    private IEnumerator C()
    {
        _panel.DOFade(1, time);
        yield return new WaitForSeconds(time + 0.2f);
        _panel.DOFade(0, time);                  
        yield return new WaitForSeconds(time + 0.2f);
        _panel.DOFade(1, time);                  
        yield return new WaitForSeconds(time + 0.2f);
        _panel.DOFade(0, time);                  
        yield return new WaitForSeconds(time + 0.2f);
        _panel.DOFade(1, time);                  
        yield return new WaitForSeconds(time + 0.2f);
        _panel.DOFade(0, time);
    }


}
