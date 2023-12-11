using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _bulletPower;

    private int _damage;

    private Rigidbody _rigid;
    private Entity _owner;

    private string _whatIsHitableTag;

    private void OnEnable()
    {
        _rigid = GetComponent<Rigidbody>();
        StartCoroutine(WaitForDestroy());
    }

    public void SetOwner(Entity owner, string tag)
    {
        _owner = owner;
        _whatIsHitableTag = tag;
        _damage = _owner.Stat.GetDamage();
    }

    public void Fire(Vector3 dir)
    {
        _rigid.AddForce(dir * _bulletPower, ForceMode.Impulse);
    }

    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_whatIsHitableTag))
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable health))
            {
                health.ApplyDamage(_damage, Vector2.zero, Vector2.zero, _owner);
                ParticleSystem effect = Instantiate(_owner.HitEffect, other.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                effect.Play();
                Destroy(this.gameObject);
            }
        }  
        else if (!other.CompareTag("Enemy") && !other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
