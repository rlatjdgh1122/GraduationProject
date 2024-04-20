using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTrap : BaseBuilding
{
    [SerializeField]
    private LayerMask _enemyLayer;

    private bool isBattlePhase => WaveManager.Instance.IsBattlePhase;

    private bool isRunning => isBattlePhase && !isPlayed;

    private bool isPlayed;

    protected DamageCaster _damageCaster;

    protected override void Awake()
    {
        base.Awake();
        _damageCaster = GetComponent<DamageCaster>();


        isPlayed = false;                   //나중에 UI로 설치하느게 생기면 지워야함
        StartCoroutine(RunningCoroutine()); //나중에 UI로 설치하느게 생기면 지워야함
    }

    protected override void SetInstalled()
    {
        base.SetInstalled();

        isPlayed = false;
        StartCoroutine(RunningCoroutine());
    }

    private (bool iscatched, RaycastHit _raycastHit, Enemy _catchedEnemy) GetCathedEnemy()
    {
        bool catched = Physics.Raycast(transform.position, transform.up, out RaycastHit hit, 1f, _enemyLayer);
        if (catched)
        {
            return (catched, hit, hit.collider.GetComponent<Enemy>());
        }
        return (catched, default, null);
    }

    protected override void Running() //코루틴으로 해서 암것도 안 함
    {

    }

    private IEnumerator RunningCoroutine()
    {
        while (true)
        {
            if (isRunning)
            {
                var catchedInfo = GetCathedEnemy();

                if (catchedInfo.iscatched)
                {
                    CatchEnemy(catchedInfo._catchedEnemy, catchedInfo._raycastHit);
                    isPlayed = true;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    protected abstract void CatchEnemy(Enemy enemy, RaycastHit raycastHit);

}
