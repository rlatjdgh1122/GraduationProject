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

    private string _mainScene = "FSMTestMin";

    private void Update()
    {
        StartCoroutine(WaitCutSceneTime(CutSceneTime, NextScene));
    }

    IEnumerator WaitCutSceneTime(float waitCutSceneTime, string sceneName)
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            LoadingSceneController.LoadScene(_mainScene);
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
