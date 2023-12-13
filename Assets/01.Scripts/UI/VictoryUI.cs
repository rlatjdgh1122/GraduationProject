using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _texts;

    public void SetTexts()
    {
        _texts[0].text = $"������ �Ʊ� : {GameManager.Instance.GetDeadPenguinCount()}����";
        _texts[1].text = $"������ �Ʊ� : {GameManager.Instance.GetPenguinCount}����";
        _texts[2].text = $"óġ�� ���� : {GameManager.Instance.GetDeadEnemyPenguinCount()}����";
    }
}
