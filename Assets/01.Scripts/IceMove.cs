using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMove : MonoBehaviour
{
    [SerializeField] Transform glacierPos;
    [SerializeField] GameObject hexagonPos;
    [SerializeField] float speed = 5f;

    Vector3 dir;

    private void Start()
    {
        hexagonPos = GameObject.Find("HexagonPos");
        dir = hexagonPos.transform.position - glacierPos.position;
    }

    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
        dir.Normalize();

        if (Vector3.Distance(glacierPos.position, hexagonPos.transform.position) < 0.1f)
        {
            speed = 0;
            Debug.Log("½Î¿öº¼°Ô¿ë");
        }
    }
}
