using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBuilding : DefenseBuilding
{
    [SerializeField]
    private MortarRock _rockPrefab;

    [SerializeField]
    private Transform _firePos;

    protected override void Running()
    {
        Debug.Log(_currentTarget);
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Fire();
        }
    }

    private void Fire()
    {
        MortarRock rock = PoolManager.Instance.Pop("stone-small") as MortarRock;
        rock.transform.position = _firePos.position;
        StartCoroutine(rock.BulletMove(rock.transform.position, rock.transform.position + new Vector3(Random.Range(-10f,10f),0, Random.Range(-10f, 10f))));
    }
}
