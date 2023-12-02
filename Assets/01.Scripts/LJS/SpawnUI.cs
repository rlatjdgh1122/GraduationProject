using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class SpawnUI : MonoBehaviour
{
    [SerializeField] private ClickBuilding _clickBuilding;
    [SerializeField] private SpawnHero _spawnHero;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _coolText;
    [SerializeField] private Image img;
    private Transform _uiObj;

    private Sequence seq;

    private void Awake()
    {
        _uiObj = transform.Find("SpawnUI").GetComponent<Transform>();
        _clickBuilding.OnClickEvent += MoveUI;
        seq = DOTween.Sequence();
    }

    public void MoveUI(bool value)
    {
        if(value)
        {
            _uiObj.transform.DOMoveY(83, 0.4f);
        }
        else
        {
            _uiObj.transform.DOMoveY(-300, 0.3f);
        }
    }

    public void ClickHeroBtn()
    {
        _spawnHero.Spawn();
    }

    private void Update()
    {
        _text.text =
        _spawnHero.SpawnString();
        _coolText.text =
        _spawnHero.SpawnCoolString();
    }
}
