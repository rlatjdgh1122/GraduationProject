using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _bulletPower;

    private int _damage;

    private Rigidbody _rigid;
    private Entity _owner;

    Type _ownerType;

    private void OnEnable()
    {  
        _rigid = GetComponent<Rigidbody>();
        StartCoroutine(WaitForDestroy());
    }

    public void Setting(Entity owner, Type type)
    {  
        _owner = owner;
        _ownerType = type;
        _damage = _owner.Stat.GetDamage();
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(_ownerType);
        if (_ownerType == typeof(ArcherPenguin))
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.HealthCompo.ApplyDamage(_damage, Vector2.zero, Vector2.zero, _owner);
                ParticleSystem effect = Instantiate(_owner.HitEffect, other.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                effect.Play();
                Destroy(this.gameObject);
            }
        }
        else if (_ownerType == typeof(EnemyArcherPenguin))
        {
            if (other.TryGetComponent<Penguin>(out Penguin enemy))
            {
                enemy.HealthCompo.ApplyDamage(_damage, Vector2.zero, Vector2.zero, _owner);
                ParticleSystem effect = Instantiate(_owner.HitEffect, other.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                effect.Play();
                Destroy(this.gameObject);
            }
            else if (other.TryGetComponent<NexusBase>(out NexusBase nexus))
            {
                nexus.HealthCompo.ApplyDamage(_damage, Vector2.zero, Vector2.zero, _owner);
                ParticleSystem effect = Instantiate(_owner.HitEffect, other.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                effect.Play();
                Destroy(this.gameObject);
            }
        }
    }
}
