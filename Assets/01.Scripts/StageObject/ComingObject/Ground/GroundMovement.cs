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
        //빙하 올 때 이펙트
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

        // 부딪힐 때 이펙트 / 카메라 쉐이크 + 사운드
        CoroutineUtil.CallWaitForSeconds(.5f, () => Define.CamDefine.Cam.ShakeCam.enabled = true,
                                              () => Define.CamDefine.Cam.ShakeCam.enabled = false);

        WaveManager.Instance.OnIceArrivedEventHanlder();
    }

    public override void SetComingObejctPos(Transform parentTransform, Vector3 position)
    {
        transform.SetParent(parentTransform); // 부모 설정
        transform.localPosition = position; // 빙하의 위치 설정

        // 회전 및 부모 해제
        transform.rotation = Quaternion.identity;
        transform.SetParent(null);

        // 중앙과 히트 포인트 사이의 거리 계산
        float centerToHitPointX = Mathf.Abs(_meshCollider.transform.position.x - RaycastHit_ToCenterPos.x);
        float centerToHitPointZ = Mathf.Abs(_meshCollider.transform.position.z - RaycastHit_ToCenterPos.z);

        // 가장 가까운 포인트와 히트 포인트 사이의 거리 계산
        float closestPointToHitPointX = Mathf.Abs(_closestPointToCenter.x - RaycastHit_ToCenterPos.x);
        float closestPointToHitPointZ = Mathf.Abs(_closestPointToCenter.z - RaycastHit_ToCenterPos.z);

        // X와 Z 거리 계산
        float xDistance = Mathf.Abs(centerToHitPointX - closestPointToHitPointX);
        float zDistance = Mathf.Abs(centerToHitPointZ - closestPointToHitPointZ);

        Debug.Log($"xDistance: {xDistance}");
        Debug.Log($"zDistance: {zDistance}");

        // 타겟 벡터 계산
        Vector3 targetVec = new Vector3(RaycastHit_ToCenterPos.x, 0f, RaycastHit_ToCenterPos.z);

        //// X 좌표에 따라 타겟 벡터 조정 (양수인지, 음수인지, 0인지)
        targetVec.x += Mathf.Sign(transform.position.x) * xDistance;
        //
        //// Z 좌표에 따라 타겟 벡터 조정 (양수인지, 음수인지, 0인지)
        targetVec.z += Mathf.Sign(transform.position.y) * zDistance;

        // 타겟 위치 설정
        _targetPos = targetVec;
    }

    private void OnDisable()
    {
        SignalHub.OnIceArrivedEvent -= DisableWaveEffect;
    }
}
