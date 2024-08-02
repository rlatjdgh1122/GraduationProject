using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTutorial : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] _arrow;
    [SerializeField] private Image _arrowIcon;

    [SerializeField] private CameraSystem _camSystem;

    [SerializeField] private List<Vector3> _arrowPosList = new();

    [SerializeField] private TutorialInfoUI _tutorialUI;
    [SerializeField] private TutorialController _armyTutorial;

    private bool _isPlaying = false;
    private int _index = 0;

    public void ShowCameraUI()
    {
        StartCoroutine(ShowUI());
    }

    private IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(2f);
        _arrow[0].DOFade(1, 0.25f);
        _isPlaying = true;
    }

    private void Update()
    {
        if (_isPlaying)
        {
            if (_index == 0 && _camSystem.transform.position.x < -20)
            {
                _index++;
                _arrow[0].transform.position = _arrowPosList[_index];
                _arrow[0].transform.rotation = Quaternion.Euler(0, 180, 180);
                _arrowIcon.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (_index == 1 && _camSystem.transform.position.x > 20)
            {
                _index++;
                _arrow[0].transform.position = _arrowPosList[_index];
                _arrow[0].transform.rotation = Quaternion.Euler(0, 0, -270);
                _arrowIcon.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (_index == 2 && _camSystem.transform.position.z > 20)
            {
                _index++;
                _arrow[0].transform.position = _arrowPosList[_index];
                _arrow[0].transform.rotation = Quaternion.Euler(0, 0, 270);
                _arrowIcon.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (_index == 3 && _camSystem.transform.position.z < -20)
            {
                _index++;
                _arrow[0].DOFade(0, 0.25f);
                _arrow[1].DOFade(1, 0.25f);
            }

            if (_index == 4 && _camSystem.transform.eulerAngles.y > 350f)
            {
                _index++;
                _arrow[1].DOFade(0, 0.25f);
                _arrow[2].DOFade(1, 0.25f);
            }

            if (_index == 5 && Camera.main.fieldOfView < 30f)
            {
                _isPlaying = false;

                _arrow[2].DOFade(0, 0.25f).OnComplete(() =>
                {
                    _tutorialUI.CompleteSlot(_armyTutorial.CurrentTutorial(0));
                    TutorialM.Instance.AddTutorialIndex();
                });
            }
        }
    }
}