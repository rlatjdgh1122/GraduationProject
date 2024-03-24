using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPinning : MonoBehaviour
{
    private CameraSystem camSystem = null;

    private Dictionary<KeyCode, Action> keyDictionary = new();
    private void Awake()
    {
        camSystem ??= GetComponent<CameraSystem>();
        KeySetting();
    }
    private void KeySetting()
    {
        keyDictionary = new Dictionary<KeyCode, Action>()
        {
             {KeyCode.F1, ()=> ChangeArmyFollowCam(1) },
             {KeyCode.F2, ()=> ChangeArmyFollowCam(2) },
             {KeyCode.F3, ()=> ChangeArmyFollowCam(3) },
             {KeyCode.F4, ()=> ChangeArmyFollowCam(4) },
             {KeyCode.F5, ()=> ChangeArmyFollowCam(5) },
             {KeyCode.F6, ()=> ChangeArmyFollowCam(6) },
             {KeyCode.F7, ()=> ChangeArmyFollowCam(7) },
             {KeyCode.F8, ()=> ChangeArmyFollowCam(8) },
             {KeyCode.F9, ()=> ChangeArmyFollowCam(9) },
            { KeyCode.Space, () => DefaultScreen()},
        };
    }


    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (var dic in keyDictionary)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }
    }
    //화면 기본위치로 고정
    private void DefaultScreen()
    {
        //camSystem.IsMoving = !camSystem.IsMoving;

        transform.position = new Vector3(0, transform.position.y, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        camSystem.CinemachineCam.transform.rotation = Quaternion.Euler(new Vector3(50, 0, 0));
    }

    private void ChangeArmyFollowCam(int legion)
    {

    }
}
