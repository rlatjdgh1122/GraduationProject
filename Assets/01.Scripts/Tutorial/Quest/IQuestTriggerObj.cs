using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestTriggerObj
{
    public string[] QuestIds { get; set; }
    public bool isRunning { get; set; }
}
