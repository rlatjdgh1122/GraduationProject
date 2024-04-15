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

    private ParticleSystem _mortarFireParticle;

    protected override void Awake()
    {
        base.Awake();
        _mortarFireParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
    }


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
        MortarRock rock = PoolManager.Instance.Pop(prefabName) as MortarRock;
        _mortarFireParticle.Play();
        rock.transform.position = _firePos.position;
        StartCoroutine(rock.BulletMove(rock.transform.position, _currentTarget.position));

        SoundManager.Play3DSound(SoundName.MortarFire, _firePos.position);

        
    }
}
