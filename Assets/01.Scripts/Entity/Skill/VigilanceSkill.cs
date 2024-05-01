using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class VigilanceSkill : Skill
{
    [SerializeField] private float _sizeUpValue = 0.5f;
    [SerializeField] private float _sizeUpDuration = 0.5f;
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

        Gorilla.DamageCasterCompo.SetPosition();
        OnVigilanceEvent?.Invoke();
        Gorilla.DamageCasterCompo.SetPosition();
        Vector3 targetSize = (Gorilla.transform.localScale) * _sizeUpValue;
        Gorilla.transform.DOScale(targetSize, _sizeUpDuration);
        //Vector3 targetSize = (_owner.transform.localScale) * _sizeUpValue;
        //_owner.transform.DOScale(targetSize, _sizeUpDuration);
    }
}
