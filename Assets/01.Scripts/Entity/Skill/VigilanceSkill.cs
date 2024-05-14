using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class VigilanceSkill : Skill
{
    [SerializeField] private float _sizeUpValue = 0.5f;
    [SerializeField] private float _sizeUpDuration = 0.5f;
    [SerializeField] private int _sizeUpDamage = 15;
    [SerializeField] private float _stunValue = 1.5f;
    //private Vector3 _defaultSize = Vector3.one;

    private EnemyGorilla Gorilla => _owner as EnemyGorilla;

    public UnityEvent OnVigilanceEvent;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);

        //_defaultSize = _owner.transform.localScale;
    }

    public override void PlaySkill()
    {
        base.PlaySkill();

        //Gorilla.DamageCasterCompo.SetPosition();
        OnVigilanceEvent?.Invoke();
        //Gorilla.DamageCasterCompo.SetPosition();

        //체력 채워주기
        Gorilla.HealthCompo.ApplyHeal(50);
        //스탯 올려주기
        Gorilla.Stat.AddStat(_sizeUpDamage, StatType.Damage, StatMode.Increase);
        //커져라!
        Vector3 targetSize = (Gorilla.transform.localScale) * _sizeUpValue;
        Gorilla.transform.DOScale(targetSize, _sizeUpDuration);
    }

    public void CastAoEStunEvent()
    {
        // Stun Range
        // 0.7 -> 1.4 -> 2.1...
        Gorilla.DamageCasterCompo.CaseAoEDamage((0.7f * Gorilla.CurrentLevel), 0, 0, _stunValue);
    }
}
