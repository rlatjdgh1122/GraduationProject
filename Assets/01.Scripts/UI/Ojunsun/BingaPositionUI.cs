using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BingaPositionUI : MonoBehaviour
{
    private Transform _bingaPos;
    private SignalTowerPenguin _penguin;
    private Image _bingaimage;
    private Image _bingaimage2;

    private void Awake()
    {
        _penguin = GameObject.FindObjectOfType<SignalTowerPenguin>();
        _bingaimage = transform.GetChild(0).GetComponent<Image>();
        _bingaimage2 = transform.GetChild(1).GetComponent<Image>();
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += BattleStart;
    }

    private void BattleStart()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        _bingaimage.DOFade(1f, 2f);
        _bingaimage2.DOFade(1f, 2f);
        yield return new WaitForSeconds(4f);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        _bingaimage.DOFade(0f, 2f);
        _bingaimage2.DOFade(0f, 2f);
        yield return new WaitForSeconds(2f);
    }

    private Vector3 _bingaScreenPos
    {
        get
        {
            if (_bingaPos == null)
            {
                // SignalPenguin�� Ž���� ������ ��ġ�� ������
                _bingaPos = _penguin.Target[_penguin.targetCnt].transform;
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
        //���� UI�� ������ġ�� ����
        _bingaimage.gameObject.rectTransform().position = _bingaScreenPos;
        _bingaimage2.gameObject.rectTransform().position = _bingaScreenPos;
    }
}
