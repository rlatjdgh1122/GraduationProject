using UnityEngine;

public class TentInitPos : MonoBehaviour
{
    int remainCnt => PenguinManager.Instance.DummyPenguinCount;
    int initCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PoolableMono obj))
        {
            if(obj.CanInitTent)
            {
                PoolManager.Instance.Push(obj);
                initCount++;
                if (initCount == remainCnt)
                {
                    //WaveManager.Instance.DummyPenguinInitTentFinHandle();
                    SignalHub.OnCompletedGoToHouseEvent?.Invoke();
                    Debug.Log("다 들어갔어 친구야");
                    initCount = 0;
                }
            }
        }

    }
}
