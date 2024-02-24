using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableEntity : MonoBehaviour
{
    public DamageCaster DamageCasterCompo { get; private set; }

    private Entity owner;

    private void Awake()
    {
        DamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        owner = GetComponent<Entity>(); 
        DamageCasterCompo.SetOwner(owner.Stat);
    }

    public void Attack()
    {
        DamageCasterCompo.CastDamage();
    }
}
