using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedAttackableEntity : EntityAttackData
{
    [SerializeField] private CanvasLookToCamera _canvasCamera;
    [SerializeField] private int _attackEventValue;
    [SerializeField] private int _bleedDmg;

    [SerializeField] private int _repeat;
    [SerializeField] private float _duration;
    private Canvas _healthCanvas;

    public int AttackEventValue
    {  
        get
        {
            return _attackEventValue;
        }
    }

    public bool Bleed;

    protected override void Awake()
    {
        base.Awake();

        _healthCanvas = transform.Find("HealthUICanvas").GetComponent<Canvas>();
    }

    public override void MeleeSphereAttack()
    {
        if(Bleed)
        {
            StartCoroutine(BleedAnimation());

            Bleed = false;
            DamageCasterCompo.BleedCast(_bleedDmg, _repeat, _duration, HitType.BleedHit);
        }

        DamageCasterCompo.CaseAoEDamage(false, 0);
    }

    private IEnumerator BleedAnimation()
    {
        _canvasCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _canvasCamera.gameObject.SetActive(false);
    }
}
