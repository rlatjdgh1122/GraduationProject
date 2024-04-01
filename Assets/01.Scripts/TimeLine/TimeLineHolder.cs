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

    private void Start()
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
        //if (waveManager.IsCurrentWaveCountEqualTo(3) && isFirst)
        //{
        //    pd.Play(ta[0]);
        //    StartCoroutine(TutoQuestCorou());
        //    isFirst = false;
        //}
        //else
        //{
        //    Debug.Log(waveManager.CurrentWaveCount);
        //}
    }

    private IEnumerator TutoQuestCorou()
    {
        yield return new WaitForSeconds(4.5f);
        TutorialManager.Instance.StartCurTutorialQuest();
    }

    private void AfterBattleWave()
    {

    }
}
