using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Object = UnityEngine.Object;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField]
    private QuestDataSO _questDataSO;

    private Dictionary<string, QuestData> _allQuests = new Dictionary<string, QuestData>();

    private List<QuestData> _curInprogressQuests = new List<QuestData>();
    public List<QuestData> CurInprogressQuests => _curInprogressQuests; //���� �������� ����Ʈ

    private DialogSystem _dialogSystem;
    private QuestUI _questUI;

    public override void Awake()
    {
        base.Awake();

        _dialogSystem = Object.FindAnyObjectByType<DialogSystem>();
        _questUI = Object.FindAnyObjectByType<QuestUI>();
    }

    private void Start()
    {
        LoadQuestData();
    }

    private void LoadQuestData()
    {
        _allQuests.Clear();

        for (int i = 0; i < _questDataSO.QuestDatas.Count; i++) //So�� �ִ� �� ��ųʸ��� �߰���.
        {
            _allQuests.Add(_questDataSO.QuestDatas[i].Id,
                           new QuestData(_questDataSO.QuestDatas[i])); //So�ϱ� ���� ������ �����Ͱ� ����Ǹ� �� �ż� �����ڸ� ���� �� ���� ����
        }
    }

    public void StartQuest(string questId) //����Ʈ ����
    {
        QuestData questData = _allQuests[questId];

        switch (questData.QuestStateEnum)
        {
            case QuestState.Running:
                Debug.Log($"{questData.Id}�� �̹� �������� ����Ʈ�� ������;;");
                return;
            case QuestState.Finish:
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

            _dialogSystem.Begin(questData.TutorialTexts); //Ʃ�丮�� �ؽ�Ʈ �߰�
        }

        Debug.Log($"{questData.Id} ����Ʈ ������");

        _curInprogressQuests.Add(questData); //���� ������ ����Ʈ ����Ʈ�� �߰�
        InstantiateQuest(questData);

        _curInprogressQuests[questData.TutorialQuestIdx].QuestStateEnum = QuestState.Running;

        _questUI.CreateScrollViewUI(questData); //�ϴ��� ���⿡ �ٰ� ��. ����Ʈ UI�� ����Ʈ �߰��ϴ� �ڵ���
        SignalHub.OnStartQuestEvent?.Invoke(); //����Ʈ ���� �̺�Ʈ
    }

    private void InstantiateQuest(QuestData questData) //����Ʈ�� ������Ʈ�� �����ϴ� ����. ex: ���� 3�� �Դ� ����Ʈ�� �� ���̵��� ����Ʈ ������Ʈ 3���� ����
    {
        for (int i = 0; i < questData.RepeatCount; i++)
        {
            GameObject questObj = new GameObject(questData.Id);
            questObj.transform.SetParent(transform);
        }
    }

    public void ProgressQuest(string questId) //����Ʈ�� ����Ǿ�����. ex: ������ �Ծ�����.
    {
        QuestData questData = _allQuests[questId];

        switch (questData.QuestStateEnum)
        {
            case QuestState.Finish:
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

        _curInprogressQuests[questData.TutorialQuestIdx].QuestStateEnum = QuestState.Finish; // ����Ʈ�� �Ϸ�ó�� ���ְ�

        _curInprogressQuests.Remove(questData); //���� ������ ����Ʈ ����Ʈ���� ����

        if (questData.IsTutorialQuest) { TutorialManager.Instance.IncreaseQuestIdx(); } // Ʃ�丮�� ����Ʈ�� ������� �ؾ� �ϴϱ� idx ����

        _questUI.RemoveQuestContentUI(questData.Id);

        SignalHub.OnEndQuestEvent?.Invoke(); //����Ʈ ���� �̺�Ʈ
    }

}
