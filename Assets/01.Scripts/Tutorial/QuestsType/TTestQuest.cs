using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTestQuest : QuestStep
{
    private void Start()
    {
        ChangeState("");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState("");
            FinishQuest();
        }
    }

    protected override void SetQuestStepState(string state)
    {
        
    }

}
