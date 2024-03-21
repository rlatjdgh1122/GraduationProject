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
    public QuestInfoSO[] questPrerequisites; //����Ʈ �ϱ� ���� ��������

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    // Ʃ�丮�󿡼��� �� �� �� �����ѵ� ������ �ص�
    //[Header("Rewards")]
    //public int goldReward;
    //public int experienceReward; 

    // �� �ٲٸ� �����
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }


}
