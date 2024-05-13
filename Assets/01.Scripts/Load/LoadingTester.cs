using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingTester : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {
        LoadingSceneController.LoadScene("FSMTest");
    }
}
