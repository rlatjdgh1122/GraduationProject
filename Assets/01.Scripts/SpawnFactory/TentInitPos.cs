using UnityEngine;

public class TentInitPos : MonoBehaviour
{
    int remainCnt => WaveManager.Instance.CurPTspawnPenguinCount;
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
                    WaveManager.Instance.DummyPenguinInitTentFinHandle();
                    initCount = 0;
                }
            }
        }

    }
}
