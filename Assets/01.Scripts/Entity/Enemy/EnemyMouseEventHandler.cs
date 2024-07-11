using ArmySystem;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class EnemyMouseEventHandler : MonoBehaviour
{
    [SerializeField] private Enemy _owner = null;

    private CapsuleCollider _coliderCompo = null;

    public EnemyArmy EnemyArmy => _owner.MyArmy;

    private void Awake()
    {
        _coliderCompo = GetComponent<CapsuleCollider>();
    }

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

    public void SetColiderActive(bool active) => _coliderCompo.enabled = active;
}
