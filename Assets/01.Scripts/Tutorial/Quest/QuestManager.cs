using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField]
    private QuestDataSO _questDataSO;

    private Dictionary<string, QuestData> _allQuests = new Dictionary<string, QuestData>();

    private List<QuestData> _curInprogressQuests = new List<QuestData>();

    private void Start()
    {
        _allQuests.Clear();

        for (int i = 0; i < _questDataSO.QuestDatas.Count; i++) //So�� �ִ� �� ��ųʸ��� �߰���.
        {
            _allQuests.Add(_questDataSO.QuestDatas[i].Id,
                           _questDataSO.QuestDatas[i]);
        }
    }

    public void StartQuest(string questId) //����Ʈ ����
    {
        QuestData questData = _allQuests[questId];

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

        if (questData.IsTutorialQuest)
        {
            if (TutorialManager.Instance.CurQuestIdx != questData.TutorialQuestIdx) //Ʃ�丮�� ����Ʈ�� ������� �� ������
            {
                Debug.Log($"��... �ʴ� ���� {TutorialManager.Instance.CurQuestIdx}��° Ʃ�丮�� ����Ʈ�� �ؾ��ϴµ�" +
                    $"{questData.TutorialQuestIdx}��° ����Ʈ�� {questData.Id}�� �Ϸ��� ���ݾ�;; ������");
                return;
            }
        }

        Debug.Log($"{questData.Id} ����Ʈ ������");

        _curInprogressQuests.Add(questData); //���� ������ ����Ʈ ����Ʈ�� �߰�
        InstantiateQuest(questData);

        _curInprogressQuests[questData.TutorialQuestIdx].isStartedQuest = true;

        SignalHub.OnStartQuestEvent?.Invoke(); //����Ʈ ���� �̺�Ʈ
    }

    private void InstantiateQuest(QuestData questData) //����Ʈ�� ������Ʈ�� �����ϴ� ����. ex: ���� 3�� �Դ� ����Ʈ�� �� ���̵��� ����Ʈ ������Ʈ 3���� ����
    {
        for (int i = 0; i < questData.RepetCount; i++)
        {
            GameObject questObj = Object.Instantiate<GameObject>(questData.QuestObj, transform);
            questObj.name = questData.Id;
        }
    }

    public void ProgressQuest(string questId) //����Ʈ�� ����Ǿ�����. ex: ������ �Ծ�����.
    {
        QuestData questData = _allQuests[questId];

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
            SignalHub.OnProgressQuestEvent?.Invoke(); //����Ʈ ���� �̺�Ʈ
        }
        catch
        {
            EndQuest(questData); //���� �� ���� ������Ʈ�� ������ ����Ʈ �Ϸ�
        }
    }

    public void EndQuest(QuestData questData)
    {
        Debug.Log($"{questData.Id} ����Ʈ ����");

        _curInprogressQuests[questData.TutorialQuestIdx].isFinQuest = true;     //����Ʈ�� �Ϸ�ó�� ���ְ�
        _curInprogressQuests[questData.TutorialQuestIdx].isStartedQuest = true; //����Ʈ�� �Ϸ�ó�� ���ְ�

        _curInprogressQuests.Remove(questData); //���� ������ ����Ʈ ����Ʈ���� ����

        if (questData.IsTutorialQuest) { TutorialManager.Instance.IncreaseQuestIdx(); } // Ʃ�丮�� ����Ʈ�� ������� �ؾ� �ϴϱ� idx ����
        
        SignalHub.OnEndQuestEvent?.Invoke(); //����Ʈ ���� �̺�Ʈ
    }

}
