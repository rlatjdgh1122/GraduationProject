using AssetKits.ParticleImage.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : PopupUI
{
    private Transform _questPopupContentsParentTrm;

    private QuestInfoUI _questInfoUI;

    [SerializeField]
    private GameObject _questUIPrefabs;

    public override void Awake()
    {
        base.Awake();

        _questPopupContentsParentTrm = transform.Find("QuestList/PopUp/ScrollView/Viewport/Content").gameObject.transform;

        _questInfoUI = transform.Find("QuestList/QuestName/PopUp").GetComponent<QuestInfoUI>();
    }

    public void CreateScrollViewUI(QuestData questData)
    {
        ScrollViewQuestUI newQuest = Instantiate(_questUIPrefabs, _questPopupContentsParentTrm).GetComponent< ScrollViewQuestUI>();
        newQuest.SetUpScrollViewUI(questData.Id,
            () => UpdatePopUpQuestUI(questData));

    }

    public void UpdatePopUpQuestUI(QuestData questData)
    {
        _questInfoUI.UpdatePopUpQuestUI(questData.QuestStateEnum,
                                                  questData.Id,
                                                  questData.QuestUIDataInfo.QuestContentsInfo,
                                                  questData.QuestRewardInfo.RewardTypeImg,
                                                  questData.QuestRewardInfo.RewardCount);
    }

    private void RemoveQuestContentUI(GameObject removeUI)
    {
        Destroy(removeUI);
    }
}
