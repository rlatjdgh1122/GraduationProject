using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class TutorialArmyUseSkill : MonoBehaviour
{
    [SerializeField] private GameObject[] _lockObjs;

    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private TutorialController _controller;

    private bool _unlock = false;

    private bool _skillUse;
    private bool _ultimateUse;

    public void Unlock()
    {
        _lockObjs[0].SetActive(false);
        _lockObjs[1].SetActive(false);

        _canvasGroup.DOFade(1, 0.3f);

        _unlock = true;
    }

    private void Update()
    {
        if(_unlock )
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                _controller.TutorialInfoUI.CompleteSlot(_controller.CurrentTutorial(0, 2));
                _skillUse = true;

                if (_skillUse && _ultimateUse)
                {
                    TutorialM.Instance.AddTutorialIndex();
                }
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                _controller.TutorialInfoUI.CompleteSlot(_controller.CurrentTutorial(1, 2));
                _ultimateUse = true;

                if(_skillUse && _ultimateUse)
                {
                    TutorialM.Instance.AddTutorialIndex();
                    _unlock = false;
                }
            }
        }
    }
}