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
    PenguinBuy
}

public class GifScreenController : MonoBehaviour
{
    [SerializeField] private GifScreenDataSO _gifDataSO;

    private VideoPlayer _videoPlayer;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    public void ShowGif(GifType type)
    {
        if (_videoPlayer.clip != null) return;

        ScreenData gif = _gifDataSO.GifScreenList.Find(g => g.GifType == type);

        if (gif != null)
        {
            UIManager.Instance.ShowPanel("GifScreen");

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

        Time.timeScale = 1;

        _videoPlayer.loopPointReached -= OnGifEnd;
    }

    public void ExitGifScreenUI()
    {
        OnGifEnd(null);
    }
}
