using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickParticleSystem : MonoBehaviour
{
    private ParticleSystem ClickParticle = null;

    private void Awake()
    {
        ClickParticle = GameObject.Find("ClickParticle").GetComponent<ParticleSystem>();
        if (ClickParticle == null) Debug.LogError("클릭 파티클 프리팹이 없습니다.");
    }

    public void OnPlayClickParticle(RaycastHit hit)
    {
        ClickParticle.transform.position = hit.point + new Vector3(0, 0.1f, 0);
        ClickParticle.Play();
    }
}
