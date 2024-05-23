using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private float CutSceneTime = 0f;
    [SerializeField]
    private string NextScene;

    [SerializeField]
    private CanvasGroup _blackPanel;
    [SerializeField]
    private float _duration;

    private string _mainScene = "FSMTestMin";

    private void Update()
    {
        StartCoroutine(WaitCutSceneTime(CutSceneTime, NextScene));
    }

    IEnumerator WaitCutSceneTime(float waitCutSceneTime, string sceneName)
    {
        if(Input.anyKeyDown)
        {
            if(!Input.GetKeyDown(KeyCode.Escape) && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.RightAlt)) 
            {
                _blackPanel.DOFade(1, _duration).OnComplete(() => LoadingSceneController.LoadScene(_mainScene));
            }
        }

        yield return new WaitForSeconds(waitCutSceneTime);

        if(NextScene != _mainScene)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            LoadingSceneController.LoadScene(sceneName);
        }

    }
}
