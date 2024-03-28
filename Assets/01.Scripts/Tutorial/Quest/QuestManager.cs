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
    public List<QuestData> CurInprogressQuests => _curInprogressQuests; //���� �������� ����Ʈ

    private DialogSystem _dialogSystem;
    private QuestUI _questUI;
    private CostUI _costUI;

    public override void Awake()
    {
        base.Awake();

        _dialogSystem = Object.FindAnyObjectByType<DialogSystem>();
        _questUI = Object.FindAnyObjectByType<QuestUI>();
        _costUI = Object.FindAnyObjectByType<CostUI>();
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

    public void SetCanStartQuest(string questId)
    {
        QuestData questData = _allQuests[questId];

        questData.QuestStateEnum = QuestState.CanStart;

        _questUI.CreateScrollViewUI(questData); //�ϴ��� ���⿡ �ٰ� ��. ����Ʈ UI�� ����Ʈ �߰��ϴ� �ڵ���

        if (questData.IsTutorialQuest)
        {
            StartTutorial(questId);
        }
    }

    public void StartTutorial(string questId)
    {
        QuestData questData = _allQuests[questId];

        if (TutorialManager.Instance.CurQuestIdx != questData.TutorialQuestIdx) //Ʃ�丮�� ����Ʈ�� ������� �� ������
        {
            Debug.Log($"��... �ʴ� ���� {TutorialManager.Instance.CurQuestIdx}��° Ʃ�丮�� ����Ʈ�� �ؾ��ϴµ�" +
                $"{questData.TutorialQuestIdx}��° ����Ʈ�� {questData.Id}�� �Ϸ��� ���ݾ�;; ������");
            return;
        }

        _dialogSystem.Begin(questData.TutorialTexts); //Ʃ�丮�� �ؽ�Ʈ �߰�
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
            case QuestState.Locked:
                Debug.Log($"{questData.Id}�� ���� ���ϴ� ����Ʈ�� ������;;");
                return;
        }

        Debug.Log($"{questData.Id} ����Ʈ ������");

        _curInprogressQuests.Add(questData); //���� ������ ����Ʈ ����Ʈ�� �߰�
        InstantiateQuest(questData);

        _curInprogressQuests[questData.TutorialQuestIdx].QuestStateEnum = QuestState.Running;

        _questUI.UpdatePopUpQuestUI(questData); // ����Ʈ ���� ������Ʈ
        SignalHub.OnStartQuestEvent?.Invoke(); //����Ʈ ���� �̺�Ʈ

        SignalHub.OnProgressQuestEvent += () => _questUI.UpdateQuestUIToProgress(_allQuests[questData.Id]);
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

        _allQuests[questData.Id].CurProgressCount++;

        switch (questData.QuestStateEnum)
        {
            case QuestState.Finish:
                Debug.Log($"{questData.Id}�� �̹� ������ ����Ʈ�� ������;;");
                return;
            default:
                if (questData.QuestStateEnum != QuestState.Running)
                {
                    Debug.Log("�̰� ���� ���� �� �ߴµ���");
                    return;
                }
                break;
        }

        Transform[] foundChildren = transform.GetComponentsInChildren<Transform>(true);
        Transform[] questObj = Array.FindAll(foundChildren, t => t.name == questData.Id && t != transform);

        if (questObj.Length > 1)
        {
            Destroy(questObj[0].gameObject);
            Debug.Log($"���� ���� {questData.Id}����Ʈ {questObj.Length}���� �� ��");
            SignalHub.OnProgressQuestEvent?.Invoke(); //����Ʈ ���� �̺�Ʈ
        }
        else
        {
            EndQuest(questData); //���� �� ���� ������Ʈ�� ������ ����Ʈ �Ϸ�
        }
    }

    public void EndQuest(QuestData questData)
    {
        SignalHub.OnProgressQuestEvent -= () => _questUI.UpdateQuestUIToProgress(_allQuests[questData.Id]);

        Debug.Log($"{questData.Id} ����Ʈ ����");

        _curInprogressQuests[questData.TutorialQuestIdx].QuestStateEnum = QuestState.Finish; // ����Ʈ�� �Ϸ�ó�� ���ְ�

        _curInprogressQuests.Remove(questData); //���� ������ ����Ʈ ����Ʈ���� ����

        if (questData.IsTutorialQuest) { TutorialManager.Instance.IncreaseQuestIdx(); } // Ʃ�丮�� ����Ʈ�� ������� �ؾ� �ϴϱ� idx ����

        _questUI.RemoveQuestContentUI(questData.Id); // ����Ʈ UI���� ����

        SignalHub.OnEndQuestEvent?.Invoke(); //����Ʈ ���� �̺�Ʈ

        _costUI.CostTween(questData.QuestRewardInfo.RewardCount, true, _questUI.QuestInfoUICompo.RewardPos.position);
        _questUI.QuestInfoUICompo.OffCanvasGroups();
    }

}
