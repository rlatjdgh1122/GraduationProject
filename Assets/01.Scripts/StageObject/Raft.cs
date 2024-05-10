using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : PoolableMono, IComingObject
{
    private Enemy[] _enemies;

    private Transform _elementsParent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnSink(); //Debug
        }
    }

    public void OnSink()
    {
        transform.DOMoveY(-15f, 10f).OnComplete(() => gameObject.SetActive(false));
    }

    public void SetComingObjectInfo(Transform parentTransform, Vector3 position, ComingElements groundElements)
    {
        //SetEnemies(groundElements.Enemies);
        //SetElementsParent(groundElements as GroundElements);
    }

    private void ActivateEnemies()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.NavAgent.enabled = true;
            enemy.IsMove = true;
        }
    }

    private void SetElementsParent(GroundElements elements)
    {
        foreach (var element in elements.Elements)
        {
            element.SetParent(_elementsParent);
        }
    }

    public void SetEnemies(Enemy[] enemies)
    {
        _enemies = enemies;
    }
}
