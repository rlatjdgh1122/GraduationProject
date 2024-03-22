using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField]
    private string QuestId;

    public void StartQuest()
    {
        QuestManager.Instance.StartQuest(QuestId);
    }

    private void Update() //�׽�Ʈ
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            QuestManager.Instance.StartQuest(QuestId);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            QuestManager.Instance.ProgressQuest(QuestId);
        }
    }
}
