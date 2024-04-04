using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct QuestUIData
{
    #region PopUp

    public string QuestContentsInfo; // 어떤 퀘스트인지 설명하는 문자열
    public Sprite _questTypeIMG; // 퀘스트 완료 조건의 개수 (ex: 원거리적도 3마리 잡고, 근거리 적도 3마리 잡아야 하면 2)

    #endregion
}
