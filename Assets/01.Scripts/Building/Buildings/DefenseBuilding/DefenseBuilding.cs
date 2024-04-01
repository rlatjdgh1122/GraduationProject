using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class DefenseBuilding : BaseBuilding
{
    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float groundCheckRange;

    FieldOfView _fov;
    protected FieldOfView FOV => _fov;

    HashSet<Transform> _visibleTargets => _fov.FindVisibleTargets();

    private Dictionary<int, Ground> _groundOutlines = new();

    private HashSet<Ground> _previousGrounds = new();

    private HashSet<Ground> _removedGrounds = new();
    private HashSet<Ground> currentGrounds = new();

    private Health _health;

    protected override void Awake()
    {
        base.Awake();

        _fov = GetComponent<FieldOfView>();
        _health = GetComponent<Health>();

        _health.SetHealth(_characterStat);
        _health.enabled = false; // 설치 완료 되기 전까지는 공격 대상 X
    }

    protected override void Update()
    {
        base.Update();

        if (IsSelected)
        {
            Collider[] newColls = Physics.OverlapSphere(transform.position, groundCheckRange, _groundLayer);
            currentGrounds.Clear();

            foreach (Collider coll in newColls)
            {
                int id = coll.GetInstanceID();

                if (!_groundOutlines.TryGetValue(id, out Ground ground))
                {
                    ground = coll.GetComponent<Ground>();
                    _groundOutlines.Add(id, ground);
                }

                currentGrounds.Add(ground);

                if (ground.OutlineCompo.OutlineWidth < 10)
                {
                    ground.enabled = true;
                    ground.OutlineCompo.OutlineMode = Outline.Mode.OutlineVisible;
                    ground.UpdateOutlineColor(OutlineColorType.Red);
                    ground.OutlineCompo.OutlineWidth = 10.0f;
                }
            }

            _removedGrounds.Clear();
            _removedGrounds.UnionWith(_previousGrounds); // _removedGrounds에 _previousGrounds 추가
            _removedGrounds.ExceptWith(currentGrounds); //removedGrounds애서 currentGrounds에 있는 요소들을 제외 Linq의 Except보다 메모리소모가 적고 빠름.
            foreach (var ground in _removedGrounds)
            {
                ground.UpdateOutlineColor(OutlineColorType.None);
            }

            _previousGrounds.UnionWith(currentGrounds);
        }
    }

    public override void StopInstall()
    {
        base.StopInstall();

        foreach (var g in _groundOutlines.Values)
        {
            g.UpdateOutlineColor(OutlineColorType.None);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, groundCheckRange);
    }

    private void OnMouseEnter()
    {
        if (IsInstalled)
        {
            _health.OnUIUpdate?.Invoke(_health.currentHealth, _health.maxHealth);
        }
    }

    private void OnMouseExit()
    {
        if (IsInstalled)
        {
            _health.OffUIUpdate?.Invoke();
        }
    }

    protected override void SetInstalled()
    {
        base.SetInstalled();
        _health.enabled = true; // 설치 완료 되면 공격 대상 O
    }
}
