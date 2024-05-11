using DG.Tweening;
using System.Collections;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class GroundMovement : ComingObjetMovement
{
    private GameObject _waveEffect;

    protected override void Awake()
    {
        base.Awake();

        _waveEffect = transform.Find("TopArea/GlacierModel/WaterWave").gameObject;

        SignalHub.OnIceArrivedEvent += DisableWaveEffect;
    }

    public override void Move()
    {
        //���� �� �� ����Ʈ
        _waveEffect.gameObject.SetActive(true);

        base.Move();
    }

    private void DisableWaveEffect()
    {
        _waveEffect.gameObject.SetActive(false);
    }

    protected override void Arrived()
    {
        SoundManager.Play2DSound(SoundName.GroundHit);

        // �ε��� �� ����Ʈ / ī�޶� ����ũ + ����
        CoroutineUtil.CallWaitForSeconds(.5f, () => Define.CamDefine.Cam.ShakeCam.enabled = true,
                                             () => Define.CamDefine.Cam.ShakeCam.enabled = false);

        WaveManager.Instance.OnIceArrivedEventHanlder();
    }

    public override void SetComingObejctPos(Transform parentTransform, Vector3 position)
    {
        transform.SetParent(parentTransform); // �θ� ����
        transform.localPosition = position; // ������ ��ġ ����

        // ȸ�� �� �θ� ����
        transform.rotation = Quaternion.identity;
        transform.SetParent(null);

        base.SetComingObejctPos(parentTransform, position);
    }

    private void OnDisable()
    {
        SignalHub.OnIceArrivedEvent -= DisableWaveEffect;
    }
}
