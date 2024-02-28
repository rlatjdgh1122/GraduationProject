using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class NexusBase : MonoBehaviour
{
    #region 컴포넌트
    [SerializeField] private NexusStat _nexusStat;
    private Health _health;
    #endregion

    #region 프로퍼티
    public NexusStat NexusStat => _nexusStat;
    public Health HealthCompo => _health;
    #endregion

    [SerializeField]
    private InputReader _reader;

    private NexusUI _nexusUI
    {
        get
        {
            UIManager.Instance.overlayUIDictionary.TryGetValue(UIType.Nexus, out NormalUI nexusUI);
            return (NexusUI)nexusUI;
        }
    }

    private void Awake()
    {
        _health = GetComponent<Health>();

        _health.SetHealth(_nexusStat);
    }

    public void LevelUp()
    {
        _nexusStat.maxHealth.AddSum(_nexusStat.maxHealth.GetValue(), _nexusStat.level, _nexusStat.levelupIncreaseValue);
        _nexusStat.level++;
        _nexusStat.upgradePrice *= 2; // <-이건 임시

        _nexusUI.UpdateText();
    }

    private void OnMouseDown()
    {
        if (!WaveManager.Instance.IsBattlePhase && !_reader.IsPointerOverUI())
        {
            _nexusUI.EnableUI(0.5f, null);
        }
    }

    private void OnMouseEnter()
    {
        if (!WaveManager.Instance.IsBattlePhase && !_reader.IsPointerOverUI())
        {
            _health.OnUIUpdate?.Invoke(_health.currentHealth, _health.maxHealth);
        }
    }

    private void OnMouseExit()
    {
        _health.OffUIUpdate?.Invoke();
    }
}
