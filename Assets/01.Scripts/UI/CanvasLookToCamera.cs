using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookToCamera : MonoBehaviour
{
    [SerializeField] private List<GameObject> _container;
    private Canvas _canvas;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;

        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = _cam;
    }

    private void Update()
    {
        foreach (GameObject container in _container)
        {
            if (container != null)
            {
                container.transform.rotation = Quaternion.LookRotation(container.transform.position - _cam.transform.position);
            }
        }
    }
}
