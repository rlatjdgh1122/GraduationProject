using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TMP_Text txtSentence;
    public GameObject panel;

    Queue<string> sentences = new Queue<string>();

    public void Begin(Dialogue info)
    {
        panel.SetActive(true);
        sentences.Clear();

        foreach (var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }

        Next();
    }

    public void Next()
    {
        if (sentences.Count == 1)
        {
            End();
        }

        txtSentence.text = sentences.Dequeue();
    }

    private void End()
    {
        panel.SetActive(false);
    }
}
