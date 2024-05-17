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

        // �ε��� �� ����Ʈ / ī�޶� ����ũ + ����
        CoroutineUtil.CallWaitForSeconds(.5f, () => Define.CamDefine.Cam.ShakeCam.enabled = true,
                                              () => Define.CamDefine.Cam.ShakeCam.enabled = false);

        NavmeshManager.Instance.NavmeshBake();
        _ground.ActivateEnemies(); //�̰�

        CoroutineUtil.CallWaitForSeconds(0.1f, null, () => SignalHub.OnGroundArrivedEvent?.Invoke());

        CoroutineUtil.CallWaitForSeconds(.5f, null, () => Destroy(_groundHitEffect));
    }

    public override void SetComingObejctPos(Transform parentTransform, Vector3 position)
    {
        transform.SetParent(parentTransform); // �θ� ����
        transform.localPosition = position; // ������ ��ġ ����

        // ȸ�� �� �θ� ����
        transform.rotation = Quaternion.identity;
        transform.SetParent(null);

        // �߾Ӱ� ��Ʈ ����Ʈ ������ �Ÿ� ���
        float centerToHitPointX = Mathf.Abs(_meshCollider.transform.position.x - RaycastHit_ToCenterPos.x);
        float centerToHitPointZ = Mathf.Abs(_meshCollider.transform.position.z - RaycastHit_ToCenterPos.z);

        Vector3 closestPointToCenter = GetClosestPointToCenter();

        // ���� ����� ����Ʈ�� ��Ʈ ����Ʈ ������ �Ÿ� ���
        float closestPointToHitPointX = Mathf.Abs(closestPointToCenter.x - RaycastHit_ToCenterPos.x);
        float closestPointToHitPointZ = Mathf.Abs(closestPointToCenter.z - RaycastHit_ToCenterPos.z);

        // X�� Z �Ÿ� ���
        float xDistance = Mathf.Abs(centerToHitPointX - closestPointToHitPointX);
        float zDistance = Mathf.Abs(centerToHitPointZ - closestPointToHitPointZ);

        // Ÿ�� ���� ���
        Vector3 targetVec = new Vector3(RaycastHit_ToCenterPos.x, 0f, RaycastHit_ToCenterPos.z);

        // X ��ǥ�� ���� Ÿ�� ���� ���� (�������, ��������, 0����)
        targetVec.x += Mathf.Sign(transform.localPosition.x) * xDistance;
        
        // Z ��ǥ�� ���� Ÿ�� ���� ���� (�������, ��������, 0����)
        targetVec.z += Mathf.Sign(transform.localPosition.z) * zDistance;

        // Ÿ�� ��ġ ����
        _targetPos = targetVec;
    }

    private void OnDisable()
    {
        SignalHub.OnGroundArrivedEvent -= DisableWaveEffect;
    }
}
