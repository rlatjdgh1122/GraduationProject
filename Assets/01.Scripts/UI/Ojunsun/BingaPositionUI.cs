using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BingaPositionUI : MonoBehaviour
{
    private Transform _bingaPos;
    private SignalTowerPenguin _penguin;
    private Image _bingaimage;

    private void Awake()
    {
        _penguin = GameObject.FindObjectOfType<SignalTowerPenguin>();
        _bingaimage = transform.GetChild(0).GetComponent<Image>();
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += BattleStart;
    }

    private void BattleStart()
    {
        _bingaimage.gameObject.SetActive(true);
        StartCoroutine(ActiveTrue());
    }

    IEnumerator ActiveTrue()
    {
        yield return new WaitForSeconds(5f);
        _bingaimage.gameObject.SetActive(false);
    }

    private Vector3 _bingaScreenPos
    {
        get
        {
            if (_bingaPos == null)
            {
                // SignalPenguin이 탐지한 빙하의 위치를 가져옴
                _bingaPos = _penguin.Target.transform;
            }

            // 빙하의 위치를 화면 좌표로 변환
            Vector3 pos = Camera.main.WorldToScreenPoint(_bingaPos.position);

            // 화면 내에서 빙하 위치를 유지시키기 위해 제한을 둠
            return new Vector2(Mathf.Clamp(pos.x, 50f, Screen.width - 50f),
                               Mathf.Clamp(pos.y, 50f, Screen.height - 50f));
        }
    }

    private void LateUpdate()
    {
        //UI상에 빙하 위치 설정
        _bingaimage.gameObject.rectTransform().position = _bingaScreenPos;
    }
}
