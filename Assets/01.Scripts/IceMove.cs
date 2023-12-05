using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMove : MonoBehaviour
{
    CinemachineVirtualCamera _virtualCam;
    [SerializeField] Transform glacierPos;
    [SerializeField] GameObject hexagonPos;
    [SerializeField] float speed = 5f;
    [SerializeField]
    private float cameraZoomFOV;
    private float originalFOV;

    bool isMoving => Vector3.Distance(glacierPos.position, hexagonPos.transform.position) > 0.1f;

    Outline _outline;
    Vector3 dir;
    Color startColor;
    Color targetColor;
    private void Awake()
    {
        hexagonPos = GameObject.Find("HexagonPos");
        _outline = GetComponent<Outline>();
        _virtualCam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        // æ∆øÙ∂Û¿Œ ƒ√∑Ø ∫Øºˆ √ ±‚»≠
        startColor = new Color(_outline.OutlineColor.r, _outline.OutlineColor.g, _outline.OutlineColor.b, 0f);
        targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        dir = hexagonPos.transform.position - glacierPos.position;
        originalFOV = _virtualCam.m_Lens.FieldOfView;

        WaveManager.Instance.OnPhaseStartEvent += GroundMoveHandle;
    }

    private void GroundMoveHandle()
    {
        WaveManager.Instance.SetCurrentEnemyGround(this);
        StartCoroutine(MoveCorou());
    }

    private IEnumerator MoveCorou()
    {
        while (isMoving) //¿Ãµø
        {
            transform.position += dir.normalized * speed * Time.deltaTime;
            yield return null;
        }

        // Fade in
        DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, targetColor, 0.7f)
            .OnComplete(() =>
            {
                // Fade out
                DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, startColor, 0.7f).OnComplete(() =>
                {
                    _virtualCam.LookAt = transform; // ¡‹ ∂Ø±Ë
                    DOTween.To(() => _virtualCam.m_Lens.FieldOfView, fov => _virtualCam.m_Lens.FieldOfView = fov,
                        cameraZoomFOV, 0.7f);
                });
            });
    }

    public void EndWave()
    {
        // ¡‹∂Ø∞Â¥¯∞≈ «ÆæÓ¡‹
        DOTween.To(() => _virtualCam.m_Lens.FieldOfView, fov => _virtualCam.m_Lens.FieldOfView = fov,
                        originalFOV, 0.7f);

        // ∫˘«œ ¿Ãµø ±∏µ∂ «ÿ¡¶
        WaveManager.Instance.OnPhaseStartEvent -= GroundMoveHandle;
    }

    private void OnEnable()
    {
        // ø¿∫Í¡ß∆Æ ∫Ò»∞º∫»≠ Ω√ ±∏µ∂ «ÿ¡¶
        WaveManager.Instance.OnPhaseStartEvent -= GroundMoveHandle;
    }
}
