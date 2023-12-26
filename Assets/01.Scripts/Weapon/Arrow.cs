using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _bulletPower;

    private Rigidbody _rigid;
    private Entity _owner;
    private DamageCaster _damageCaster;

    private void OnEnable()
    {  
        _rigid = GetComponent<Rigidbody>();
        _damageCaster = GetComponent<DamageCaster>();
        StartCoroutine(WaitForDestroy());
    }

    public void Setting(Entity owner, LayerMask layer)
    {  
        _owner = owner;
        _damageCaster.SetOwner(owner);
        _damageCaster.TargetLayer = layer; 
    }

    public void Fire(Vector3 dir)
    {
        _rigid.AddForce(dir * _bulletPower, ForceMode.Impulse);  
    }

    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider coll)
    {
        _damageCaster.CastDamage();

        if (_damageCaster.CastDamage())
        {
            Destroy(this.gameObject);
        }
    }
}
