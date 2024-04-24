using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public abstract class BaseTrap : BaseBuilding
{
    [SerializeField]
    private LayerMask _enemyLayer;

    private bool isBattlePhase => WaveManager.Instance.IsBattlePhase;

    private bool isRunning => isBattlePhase && !isPlayed;

    private bool isPlayed;

    protected DamageCaster _damageCaster;

    [Header("TrapValue")]
    [SerializeField]
    private float catchRange;

    protected override void Awake()
    {
        base.Awake();
        _damageCaster = GetComponent<DamageCaster>();

        isPlayed = false;                   //나중에 UI로 설치하느게 생기면 지워야함
    }

    protected override void SetInstalled()
    {
        base.SetInstalled();

        isPlayed = false;
    }

    private (bool iscatched, RaycastHit _raycastHit, Enemy _catchedEnemy) GetCathedEnemy()
    {
        bool catched = Physics.SphereCast(transform.position + (Vector3.down * catchRange), catchRange, transform.up * 0.5f, out RaycastHit hit, 1f, _enemyLayer);
        if (catched)
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                return (catched, hit, enemy);
            }
        }
        return (catched, default, null);
    }

    protected override void Running()
    {
        if (isRunning)
        {
            var catchedInfo = GetCathedEnemy();

            if (catchedInfo._catchedEnemy != null)
            {
                CatchEnemy(catchedInfo._catchedEnemy, catchedInfo._raycastHit);
                isPlayed = true;
                RemoveTrap();
            }

        }
    }

    protected abstract void CatchEnemy(Enemy enemy, RaycastHit raycastHit);

    protected virtual void RemoveTrap()
    {
        //일단 이건데 나중에 디졸브로 자연스럽게
        PoolManager.Instance.Push(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + (Vector3.down * catchRange) + (transform.up * catchRange), catchRange);
    }
}
