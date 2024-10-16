using ArmySystem;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HealingController
{
    private Army _seletedArmy = null;
    private Vector3 _spawnStartPostion = Vector3.zero;
    private Vector3 _spawnEndPostion = Vector3.zero;
    private Transform _transform = null;

    private Collider[] _colls = new Collider[10]; //대충 10
    private float _detectionRange = 0f;
    private LayerMask _targetLayer;
    private int _checkCount = 0;

    public HealingController(Transform rootTrm, Vector3 startPos, Vector3 endPos, float DetectionRange)
    {
        _transform = rootTrm;
        _spawnStartPostion = startPos;
        _spawnEndPostion = endPos;
        _detectionRange = DetectionRange;

        _targetLayer = 1 << LayerMask.NameToLayer("Player");
    }

    public void SetArmy(Army army)
    {
        _seletedArmy = army;
    }


    public void GoToBuilding(Action afterAction)
    {
        _checkCount = 0;
        var penguinSet = new HashSet<GameObject>();

        ArmyManager.Instance.ChangedMoveFocusMode(_seletedArmy, MovefocusMode.MustMove);

        foreach (var penguin in _seletedArmy.AlivePenguins)
        {
            if (penguin.IgnoreToArmyCalled) //스킬쓰는 중이면
            {
                penguin.SetMustPostion(_transform.position);
            }
            else
            {
                penguin.MustMoveToTargetPostion(_transform.position);
            }

            penguinSet.Add(penguin.gameObject);
        }
        // 여기서 감지를 한다음
        // 감지에 들어온 애들은 오브젝트를 꺼주고
        // 다 들어왔으면 afterAction을 실행해줌


        CoroutineUtil.CallWaitForActionUntilTrue
            (
            () => _checkCount == _seletedArmy.AlivePenguins.Count, //이 될때까지
            () =>                                             // 반복해서 실행될 람다식
            {
                int count = Physics.OverlapSphereNonAlloc(_transform.position, _detectionRange, _colls, _targetLayer);

                for (int i = 0; i < count; i++)
                {
                    var col = _colls[i];
                    var obj = col.gameObject;

                    // check
                    if (penguinSet.Contains(obj))
                    {
                        if (obj.activeSelf)
                        {
                            obj.SetActive(false);
                            _checkCount++;
                        }
                    }//end if
                }
            },
            0.2f,                                            //를 주기로
            afterAction);                                     //조건이 만족하면 실행
    }


    /// <summary>
    /// 힐링이 끝나면 죽은 애들 다살림
    /// </summary>
    public void EndHealing()
    {
        var DeadLists = _seletedArmy.DeadPenguins.ToArray();

        foreach (var penguin in DeadLists)
        {
            penguin.OnResurrected();
            _seletedArmy.ResurrectPenguin(penguin);
        }

        var AliveLists = _seletedArmy.AlivePenguins.ToArray();

        foreach (var penguin in AliveLists)
        {
            penguin.HealthCompo.SetMaxHealth();
        }

        LeaveBuilding();
    }

    public void LeaveBuilding()
    {
        _checkCount = 0;

        var AliveLists = _seletedArmy.AlivePenguins.ToArray();
        ArmyManager.Instance.ChangedMoveFocusMode(_seletedArmy, MovefocusMode.Command);

        foreach (var penguin in AliveLists)
        {
            penguin.transform.position = _spawnStartPostion;
            penguin.OutlineCompo.enabled = false;
            penguin.gameObject.SetActive(true);
            penguin.IsMustMoving = false;
            penguin.StateInit();

            CoroutineUtil.CallWaitForSeconds(0.05f, () => { penguin.MoveToMySeat(_spawnEndPostion); });

        }
    }



}
