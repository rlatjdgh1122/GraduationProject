using System.Collections;
using UnityEngine;

public class PenguinSpawnHandler : MonoBehaviour
{
    private Coroutine _spawnDummyPenguinCorouine = null;

    private void Awake()
    {
        SignalHub.OnBattlePhaseStartEvent += DummyToPenguinSwapHandler;
        SignalHub.OnBattlePhaseEndEvent += PenguinToDummySwapHandler;
    }
    /// <summary>
    /// 전투 시작
    /// </summary>
    private void DummyToPenguinSwapHandler()
    {
        if (_spawnDummyPenguinCorouine != null)
            StopCoroutine(_spawnDummyPenguinCorouine);

        var belongDummyPenguinList = PenguinManager.Instance.BelongDummyPenguinList;
        var notBelongDummyPenguinList = PenguinManager.Instance.NotBelongDummyPenguinList;

        foreach (var dummy in belongDummyPenguinList)
        {
            var trm = dummy.transform;

            var penguin = PenguinManager.Instance.GetPenguinByDummyPenguin(dummy);

            //실제 펭귄에 대한 세팅을 해줌
            penguin.gameObject.SetActive(true);
            penguin.transform.position = trm.position;
            penguin.transform.rotation = trm.rotation;
            penguin.MousePos = GameManager.Instance.TentTrm.position;
            penguin.StateInit();


            //더미 펭귄은 꺼줌
            dummy.gameObject.SetActive(false);
        }

        //군단에 소속되지 않은 더미펭귄들은 집으로 감
        foreach (var dummy in notBelongDummyPenguinList)
        {
            dummy.ChangeNavqualityToNone();
            dummy.IsGoToHouse = true;
        }

    }

    /// <summary>
    /// 전투 끝
    /// </summary>
    private void PenguinToDummySwapHandler()
    {
        var soldierPenguinList = PenguinManager.Instance.SoldierPenguinList;

        foreach (var penguin in soldierPenguinList)
        {
            var trm = penguin.transform;
            var dummy = PenguinManager.Instance.GetDummyByPenguin(penguin);

            //더미 펭귄에 대한 세팅을 해줌
            dummy.gameObject.SetActive(true);
            dummy.IsGoToHouse = false;
            dummy.transform.position = trm.position;
            dummy.transform.rotation = trm.rotation;
            dummy.ChangeNavqualityToHigh();
            dummy.StateInit();
            //실제 펭귄은 꺼줌
            penguin.gameObject.SetActive(false);
        }



        //군단에 소속되지 않았던 애들은 여기서 처리 (순차적으로 생성)
        if (_spawnDummyPenguinCorouine != null)
            StopCoroutine(_spawnDummyPenguinCorouine);
        _spawnDummyPenguinCorouine = StartCoroutine(SpawnCorou());


    }

    private WaitForSeconds Heartbeat = new WaitForSeconds(0.2f);
    private IEnumerator SpawnCorou()
    {
        var dummyPenguinList = PenguinManager.Instance.NotBelongDummyPenguinList;
        int count = dummyPenguinList.Count;

        while (count-- > 0)
        {
            var dummy = dummyPenguinList[count];
            dummy.ChangeNavqualityToHigh();
            dummy.IsGoToHouse = false;

            dummy.gameObject.SetActive(true);
            dummy.DummyStateMachine.ChangeState(DummyPenguinStateEnum.Running);

            yield return Heartbeat;
        }
    }
    private void OnDestroy()
    {
        SignalHub.OnBattlePhaseStartEvent -= DummyToPenguinSwapHandler;
        SignalHub.OnBattlePhaseEndEvent -= PenguinToDummySwapHandler;
    }
}
