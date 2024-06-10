using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

public enum GifType
{
    GetReward,
    LegionUI,
    NoisebarInfo,
    PenguinFight,
    GeneralBuy,
    PenguinBuy,
    WorkerPenguin,
    NexusUpgrade
}

public class GifScreenController : MonoBehaviour
{
    [SerializeField] private GifScreenDataSO _gifDataSO;
    [SerializeField] private List<float> _gifSpeedList = new();

    private VideoPlayer _videoPlayer;
    private int _speedUpBtnClickCount = 0;

    public int SpeedUpBtnClickCount => _speedUpBtnClickCount;
    public GifScreenDataSO GifDataSO => _gifDataSO;
    public List<float> GifSpeedList => _gifSpeedList;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    private float timeScale;

    public void ShowGif(GifType type)
    {
        if (_videoPlayer.clip != null) return;

        ScreenData gif = _gifDataSO.GifScreenList.Find(g => g.GifType == type);

        if (gif != null)
        {
            UIManager.Instance.ShowPanel("GifScreen", true);

            timeScale = Time.timeScale;
            Time.timeScale = 0;

            _videoPlayer.clip = gif.GifClip;

            OnGifPlay();
        }
    }

    private void OnGifPlay()
    {
        if (_videoPlayer.clip == null) return;

        _videoPlayer.loopPointReached += OnGifEnd;
        _videoPlayer.Play();
    }

    private void OnGifEnd(VideoPlayer vp)
    {
        _videoPlayer.clip = null;

        _videoPlayer.Stop();
        UIManager.Instance.HidePanel("GifScreen");

        Time.timeScale = timeScale;

        _videoPlayer.loopPointReached -= OnGifEnd;
    }

    public void OnGifSpeed()
    {
        if (_videoPlayer.clip == null) return;

        _speedUpBtnClickCount = ++_speedUpBtnClickCount >= _gifSpeedList.Count ? 0 : _speedUpBtnClickCount;

        _videoPlayer.playbackSpeed = _gifSpeedList[_speedUpBtnClickCount];
    }


    public void ExitGifScreenUI()
    {
        _speedUpBtnClickCount = 0;
        _videoPlayer.playbackSpeed = 1;

        OnGifEnd(null);
    }
}
