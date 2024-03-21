using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Quest")]
public class QuestDataSO : ScriptableObject
{
    public List<QuestData> QuestDatas = new List<QuestData>();
    public List<Quest> Quess = new List<Quest>();
}
