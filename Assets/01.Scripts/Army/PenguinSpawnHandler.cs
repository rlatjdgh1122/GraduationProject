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
    /// ���� ����
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

            //���� ��Ͽ� ���� ������ ����
            penguin.gameObject.SetActive(true);
            penguin.transform.position = trm.position;
            penguin.transform.rotation = trm.rotation;
            penguin.MousePos = GameManager.Instance.TentTrm.position;
            penguin.StateInit();


            //���� ����� ����
            dummy.gameObject.SetActive(false);
        }

        //���ܿ� �Ҽӵ��� ���� ������ϵ��� ������ ��
        foreach (var dummy in notBelongDummyPenguinList)
        {
            dummy.ChangeNavqualityToNone();
            dummy.IsGoToHouse = true;
        }

    }

    /// <summary>
    /// ���� ��
    /// </summary>
    private void PenguinToDummySwapHandler()
    {
        var soldierPenguinList = PenguinManager.Instance.SoldierPenguinList;

        foreach (var penguin in soldierPenguinList)
        {
            var trm = penguin.transform;
            var dummy = PenguinManager.Instance.GetDummyByPenguin(penguin);

            //���� ��Ͽ� ���� ������ ����
            dummy.gameObject.SetActive(true);
            dummy.IsGoToHouse = false;
            dummy.transform.position = trm.position;
            dummy.transform.rotation = trm.rotation;
            dummy.ChangeNavqualityToHigh();
            dummy.StateInit();
            //���� ����� ����
            penguin.gameObject.SetActive(false);
        }



        //���ܿ� �Ҽӵ��� �ʾҴ� �ֵ��� ���⼭ ó�� (���������� ����)
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
