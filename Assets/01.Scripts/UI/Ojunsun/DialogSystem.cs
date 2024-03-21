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
    [SerializeField] GameObject dialog;

    private Tween fadeTween;

    private int dialCount = 1; //dialog가 켜진 횟수
    bool isTyping = false; //글자가 모두 출력되기 전에 스킵 하는 걸 막기 위한 변수
    bool canClick = true;

    Queue<string> sentences = new Queue<string>();

    public void Begin(Dialog info)
    {
        dialog.SetActive(true);
        FadeIn(2f);

        if (dialCount == 1)
        {
            foreach (var sentence in info.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }

        Next();
    }

    public void Next()
    {
        if(canClick)
        {
            if (isTyping)
                return;

            if (dialCount == 1 && sentences.Count == 3)
            {
                End();
                dialCount++;
            }

            if (dialCount == 2 && sentences.Count == 0)
            {
                End();
            }

            if (sentences.Count > 0)
            {
                string sentence = sentences.Dequeue();
                StartCoroutine(TypeSentence(sentence));
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        txtSentence.text = string.Empty;

        foreach (var letter in sentence)
        {
            txtSentence.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        isTyping = false;
    }

    private void End()
    {
        canClick = false;
        FadeOut(2f);
        StartCoroutine(FadeTime());
    }

    IEnumerator FadeTime()
    {
        yield return new WaitForSeconds(2f);
        dialog.SetActive(false);
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
