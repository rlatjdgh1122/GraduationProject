using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : PoolableMono
{
    [SerializeField] private float _bulletPower;

    private Rigidbody _rigid;
    protected DamageCaster _damageCaster;

    private void OnEnable()
    {
        _rigid = GetComponent<Rigidbody>();
        _damageCaster = GetComponent<DamageCaster>();
        StartCoroutine(WaitForDestroy());
    }

    public virtual void Setting(TargetObject owner, LayerMask layer)
    {
        _damageCaster.SetOwner(owner, false);
        _damageCaster.TargetLayer = layer;
    }

    public void Fire(Vector3 dir)
    {
        _rigid.AddForce(dir * _bulletPower, ForceMode.Impulse);
    }

    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (_damageCaster.CastArrowDamage(coll, _damageCaster.TargetLayer))
        {
            Destroy(this.gameObject);
        }
    }
}
