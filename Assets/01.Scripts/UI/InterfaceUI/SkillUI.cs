using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [Header("UI Setting")]
    [SerializeField] private float _waitForSkillUIDown;
    [SerializeField] private float _localYPosDownValue;
    [SerializeField] private float _yPosMoveDuration;

    private Vector3 _startTrm;
    private Transform _skillUITrm;

    private Image _generalIcon;
    private Image _skillCoolTimeImage;

    private void Awake()
    {
        _skillUITrm = transform.Find("GeneralIconBK").GetComponent<Transform>();
        _startTrm = _skillUITrm.position;

        _generalIcon = _skillUITrm.transform.Find("GeneralIcon").GetComponent<Image>();
        _skillCoolTimeImage = _skillUITrm.transform.Find("CoolTimeUI").GetComponent<Image>();
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += OnBattleStartHandler;
        SignalHub.OnBattlePhaseEndEvent += OnBattleEndHandler;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= OnBattleStartHandler;
        SignalHub.OnBattlePhaseEndEvent -= OnBattleEndHandler;
    }

    private void OnBattleStartHandler()
    {
        UIManager.Instance.InitializHudTextSequence();

        UIManager.Instance.HudTextSequence
            .PrependInterval(_waitForSkillUIDown)
            .Append(_skillUITrm.DOLocalMoveY(_localYPosDownValue, _yPosMoveDuration));
    }

    private void OnBattleEndHandler()
    {
        UIManager.Instance.InitializHudTextSequence();

        UIManager.Instance.HudTextSequence
            .PrependInterval(_waitForSkillUIDown)
            .Append(_skillUITrm.DOLocalMoveY(_startTrm.y, _yPosMoveDuration));
    }

    public void ChangeLegion(Sprite generalIcon)
    {
        if (generalIcon == null)
        {
            _generalIcon.enabled = false;
        }
        else
        {
            _generalIcon.enabled = true;

            _generalIcon.sprite = generalIcon;
        }
    }

    public void UseSkill(float coolTime)
    {
        _skillCoolTimeImage.fillAmount = 1;

        _skillCoolTimeImage.DOFillAmount(0, coolTime);
    }
}