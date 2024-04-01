using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineHolder : MonoBehaviour
{
    private WaveManager waveManager;
    private PlayableDirector pd;
    public TimelineAsset[] ta;
    public bool isFirst;

  /*  private void Start()
    {
        isFirst = true;
        pd = GetComponent<PlayableDirector>();
        waveManager = WaveManager.Instance;
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseEndEvent += BeforeBattleWave;
        SignalHub.OnBattlePhaseStartEvent += AfterBattleWave;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= BeforeBattleWave;
        SignalHub.OnBattlePhaseStartEvent -= AfterBattleWave;
    }

    private void BeforeBattleWave()
    {
<<<<<<< HEAD
        if (waveManager.IsCurrentWaveCountEqualTo(3) && isFirst)
        {
            pd.Play(ta[0]);
            isFirst = false;
        }
=======
        //if(waveManager.IsCurrentWaveCountEqualTo(4) && isFirst)
        //{
        //    pd.Play(ta[0]);
        //    isFirst = false;
        //}
>>>>>>> parent of fdf090e2 (cutscene)
    }

    private void AfterBattleWave()
    {

    }*/
}
