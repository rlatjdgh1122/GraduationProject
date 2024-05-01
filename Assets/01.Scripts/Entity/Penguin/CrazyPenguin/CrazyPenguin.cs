using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrazyPenguin : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particle;

    private Transform _startTrm;
    private Transform _targetTrm;

    private CrazyPenguinAnimationTrigger _anim;
    private PenguinMove _penguinMove;

    private void Awake()
    {
        _penguinMove = GetComponent<PenguinMove>();
        _anim = transform.Find("Visual").GetComponent<CrazyPenguinAnimationTrigger>(); 
    }

    public void CreatePenguin(Transform startTrm, Transform targetTrm)
    {
        _startTrm = startTrm;
        _targetTrm = targetTrm;

       _penguinMove.MoveToPosition(_targetTrm.position);
    }

    private void Update()
    {
        _anim.JumpAnim(_penguinMove.MoveEnd());
    }

    public void CreateParticle()
    {
        ParticleSystem particle = Instantiate(_particle);
        particle.transform.position = _targetTrm.position + new Vector3(0, .8f, 0);
        particle.Play();
    }

    public void ReturnToSpawn()
    {
        _penguinMove.MoveToPosition(_startTrm.position);
    }
}