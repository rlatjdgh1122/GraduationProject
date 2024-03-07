using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DefenseBuilding : BaseBuilding
{
    FieldOfView _fov;
    protected FieldOfView FOV => _fov;

    HashSet<Transform> _visibleTargets => _fov.FindVisibleTargets();

    private Dictionary<int, Ground> _groundOutlines = new();

    private HashSet<Ground> _previousGrounds = new();

    private HashSet<Ground> _removedGrounds = new();
    private HashSet<Ground> currentGrounds = new();


    [SerializeField]
    private LayerMask _groundLayer;

    protected override void Awake()
    {
        base.Awake();

        _fov = GetComponent<FieldOfView>();
    }

    protected override void Running()
    {
        
    }

    protected override void Update()
    {
        base.Update();

        if (IsSelected)
        {
            Collider[] newColls = Physics.OverlapSphere(transform.position, _fov.ViewRadius, _groundLayer);
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
                    ground.UpdateOutlineColor(GroundOutlineColorType.Red);
                    ground.OutlineCompo.OutlineWidth = 10.0f;
                }
            }

            _removedGrounds.Clear();
            _removedGrounds.UnionWith(_previousGrounds); // _removedGrounds에 _previousGrounds 추가
            _removedGrounds.ExceptWith(currentGrounds); //removedGrounds애서 currentGrounds에 있는 요소들을 제외 Linq의 Except보다 메모리소모가 적고 빠름.
            foreach (var ground in _removedGrounds)
            {
                ground.UpdateOutlineColor(GroundOutlineColorType.None);
            }

            _previousGrounds.UnionWith(currentGrounds);
        }
    }

    public override void StopInstall()
    {
        base.StopInstall();

        foreach (var g in _groundOutlines.Values)
        {
            g.UpdateOutlineColor(GroundOutlineColorType.None);
        }
    }

}
