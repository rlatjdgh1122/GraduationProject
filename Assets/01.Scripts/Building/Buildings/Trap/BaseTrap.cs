using DG.Tweening;
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

    private float dissapearTime = 2f;

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
            }

        }
    }

    protected abstract void CatchEnemy(Enemy enemy, RaycastHit raycastHit);

    public virtual void RemoveTrap()
    {
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            StartCoroutine(ChangeToTransparentMatCorou(_meshRenderers[i].material));
        }
    }

    private IEnumerator ChangeToTransparentMatCorou(Material material)
    {
        float current = 0;
        float percent = 0;
        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / dissapearTime;

            float alpha = Mathf.Lerp(1f, 0f, percent);
            material.color = new Color(0,0,0, alpha);

            Debug.Log($"{material}: {alpha}");
            Debug.Log($"Color {material.color}");

            yield return new WaitForEndOfFrame();
        }

        PoolManager.Instance.Push(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + (Vector3.down * catchRange) + (transform.up * catchRange), catchRange);
    }
}
