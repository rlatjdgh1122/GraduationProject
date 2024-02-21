using System.Security.Cryptography;
using UnityEngine;

public class PenguinAnimationTrigger : MonoBehaviour
{
    private Penguin _penguin;

    private void Awake()
    {
        _penguin = transform.parent.GetComponent<Penguin>();
    }

    public void AttackTrigger()
    {
        _penguin.Attack();
    }

    public void AoEAttackTrigger()
    {

        Debug.Log("АјАн 1");
        _penguin.AoEAttack();
    }

    private void RangeAttackTrigger()
    {
        _penguin.RangeAttack();
    }

    public void DeadCompleteTrigger()
    {
        _penguin.enabled = false;
    }

    private void AnimationEndTrigger()
    {
        _penguin.AnimationTrigger();
    }
}
