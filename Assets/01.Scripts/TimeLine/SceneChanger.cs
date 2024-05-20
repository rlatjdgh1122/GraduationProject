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

    private float startTime;

    private void Update()
    {
        StartCoroutine(WaitCutSceneTime(CutSceneTime, NextScene));
    }

    IEnumerator WaitCutSceneTime(float waitCutSceneTime, string sceneName)
    {
        yield return new WaitForSeconds(waitCutSceneTime);
        if(NextScene != "FSMTestMin")
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            LoadingSceneController.LoadScene(sceneName);
        }
    }
}
