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

    private void Update()
    {
        if(waveManager.IsCurrentWaveCountEqualTo(4) && isFirst)
        {
            pd.Play(ta[0]);
            isFirst = false;
        }
    }
}
