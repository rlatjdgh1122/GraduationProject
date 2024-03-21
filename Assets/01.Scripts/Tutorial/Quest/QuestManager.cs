using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField]
    private QuestDataSO _questDataSO;

    private Dictionary<QuestData, Quest> _allQuests = new Dictionary<QuestData, Quest>();

    private List<QuestData> _curInprogressQuests = new List<QuestData>();

    private void Start()
    {
        _allQuests.Clear();

        for (int i = 0; i < _questDataSO.QuestDatas.Count; i++)
        {
            _allQuests.Add(_questDataSO.QuestDatas[i],
                           _questDataSO.Quess[i]);
        }
    }

    public void StartQuest(QuestData questData)
    {
        if (questData.isStartedQuest)
        {
            Debug.Log($"{questData.Id}�� �̹� �������� ����Ʈ�� ������;;");
            return;
        }
        else if(questData.isFinQuest)
        {
            Debug.Log($"{questData.Id}�� �̹� ������ ����Ʈ�� ������;;");
            return;
        }
        
        Debug.Log($"{questData.Id} ����Ʈ ������");

        Quest quest = _allQuests[questData];
        _curInprogressQuests.Add(questData); //���� ����Ʈ ����Ʈ�� �߰�
        InstantiateQuest(questData);

        _curInprogressQuests[questData.TutorialQuestIdx].isStartedQuest = true;

        SignalHub.OnStartQuestEvent?.Invoke();
    }

    private void InstantiateQuest(QuestData questData)
    {
        for (int i = 0; i < questData.RepetCount; i++)
        {
            GameObject questObj = Object.Instantiate<GameObject>(questData.QuestObj, transform);
            questObj.name = questData.Id;
        }
    }

    public void ProgressQuest(QuestData questData)
    {
        if (questData.isFinQuest)
        {
            Debug.Log($"{questData.Id}�� �̹� ������ ����Ʈ�� ������;;");
            return;
        }

        Transform[] foundChildren = transform.GetComponentsInChildren<Transform>(true);
        Transform[] questObj = Array.FindAll(foundChildren, t => t.name == questData.Id && t != transform);

        try
        {
            Destroy(questObj[0].gameObject);
            Debug.Log($"���� ���� {questData.Id}����Ʈ {questObj.Length}���� �� ��");
        }
        catch
        {
            EndQuest(questData);
        }
    }

    public void EndQuest(QuestData questData)
    {
        Debug.Log($"{questData.Id} ����Ʈ ����");

        _curInprogressQuests[questData.TutorialQuestIdx].isFinQuest = true;
        _curInprogressQuests[questData.TutorialQuestIdx].isStartedQuest = true;

        _curInprogressQuests.Remove(questData);
        SignalHub.OnEndQuestEvent?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartQuest(_questDataSO.QuestDatas[0]);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ProgressQuest(_questDataSO.QuestDatas[0]);
        }
    }

}
