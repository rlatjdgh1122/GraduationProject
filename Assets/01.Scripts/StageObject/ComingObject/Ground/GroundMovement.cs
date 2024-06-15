using DG.Tweening;
using System.Collections;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class GroundMovement : ComingObjetMovement
{
    private GameObject _waveEffect;
    [SerializeField] private GameObject _groundHitEffect;

    private Ground _ground;
    protected override void Awake()
    {
        base.Awake();

        _waveEffect = transform.Find("TopArea/GlacierModel/WaterWave").gameObject;
        _ground = GetComponent<Ground>();

        SignalHub.OnGroundArrivedEvent += DisableWaveEffect;
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
        _groundHitEffect = Instantiate(_groundHitEffect, GetClosestPointToCenter(), Quaternion.LookRotation(transform.position - GetClosestPointToCenter()));
        
        SoundManager.Play2DSound(SoundName.GroundHit);

        Define.CamDefine.Cam.ShakeCam.enabled = true;
        Define.CamDefine.Cam.ShakeCam.m_Lens.FieldOfView = Define.CamDefine.Cam.MainCam.fieldOfView;

        // �ε��� �� ����Ʈ / ī�޶� ����ũ + ����
        CoroutineUtil.CallWaitForSeconds(.5f, () => Define.CamDefine.Cam.ShakeCam.enabled = false);

        NavmeshManager.Instance.NavmeshBake();
        SignalHub.OnGroundArrivedEvent?.Invoke();
        _ground.ActivateEnemies(); //�̰�
        //CoroutineUtil.CallWaitForSeconds(0.1f, null, () => SignalHub.OnGroundArrivedEvent?.Invoke());

        CoroutineUtil.CallWaitForSeconds(.5f, () => Destroy(_groundHitEffect));
    }

    private void OnDisable()
    {
        SignalHub.OnGroundArrivedEvent -= DisableWaveEffect;
    }
}
