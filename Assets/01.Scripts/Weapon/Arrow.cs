using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : PoolableMono
{
    [SerializeField] private float _bulletPower;

    protected Rigidbody _rigid;
    protected DamageCaster _damageCaster;

    private Coroutine WaitForDestroyCorountine = null;

    private void OnDestroy()
    {
        if (WaitForDestroyCorountine != null)
            StopCoroutine(WaitForDestroyCorountine);
    }

    protected virtual void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _damageCaster = GetComponent<DamageCaster>();
    }

    protected virtual void OnEnable()
    {
        WaitForDestroyCorountine = StartCoroutine(WaitForDestroy());
    }

    public virtual void Setting(TargetObject owner, LayerMask layer)
    {
        _damageCaster.SetOwner(owner, false);
        _damageCaster.TargetLayer = layer;
    }

    public virtual void Fire(Vector3 dir)
    {
        _rigid.AddForce(dir * _bulletPower, ForceMode.Impulse);
    }

    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(4);
        //PoolManager.Instance.Push(this);
        Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter(Collider coll)
    {
        if (_damageCaster.CastArrowDamage(coll, _damageCaster.TargetLayer))
        {
            //PoolManager.Instance.Push(this);
            Destroy(this.gameObject);
        }
    }
}
