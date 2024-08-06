using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LanceUltTruck : PoolableMono
{
    [SerializeField]
    private float _truckSpeed;
    [SerializeField]
    private float _stunValue;
    [SerializeField]
    private int _ultimateDamage;
    
    private int _groundLayer = 0;
    private Rigidbody _rigid;
    private DamageCaster _damageCaster;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _damageCaster = GetComponent<DamageCaster>();
        _groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }

    private void Update()
    {
        if (!IsPositionValid(transform.position))
        {
            StartCoroutine(WaterFallTruck());
        }
    }

    public void Setting(Entity owner, LayerMask layer)
    {
        _damageCaster.SetOwner(owner);
        _damageCaster.TargetLayer = layer;
    }

    private bool IsPositionValid(Vector3 position)
    {
        return Physics.Raycast(position, new Vector3(0, -1f, 0), 5f, _groundLayer);
    }

    public void TruckMove()
    {
        StartCoroutine(WaitForAnim());
    }

    IEnumerator WaterFallTruck()
    {
        _rigid.useGravity = true;

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }

    IEnumerator WaitForAnim()
    {
        _rigid.AddForce(transform.forward * 4, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
        _rigid.AddForce(transform.forward * _truckSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        // 트럭 폭발 로직 추가
        SoundManager.Play3DSound(SoundName.UltimateLanceExplosion, transform.position);
        _damageCaster.CastTruckDamage(_stunValue, _ultimateDamage, transform.position, _damageCaster.TargetLayer);
        Destroy(this.gameObject);
    }
}