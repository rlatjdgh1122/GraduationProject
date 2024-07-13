using System.Collections;
using UnityEngine;

public class MortarRock : Arrow, IParabolicProjectile
{
    private float timer;
    private MortarExplosionEffect _attackFeedback;

    [SerializeField] // 일단 여기다가 넣음
    private int damage;

    private bool isDestoried;

    [SerializeField]
    private GameObject _mortarAttackRangeSprite;

    private Vector3 _endPos;

    private void Awake()
    { 
        //_damageCaster = GetComponent<DamageCaster>();
        _attackFeedback = transform.GetChild(0).GetComponent<MortarExplosionEffect>();
    }

    public override void Setting(TargetObject owner, LayerMask layer)
    {
        _damageCaster.SetOwner(owner, false);
        _damageCaster.TargetLayer = layer;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        DestroyRock();
    }

    private void DestroyRock()
    {
        if (!isDestoried)
        {
            isDestoried = true;
            _mortarAttackRangeSprite.transform.SetParent(transform);
            _mortarAttackRangeSprite.SetActive(false);
            _damageCaster.CastBuildingAoEDamage(transform.position, _damageCaster.TargetLayer, damage);
            _attackFeedback.CreateFeedback(_endPos);
            SoundManager.Play3DSound(SoundName.MortarExplosion, transform.position);

            CoroutineUtil.CallWaitForSeconds(0.7f, () =>
            {
                PoolManager.Instance.Push(this);
            });
        }
    }

    public void ExecuteAttack(Vector3 startPosition, Vector3 targetPosition, float maxTime, bool isPool)
    {
        _mortarAttackRangeSprite.SetActive(true);
        _mortarAttackRangeSprite.transform.position = targetPosition;
        _mortarAttackRangeSprite.transform.SetParent(null);

        StartCoroutine(Parabola.ParabolaMove(this,_rigid, startPosition, targetPosition, maxTime, isPool, true, DestroyRock));
    }
}
