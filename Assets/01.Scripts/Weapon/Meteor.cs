using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : PoolableMono
{
    [SerializeField] private float _meteorPower;
    [SerializeField] private float _explosionRadius;

    private Rigidbody _rigid;
    private DamageCaster _damageCaster;

    private void OnEnable()
    {
        _rigid = GetComponent<Rigidbody>();
        _damageCaster = GetComponent<DamageCaster>();
    }

    public void Setting(Entity owner, LayerMask layer)
    {
        _damageCaster.SetOwner(owner);
        _damageCaster.TargetLayer = layer;
    }

    public void Fire(Vector3 dir)
    {
        _rigid.AddForce(dir * _meteorPower, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Ground") || coll.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        // ���׿� ���� ���� �߰�
        _damageCaster.CastMeteorDamage(transform.position, _damageCaster.TargetLayer);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}