using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[Serializable]
public class ScreenData
{
    [SerializeField]
    private GifType _gifType;
    public GifType GifType => _gifType;

    [SerializeField]
    private VideoClip _gifClip;
    public VideoClip GifClip => _gifClip;

    [TextArea] [SerializeField]
    private string _gifName;
    public string GifName => _gifName;
}

[Serializable]
public class GifSpeedData
{
    [SerializeField]
    private float _speed;
    public float Speed => _speed;

    [SerializeField] [TextArea]
    private string _speedText;
    public string SpeedText => _speedText;
}

[CreateAssetMenu(menuName = "SO/Screen")]
public class GifScreenDataSO : ScriptableObject
{
    [SerializeField] private List<ScreenData> _gifScreenList = new();
    public List<ScreenData> GifScreenList => _gifScreenList;

    [SerializeField] private List<GifSpeedData> _gifSpeedList = new();
    public List<GifSpeedData> GifSpeedList => _gifSpeedList;
}