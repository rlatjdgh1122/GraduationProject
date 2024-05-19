using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    [SerializeField] TMP_Text txtSentence;
    [SerializeField] private float _fadeValue;
    [SerializeField] private float defaultTippingSpeed;

    private float tippingSpeed;

    private Tween fadeTween;

    bool isTyping = false; //글자가 모두 출력되기 전에 스킵 하는 걸 막기 위한 변수
    bool canClick = true;

    Queue<string> sentences = new Queue<string>();

    private void Start()
    {
        tippingSpeed = defaultTippingSpeed;
    }

    public void Begin(string[] texts)
    {
        sentences.Clear();

        foreach (var sentence in texts)
        {
            sentences.Enqueue(sentence);
        }

        Next();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && isTyping && canClick)
        {
            tippingSpeed = 0;
        }
    }

    public void Next()
    {
        if (canClick)
        {
            FadeIn(_fadeValue);

            if (isTyping)
                return;

            tippingSpeed = defaultTippingSpeed;

            if (sentences.Count <= 0)
            {
                End();
                return;
            }

            string sentence = sentences.Dequeue();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        txtSentence.text = string.Empty;

        foreach (var letter in sentence)
        {
            txtSentence.text += letter;
            yield return new WaitForSeconds(tippingSpeed);
        }

        isTyping = false;
    }

    private void End()
    {
        canClick = false;
        FadeOut(_fadeValue);
        StartCoroutine(FadeTime());

        if (WaveManager.Instance.CurrentWaveCount % 2 == 1)
        {
            MaskingUIManager.Instance.SetMaskingImagePos();
        }

        if (WaveManager.Instance.CurrentWaveCount == 4)
        {
            UIManager.Instance.GifController.ShowGif(GifType.GeneralBuy);
        }
    }

    IEnumerator FadeTime()
    {
        yield return new WaitForSeconds(_fadeValue);
        canClick = true;
    }

    public void FadeIn(float duration)
    {
        Fade(1f, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }

    public void FadeOut(float duration)
    {
        Fade(0f, duration, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }

    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }
}
