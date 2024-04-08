using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : PoolableMono
{
    [SerializeField] private float _meteorPower;
    private bool _canMove = false;

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
        StartCoroutine(BeforeMeteorAtk());

        if (_canMove)
        {
            _rigid.AddForce(dir * _meteorPower, ForceMode.Impulse);
        }
    }

    private IEnumerator BeforeMeteorAtk()
    {
        yield return new WaitForSeconds(3);
        _canMove = true;
    }

    private void Explode()
    {
        // 메테오 폭발 로직 추가
        _damageCaster.CastMeteorDamage(transform.position, _damageCaster.TargetLayer);
        _canMove = false;
        Destroy(this.gameObject);
    }
}
