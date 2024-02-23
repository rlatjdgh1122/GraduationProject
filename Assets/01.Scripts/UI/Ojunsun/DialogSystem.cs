using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public TMP_Text txtSentence;
    public GameObject dialog;
    private int dialCount = 1; //dialog�� ���� Ƚ��
    bool isTyping = false; //���ڰ� ��� ��µǱ� ���� ��ŵ �ϴ� �� ���� ���� ����

    Queue<string> sentences = new Queue<string>();

    public void Begin(Dialog info)
    {
        dialog.SetActive(true);

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
        if (isTyping)
            return;

        if (dialCount == 1 && sentences.Count == 2) 
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
        dialog.SetActive(false);
    }
}
