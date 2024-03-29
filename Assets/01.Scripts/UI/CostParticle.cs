using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostParticle : PoolableMono
{
    [Header("Icon")]
    [SerializeField] private float _scaleValue;
    [SerializeField] private float _duration;
    [SerializeField] private SoundName _soundName;

    private Image _icon;

    private ParticleImage _particleImage;
    private bool IsParticleImage;
    private int _cost;
    private int _repeat;        
    private int _increaseValue; //������
    private int _divideCost = 0;

    private void Awake()
    {
        _particleImage = GetComponent<ParticleImage>();
    }

    public void TargetPosition(Transform parent, Image target) //Ÿ�� ����
    {
        transform.parent = parent;
        _particleImage.attractorTarget = target.transform;

        transform.position = Vector3.zero;

        _repeat = 0;
    }

    public void Position(Vector3 startPosition) //���� ��ġ ����
    {
        transform.position = startPosition;
    }

    public void SetBurst(int index, float time, int value) //�̹��� ����
    {
        _particleImage.SetBurst(index, time, value);
    }

    public void Setting(int cost, int divideCost) //�� ������ ��, ������
    {
        _cost = cost;
        _divideCost = divideCost;
    }

    public void PlayParticle()
    {
        _particleImage.Play();
    }

    public void OnAnyParticleFinished()
    {
        _repeat++;

        SoundManager.Play2DSound(_soundName);

        _increaseValue = CostManager.Instance.Cost + (_repeat * _divideCost);
        CostManager.Instance.CostArriveText(_increaseValue);
    }

    public void OnLastParticleFinished()
    {
        _repeat = 0;

        StopAllCoroutines();

        CostManager.Instance.OnlyCostUIUseThis(_cost);
        CostManager.Instance.CostStopText();

        PoolManager.Instance.Push(this);
    }
}
