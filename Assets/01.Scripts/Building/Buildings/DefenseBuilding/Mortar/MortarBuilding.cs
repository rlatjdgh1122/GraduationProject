using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.iOS;

public class MortarBuilding : DefenseBuilding
{
    [SerializeField]
    private MortarRock _rockPrefab;

    [SerializeField]
    private Transform _firePos;

    private readonly string prefabName = "MortarRock";

    private bool isFired;

    private ParticleSystem _mortarFireParticle;

    private IgnitingPenguinAnimaionTrigger _ignitingPenguin;
    private BurningRope _burningRope;

    private Transform _cannonTransform;

    private float chargingMoveValue = -1f;
    private float fireMoveValue = 1f;

    private Vector3 _originScale = Vector3.one;
    private Vector3 _chargingScale = new Vector3(1.2f, 1.0f, 1.2f);

    private bool isBattlePhase => WaveManager.Instance.IsBattlePhase;

    protected override void Awake()
    {
        base.Awake();
        _mortarFireParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        _ignitingPenguin = transform.Find("IgnitingPenguin/Visual").GetComponent<IgnitingPenguinAnimaionTrigger>();
        _burningRope = transform.Find("Rope").GetComponent<BurningRope>();

        _cannonTransform = transform.Find("Visual/Cannon").transform;
    }


    protected override void Running()
    {
        if (isBattlePhase) // 이것을 Running으로 옮길 것 입니다.
        {
            if (_currentTarget != null && !isFired)
            {
                StartCoroutine(FireRoutine());
            }
        }
    }

    private IEnumerator FireRoutine()
    {
        isFired = true;
        float elapsedTime = 0.0f;
        float remainWaitTime = 0.0f;

        while (isBattlePhase && _currentTarget != null)
        {
            _ignitingPenguin.SetGetTourchAnimation();
            float waitTime = _ignitingPenguin.AnimaionLength + _burningRope.Duration;
            remainWaitTime = waitTime - Time.deltaTime;

            while (elapsedTime < waitTime)
            {
                elapsedTime += Time.deltaTime;

                if (!isBattlePhase) // 쏘려고 하는데 전투페이즈가 아니면 부채로 호다닥 끔
                {
                    CoroutineUtil.CallWaitForSeconds(remainWaitTime,
                        () => _ignitingPenguin.StartSwingAnimation(),
                        () => _ignitingPenguin.StopSwingAnimation());
                    ;
                    yield break;
                }

                yield return null;
            }

            Fire();
            elapsedTime = 0.0f; // 시간 초기화
        }
    }

    private void Fire()
    {
        float endYValue = _cannonTransform.localPosition.y + fireMoveValue;
        _cannonTransform.DOLocalMoveY(endYValue, 1f).SetEase(Ease.OutBack);
        _cannonTransform.DOScale(_originScale, 1f).SetEase(Ease.OutBack);

        MortarRock rock = PoolManager.Instance.Pop(prefabName) as MortarRock;
        _mortarFireParticle.Play();
        rock.transform.position = _firePos.position;

        StartCoroutine(rock.BulletMove(rock.transform.position, _currentTarget.position));

        SoundManager.Play3DSound(SoundName.MortarFire, _firePos.position);
    }

    public void ChargingCannon()
    {
        float endYValue = _cannonTransform.localPosition.y + chargingMoveValue;

        _cannonTransform.DOLocalMoveY(endYValue, 3f);

        _cannonTransform.DOScale(_chargingScale, 3f);
    }

    protected override void SetInstalled()
    {
        base.SetInstalled();

        _cannonTransform.transform.localScale = _originScale;
        isFired = false;
    }
}
