using DG.Tweening;
using System.Collections;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    [SerializeField] private float _moveDuration = 5f;

    [SerializeField] private Color startColor;
    [SerializeField] private Color targetColor;
    [SerializeField] private Color endColor;

    [SerializeField]
    private LayerMask _groundLayer;

    #region ������Ƽ
    private Outline _outline;
    #endregion

    private GameObject _waveEffect;

    private MeshCollider _meshCollider;

    private Vector3 _centerPos;
    private Vector3 _closestPointDirToCenter => _meshCollider.ClosestPoint(_centerPos); // �ݵ�� _meshCollider�� convex�� �������

    private Vector3 RaycastHit_ToCenterPos
    {
        get
        {
            if (Physics.Raycast(_closestPointDirToCenter, (_centerPos - _closestPointDirToCenter).normalized, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                Debug.Log(hit.transform.position);
                return hit.point;
            }

            Debug.Log("?!");
            return Vector3.zero;
        }
    }

    private Vector3 _targetPos;

    private void Awake()
    {
        _outline = GetComponent<Outline>();

        _waveEffect = transform.Find("TopArea/GlacierModel/WaterWave").gameObject;
        _meshCollider = transform.Find("TopArea").GetComponent<MeshCollider>();
    }

    public void GroundMove()
    {
        //���� �� �� ����Ʈ
        _waveEffect.gameObject.SetActive(true);

        transform.DOMove(_targetPos, _moveDuration).
            OnComplete(() =>
            {
                SoundManager.Play2DSound(SoundName.GroundHit);

                // �ε��� �� ����Ʈ / ī�޶� ����ũ + ����
                CoroutineUtil.CallWaitForSeconds(.5f, () => Define.CamDefine.Cam.ShakeCam.enabled = true,
                                                     () => Define.CamDefine.Cam.ShakeCam.enabled = false);

                _waveEffect.gameObject.SetActive(false);

                WaveManager.Instance.OnIceArrivedEventHanlder();
                SignalHub.OnBattlePhaseStartEvent -= GroundMove;

                //DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, targetColor, 0.7f).OnComplete(() =>
                //{
                //    WaveManager.Instance.OnIceArrivedEventHanlder();

                //    Debug.Log(_enemies.Length);
                //    foreach (Enemy enemy in _enemies)
                //    {
                //        enemy.IsMove = true;
                //        enemy.NavAgent.enabled = true;
                //    }
                //    SignalHub.OnBattlePhaseStartEvent -= GroundMoveHandle;
                //});
            });


    }

    //private void SetOutline()
    //{
    //    DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, endColor, 0.7f).OnComplete(() =>
    //    {
    //        _outline.enabled = false;
    //        SignalHub.OnBattlePhaseEndEvent -= GroundMove;
    //    });
    //}

    public void SetGroundPos(Transform parentTransform, Vector3 position)
    {
        transform.SetParent(parentTransform); // �θ� ����
        transform.localPosition = position; // ������ ��ġ ����

        // ȸ�� �� �θ� ����
        transform.rotation = Quaternion.identity;
        transform.SetParent(null);

        // �߾Ӱ� ��Ʈ ����Ʈ ������ �Ÿ� ���
        float centerToHitPointX = Mathf.Abs(_meshCollider.transform.position.x - RaycastHit_ToCenterPos.x);
        float centerToHitPointZ = Mathf.Abs(_meshCollider.transform.position.z - RaycastHit_ToCenterPos.z);

        // ���� ����� ����Ʈ�� ��Ʈ ����Ʈ ������ �Ÿ� ���
        float closestPointToHitPointX = Mathf.Abs(_closestPointDirToCenter.x - RaycastHit_ToCenterPos.x);
        float closestPointToHitPointZ = Mathf.Abs(_closestPointDirToCenter.z - RaycastHit_ToCenterPos.z);

        // X�� Z �Ÿ� ���
        float xDistance = Mathf.Abs(centerToHitPointX - closestPointToHitPointX);
        float zDistance = Mathf.Abs(centerToHitPointZ - closestPointToHitPointZ);

        // Ÿ�� ���� ���
        Vector3 targetVec = new Vector3(RaycastHit_ToCenterPos.x, 0f, RaycastHit_ToCenterPos.z);

        //// X ��ǥ�� ���� Ÿ�� ���� ���� (�������, ��������, 0����)
        targetVec.x += Mathf.Sign(transform.position.x) * xDistance;
        //
        //// Z ��ǥ�� ���� Ÿ�� ���� ���� (�������, ��������, 0����)
        targetVec.z += Mathf.Sign(transform.position.z) * zDistance;

        // Ÿ�� ��ġ ����
        _targetPos = targetVec;
    }

    public void SetMoveTarget(Transform trm)
    {
        transform.SetParent(trm);
        _centerPos = trm.position;
    }
}
