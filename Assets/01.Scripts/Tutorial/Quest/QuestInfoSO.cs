using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO")]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField]
    public string id { get; private set; }

    [field: SerializeField]
    public int QuestIDX { get; private set; }

    [Header("Requirements")]
    public QuestInfoSO[] questPrerequisites; //퀘스트 하기 위한 전제조건

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    // 튜토리얼에서는 안 쓸 것 같긴한데 구현은 해둠
    //[Header("Rewards")]
    //public int goldReward;
    //public int experienceReward; 

    // 값 바꾸면 실행됨
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }


}
