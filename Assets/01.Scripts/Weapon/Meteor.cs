using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : PoolableMono
{
    [SerializeField] private float _meteorPower;

    private Rigidbody _rigid;
    private DamageCaster _damageCaster;

    private void Awake()
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
        if (coll.gameObject.layer == LayerMask.NameToLayer("Ground") 
            || coll.gameObject.layer == LayerMask.NameToLayer("SmallGround"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        // 메테오 폭발 로직 추가
        _damageCaster.CastMeteorDamage(transform.position, _damageCaster.TargetLayer);
        Destroy(this.gameObject);
        //PoolManager.Instance.Push(this);
    }
}
