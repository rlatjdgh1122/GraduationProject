using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceUltTruck : PoolableMono
{
    [SerializeField]
    private float _truckSpeed;

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

    public void TruckMove(Vector3 mousePos)
    {
        StartCoroutine(WaitAnim(mousePos));
    }

    IEnumerator WaitAnim(Vector3 mousePos)
    {
        yield return new WaitForSeconds(1.3f);
        _rigid.AddForce(mousePos * _truckSpeed, ForceMode.Impulse);
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
        // 메테오 폭발 로직 추가
        _damageCaster.CastMeteorDamage(transform.position, _damageCaster.TargetLayer);
        //Destroy(this.gameObject);
        PoolManager.Instance.Push(this);
    }
}

