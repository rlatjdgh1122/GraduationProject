using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTrap : BaseBuilding
{
    [SerializeField]
    private LayerMask _enemyLayer;

    private bool isBattlePhase => WaveManager.Instance.IsBattlePhase;
    private bool isCatchedEnemy;

    private bool isRunning => isBattlePhase && isCatchedEnemy;

    public override void Init()
    {
        base.Init();
        isCatchedEnemy = false;
    }

    private Enemy _trappedEnemy
    {
        get
        {
            if (Physics.Raycast(transform.position, transform.up, out RaycastHit hit, 1f, _enemyLayer))
            {
                return hit.collider.GetComponent<Enemy>();
            }
            return null;
        }
    }

    protected override void Running() // 나중에는 이거 주석 풀고 써야됨
    {
        //if (!isRunning) { return; }

        //if (_trappedEnemy != null)
        //{
        //    CatchEnemy(_trappedEnemy);
        //}
    }

    protected override void Update()
    {
        if (_trappedEnemy != null)
        {
            CatchEnemy(_trappedEnemy);
        }
    }

    protected abstract void CatchEnemy(Enemy enemy);

}
