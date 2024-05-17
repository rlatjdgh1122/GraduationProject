using UnityEngine;

public class NexusBase : MonoBehaviour
{
    private NexusStat _nexusStat;
    private Health _health;
    public Health HealthCompo => _health;

    private LayerMask _groundLayer = 1 << 3;


    private void Start()
    {
        _nexusStat = NexusManager.Instance.NexusStat;

        _health = GetComponent<Health>();
        _health.SetHealth(_nexusStat);

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            hit.collider.transform.parent.GetComponent<Ground>().InstallBuilding();
        }
    }

    private void OnMouseDown()
    {
        if (!WaveManager.Instance.IsBattlePhase)
        {
            UIManager.Instance.ShowPanel("NexusUI");
            SignalHub.OnDefaultBuilingClickEvent?.Invoke();
        }
    }

    private void OnMouseOver()
    {
        if (!WaveManager.Instance.IsBattlePhase)
        {
            _health.OnUIUpdate?.Invoke(_health.currentHealth, _health.maxHealth);
        }
    }
}
