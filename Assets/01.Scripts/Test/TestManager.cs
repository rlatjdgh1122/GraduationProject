using UnityEngine;

public class TestManager : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private TestGroup[] _group;
    [SerializeField] private ShieldGeneralPenguin _shield;

    [SerializeField] private GameObject _camera;
    [SerializeField] private EnemyGorilla _enemyGorilla;

    private int _index = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _canvas.SetActive(false);
            _group[_index].SetTarget();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            _group[_index].gameObject.SetActive(false);
            _group[++_index].gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _shield.OnPassiveHitEvent();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _canvas.SetActive(false);
            _camera.SetActive(true);
        }
    }
}