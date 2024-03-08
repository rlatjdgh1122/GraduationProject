using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class NexusBase : MonoBehaviour
{
    #region components
    [SerializeField] private NexusStat _nexusStat;
    private Health _health;
    #endregion

    #region property
    public NexusStat NexusStat => _nexusStat;
    public Health HealthCompo => _health;
    #endregion

    [SerializeField]
    private InputReader _input;

    private void Awake()
    {
        _health = GetComponent<Health>();

        _health.SetHealth(_nexusStat);
    }

    private void OnMouseDown()
    {
        if (!WaveManager.Instance.IsBattlePhase && !_input.IsPointerOverUI())
        {
            UIManager.Instance.ShowPanel("NexusUI");
        }
    }

    private void OnMouseEnter()
    {
        if (!WaveManager.Instance.IsBattlePhase && !_input.IsPointerOverUI())
        {
            _health.OnUIUpdate?.Invoke(_health.currentHealth, _health.maxHealth);
        }
    }

    private void OnMouseExit()
    {
        _health.OffUIUpdate?.Invoke();
    }
}
