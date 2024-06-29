using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LanceUltTruck : PoolableMono
{
    [SerializeField]
    private float _truckSpeed;

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
        if (IsPositionValid(transform.position))
        {
            StartCoroutine(WaterFallTruck());
        }
    }

    IEnumerator WaterFallTruck()
    {
        _rigid.useGravity = true;

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }
    
    public void TruckMove()
    {
        StartCoroutine(WaitAnim());
    }

    IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(1.3f);
        _rigid.AddForce(transform.forward * _truckSpeed, ForceMode.Impulse);
    }
    public void Setting(Entity owner, LayerMask layer)
    {
        _damageCaster.SetOwner(owner);
        _damageCaster.TargetLayer = layer;
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
        _damageCaster.CastMeteorDamage(transform.position, _damageCaster.TargetLayer);
        Destroy(this.gameObject);
    }

    private bool IsPositionValid(Vector3 position)
    {
        return Physics.Raycast(position, new Vector3(0, -1, 0), 5f, _groundLayer);
    }
}

