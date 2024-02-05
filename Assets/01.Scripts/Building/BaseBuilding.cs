using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Health))]
public abstract class BaseBuilding : PoolableMono
{
    private bool isSelected;
    private bool isPlaced;


    [SerializeField]
    private Material[] _materials;

    private Grid _grid;
    
    private MeshRenderer _meshRenderer;

    private Coroutine _followMousePositionCoroutine;
    
    private Vector3 _mousePos => Input.mousePosition;


    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _grid = GetComponent<Grid>();
    }

    public void SetSelect()
    {
        isSelected = true;
        _meshRenderer.material = _materials[1];

        _followMousePositionCoroutine = StartCoroutine(FollowMousePosition());
    }

    private IEnumerator FollowMousePosition()
    {
        while (true)
        {
            if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
                                                out var hit, Mathf.Infinity))
            {
                Vector3Int gridPosition = _grid.WorldToCell(hit.point);
                transform.position = _grid.CellToWorld(gridPosition);
            }

            yield return 0.1f;
        }
    }

    public void Deselect()
    {
        isSelected = false;
        _meshRenderer.material = _materials[0];


    }

}
