using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ArmyManager : Singleton<ArmyManager>
{
    [SerializeField] private List<Army> armies;
    public List<Army> Armies { get { return armies; } }
    private Dictionary<KeyCode, Action> keyDictionary = new();

    private MovefocusMode curFocusMode = MovefocusMode.Command;
    public MovefocusMode CurFocusMode => curFocusMode;

    private int curArmyIdx = 0;
    public int CurLegion => curArmyIdx + 1;

    public int ArmiesCount => armies.Count;

    private void Start()
    {
        CreateArmy();
        SignalHub.OnBattleModeChanged?.Invoke(curFocusMode);

        KeySetting();
    }
    private void KeySetting()
    {
        keyDictionary = new Dictionary<KeyCode, Action>()
        {
             {KeyCode.Alpha1, ()=> ChangeArmy(1) },
             {KeyCode.Alpha2, ()=> ChangeArmy(2) },
             {KeyCode.Alpha3, ()=> ChangeArmy(3) },
             {KeyCode.Alpha4, ()=> ChangeArmy(4) },
             {KeyCode.Alpha5, ()=> ChangeArmy(5) },
             {KeyCode.Alpha6, ()=> ChangeArmy(6) },
             {KeyCode.Alpha7, ()=> ChangeArmy(7) },
             {KeyCode.Alpha8, ()=> ChangeArmy(8) },
             {KeyCode.Alpha9, ()=> ChangeArmy(9) },
             {KeyCode.A,      ()=>
             {
                 curFocusMode = curFocusMode == MovefocusMode.Command ?
                 MovefocusMode.Battle :  MovefocusMode.Command;
             SignalHub.OnBattleModeChanged?.Invoke(curFocusMode); }
            },
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

    /// <summary>
    /// 현재 선택된 Army를 리던
    /// </summary>
    /// <returns> Army를 리던</returns>
    public Army GetCurArmy() //현재 army 리턴
    {
        var idx = curArmyIdx < 0 ? 0 : curArmyIdx;
        return armies[idx];
    }

    public Army GetArmy(int legion)
    {
        return armies[legion - 1];
    }

    /// <summary>
    /// 군단 변경
    /// </summary>
    /// <param name="legion"> 몇번째 군단</param>
    private void ChangeArmy(int legion)
    {
        int Idx = legion - 1;
        if (curArmyIdx == Idx) return;
        if (armies.Count < legion) return;

        var prevIdx = curArmyIdx < 0 ? 0 : curArmyIdx;
        SignalHub.OnArmyChanged.Invoke(armies[prevIdx], armies[Idx]);

        curArmyIdx = Idx;

        armies.IdxExcept(
            Idx,
            p =>
            p.Soldiers.ForEach(s =>
            {
                CoroutineUtil.CallWaitForSeconds(.7f,
                    () => s.OutlineCompo.enabled = true,
                    () => s.OutlineCompo.enabled = false);

                s.HealthCompo.OnUIUpdate?.Invoke(s.HealthCompo.currentHealth, s.HealthCompo.maxHealth);
            })

           , p =>
           p.Soldiers.ForEach(s =>
           {
               s.OutlineCompo.enabled = false;
               s.HealthCompo.OffUIUpdate?.Invoke();
           }));
    }

    #region 스탯 부분
    /// <summary>
    /// 현재 선택된 군단의 스탯을 상승 및 감소 
    /// </summary>
    /// <param name="value"> 값(%)</param>
    /// <param name="type"> 값을 변경할 스탯타입 </param>
    /// <param name="mode"> 상승 또는 감소</param>
    public void AddStatCurAmry(int value, StatType type, StatMode mode)
    {
        armies[curArmyIdx].AddStat(armies[curArmyIdx], value, type, mode);
    }

    /// <summary>
    /// 현재 선택된 군단의 스탯을 삭제
    /// </summary>
    /// <param name="value"> 삭제할 값(단, 똑같은 값이 존재해야함)</param>
    /// <param name="type"> 값을 삭제할 스탯타입 </param>
    /// <param name="mode"> 상승 또는 감소</param>
    public void RemoveStatCurAmry(int value, StatType type, StatMode mode)
    {
        armies[curArmyIdx].RemoveStat(armies[curArmyIdx], value, type, mode);
    }

    /// <summary>
    /// 군단의 스탯을 상승 및 감소
    /// </summary>
    /// <param name="legion"> 몇번째 군단 </param>
    /// <param name="value"> 값(%)</param>
    /// <param name="type"> 값을 변경할 스탯타입</param>
    /// <param name="mode"> 상승 또는 감소</param>
    public void RemoveStat(int legion, int value, StatType type, StatMode mode)
    {
        armies[legion - 1].RemoveStat(armies[legion], value, type, mode);
    }
    /// <summary>
    /// 군단의 스탯을 삭제
    /// </summary>
    /// <param name="legion"> 몇번째 군단 </param>
    /// <param name="value"> 값(%)</param>
    /// <param name="type"> 값을 변경할 스탯타입</param>
    /// <param name="mode"> 상승 또는 감소</param>
    public void AddStat(int legion, int value, StatType type, StatMode mode)
    {
        armies[legion - 1].AddStat(armies[legion], value, type, mode);
    }

    #endregion

    #region 군단 영입 부분
    /// <summary>
    /// 장군을 제외한 펭귄을 군단에 넣는 함수
    /// </summary>
    /// <param name="legion"> 몇번째 군단</param>
    /// <param name="obj"> Penguin 타입만 가능</param>

    public void JoinArmyToSoldier(int legion, Penguin obj) //들어가고 싶은 군단, 군인펭귄
    {
        if (armies.Find(p => p.Legion == legion) == null)
        {
            Debug.Log("그런 군단 이름은 없습니다.");
            return;
        }

        var Army = armies[legion - 1];

        obj.SetOwner(Army);
        Army.Soldiers.Add(obj);
    }

    /// <summary>
    /// 장군펭귄을 군단에 넣는 함수
    /// </summary>
    /// <param name="legion"> 몇번째 군단</param>
    /// <param name="obj"> Penguin 타입만 가능</param>
    public void JoinArmyToGeneral(int legion, General obj) //들어가고 싶은 군단, 장군펭귄
    {
        if (armies.Find(p => p.Legion == legion) == null)
        {
            Debug.Log("그런 군단 이름은 없습니다.");
            return;
        }

        var Army = armies[legion - 1];
        var LegionStat = obj.ligeonStat;
        var followCam = Army.FollowCam;

        if (Army.General != null)
        {
            Debug.Log($"현재 {legion}군단에는 장군이 존재합니다.");
            return;
        }

        obj.SetOwner(Army);
        Army.General = obj;

        followCam.Obj.transform.parent = obj.transform;
        followCam.isInGeneral = true;

        Army.AddStat(Army, LegionStat);

    }
    #endregion

    #region 펭귄 및 군단 생성 부분

    /// <summary>
    /// 펭귄 지우기
    /// </summary>
    /// <param name="legion"> 몇번째 군단 *owner.Legion 입력*</param>
    /// <param name="obj"> Penguin 타입만 가능 *this 입력*</param>

    public void Remove(int legion, Penguin obj)
    {
        var soldiers = armies[legion - 1].Soldiers;
        soldiers.Remove(obj); //리스트에서 제외

    }

    /// <summary>
    /// 새로운 군단 생성
    /// </summary>
    public void CreateArmy()
    {
        Army newArmy = new Army();

        newArmy.Legion = ArmiesCount + 1;
        newArmy.IsCanReadyAttackInCurArmySoldiersList = true;

        GameObject followCam = new GameObject($"{newArmy.Legion}Legion_FollowCam");
        ArmyFollowCam armyFollowCam = new ArmyFollowCam();

        //위치 초기화
        followCam.transform.position = new Vector3(4.18f, 20f, 1.8f);

        armyFollowCam.Obj = followCam;
        newArmy.FollowCam = armyFollowCam;

        armies.Add(newArmy);
    }

    #endregion
}
