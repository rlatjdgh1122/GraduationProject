using AssetKits.ParticleImage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationMarkParticle : PoolableMono
{
    private ParticleImage _exclamationMarkParticle;

    private void Awake()
    {
        _exclamationMarkParticle = GetComponent<ParticleImage>();
    }

    public void Play(Vector3 penguinPos)
    {
        Vector3 particlePos = new Vector3(penguinPos.x,
                                  penguinPos.y + 2f,
                                  penguinPos.z);

        _exclamationMarkParticle.transform.position = Camera.main.WorldToScreenPoint(particlePos);
        _exclamationMarkParticle.Play();

    }
}
