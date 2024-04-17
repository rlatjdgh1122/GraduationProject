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
        StartCoroutine(WwaitCutSceneTime(CutSceneTime, NextScene));
    }

    IEnumerator WwaitCutSceneTime(float waitCutSceneTime, string sceneName)
    {
        yield return new WaitForSeconds(waitCutSceneTime);
        SceneManager.LoadScene(sceneName);
    }
}
