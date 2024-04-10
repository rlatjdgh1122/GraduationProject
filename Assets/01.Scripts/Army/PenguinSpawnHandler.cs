using UnityEngine;

public class PenguinSpawnHandler : MonoBehaviour
{
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
        var dummyPenguinList = PenguinManager.Instance.DummyPenguinList;

        foreach (var dummy in dummyPenguinList)
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
        foreach (var dummy in dummyPenguinList)
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
        var dummyPenguinList = PenguinManager.Instance.NotBelongDummyPenguinList;

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

        //���ܿ� �Ҽӵ��� �ʾҴ� �ֵ��� ���⼭ ó��

        //���������� �������ϴ� �ڵ尡 �ʿ�
    }
    private void OnDestroy()
    {
        SignalHub.OnBattlePhaseStartEvent -= DummyToPenguinSwapHandler;
        SignalHub.OnBattlePhaseEndEvent -= PenguinToDummySwapHandler;
    }
}
