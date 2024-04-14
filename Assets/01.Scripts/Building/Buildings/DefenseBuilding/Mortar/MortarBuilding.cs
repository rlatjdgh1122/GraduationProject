using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBuilding : DefenseBuilding
{
    [SerializeField]
    private MortarRock _rockPrefab;

    [SerializeField]
    private Transform _firePos;

    private readonly string prefabName = "stone-small";

    private bool isFired;

    protected override void Running()
    {
    }

    protected override void Update()
    {
        if (WaveManager.Instance.IsBattlePhase) // 이것을 Running으로 옮길 것 입니다.
        {
            if (_currentTarget != null && !isFired)
            {
                StartCoroutine(FireRoutine());
            }
        }
    }

    private IEnumerator FireRoutine()
    {
        isFired = true;
        while (WaveManager.Instance.IsBattlePhase && _currentTarget != null)
        {
            Fire();
            yield return new WaitForSeconds(3f);
        }
        isFired = false;
    }

    private void Fire()
    {
        Debug.Log("?!");
        MortarRock rock = PoolManager.Instance.Pop(prefabName) as MortarRock;
        rock.transform.position = _firePos.position;
        StartCoroutine(rock.BulletMove(rock.transform.position, _currentTarget.position));

        SoundManager.Play3DSound(SoundName.MortarFire, _firePos.position);
    }
}
