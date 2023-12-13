using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _texts;

    public void SetTexts()
    {
        _texts[0].text = $"전사한 아군 : {GameManager.Instance.GetDeadPenguinCount()}마리";
        _texts[1].text = $"생존한 아군 : {GameManager.Instance.GetPenguinCount}마리";
        _texts[2].text = $"처치한 적군 : {GameManager.Instance.GetDeadEnemyPenguinCount()}마리";
    }
}
