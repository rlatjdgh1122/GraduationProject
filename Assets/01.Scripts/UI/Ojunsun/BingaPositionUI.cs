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
                // SignalPenguin�� Ž���� ������ ��ġ�� ������
                _bingaPos = _penguin.Target.transform;
            }

            // ������ ��ġ�� ȭ�� ��ǥ�� ��ȯ
            Vector3 pos = Camera.main.WorldToScreenPoint(_bingaPos.position);

            // ȭ�� ������ ���� ��ġ�� ������Ű�� ���� ������ ��
            return new Vector2(Mathf.Clamp(pos.x, 50f, Screen.width - 50f),
                               Mathf.Clamp(pos.y, 50f, Screen.height - 50f));
        }
    }

    private void LateUpdate()
    {
        //UI�� ���� ��ġ ����
        _bingaimage.gameObject.rectTransform().position = _bingaScreenPos;
    }
}
