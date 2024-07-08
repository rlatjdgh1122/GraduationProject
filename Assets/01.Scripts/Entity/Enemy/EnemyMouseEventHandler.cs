using ArmySystem;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class EnemyMouseEventHandler : MonoBehaviour
{
    [SerializeField] private Enemy _owner = null;

    public EnemyArmy EnemyArmy => _owner.MyArmy;

    private void OnMouseEnter()
    {
        EnemyArmy.OnMouseEnter();
    }

    private void OnMouseExit()
    {
        EnemyArmy.OnMouseExit();
    }

    public void OnClick()
    {
        EnemyArmy.OnClick();
        ArmyManager.Instance.SetTargetEnemyArmy(EnemyArmy);
    }
}
