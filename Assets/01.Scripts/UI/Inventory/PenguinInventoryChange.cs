using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PenguinInventoryChange : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _curInventoryName;

    public void OnClickButton(string name)
    {
        _curInventoryName.text = name;
    }
}