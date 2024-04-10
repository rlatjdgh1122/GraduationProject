using UnityEngine;

public class PenguinSpawnHandler : MonoBehaviour
{
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
        var dummyPenguinList = PenguinManager.Instance.DummyPenguinList;

        foreach (var dummy in dummyPenguinList)
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
        foreach (var dummy in dummyPenguinList)
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
        var dummyPenguinList = PenguinManager.Instance.NotBelongDummyPenguinList;

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

        //군단에 소속되지 않았던 애들은 여기서 처리

        //순차적으로 나오게하는 코드가 필요
    }
    private void OnDestroy()
    {
        SignalHub.OnBattlePhaseStartEvent -= DummyToPenguinSwapHandler;
        SignalHub.OnBattlePhaseEndEvent -= PenguinToDummySwapHandler;
    }
}
