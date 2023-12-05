using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMove : MonoBehaviour
{
    [SerializeField] Transform glacierPos;
    [SerializeField] GameObject hexagonPos;
    [SerializeField] float speed = 5f;

    bool isMoving => Vector3.Distance(glacierPos.position, hexagonPos.transform.position) > 0.1f;

    Outline _outline;
    Vector3 dir;
    Color startColor;
    Color targetColor;
    private void Awake()
    {
        hexagonPos = GameObject.Find("HexagonPos");
        _outline = GetComponent<Outline>();
    }

    private void Start()
    {
        startColor = new Color(_outline.OutlineColor.r, _outline.OutlineColor.g, _outline.OutlineColor.b, 0f);
        targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        dir = hexagonPos.transform.position - glacierPos.position;
        WaveManager.Instance.OnPhaseStartEvent += GroundMove;
    }

    private void GroundMove()
    {
        StartCoroutine(MoveCorou());
    }

    private IEnumerator MoveCorou()
    {
        while (isMoving)
        {
            transform.position += dir.normalized * speed * Time.deltaTime;
            yield return null;
        }

        // Fade in
        DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, targetColor, 0.7f)
            .OnComplete(() =>
            {
                // Fade out
                DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, startColor, 0.7f);
            });
    }

    private void Update()
    {
        
    }
}
