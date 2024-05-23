using UnityEngine;

public class NexusBase : MonoBehaviour
{
    private NexusStat _nexusStat;
    private Health _health;
    public Health HealthCompo => _health;

    private LayerMask _groundLayer = 1 << 3;

    private bool isFirst = true;


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
        if (!WaveManager.Instance.IsBattlePhase
            && !LegionInventoryManager.Instance.CanUI
            && !UIManager.Instance.GifController.CanShow
            && !TutorialManager.Instance.ShowTutorialQuest)
        {
            if (WaveManager.Instance.CurrentWaveCount <= 2)
            {
                UIManager.Instance.ShowWarningUI("튜토리얼이 진행되지 않았습니다");
                return;
            }

            if(!ResourceManager.Instance.GetStone)
            {
                UIManager.Instance.ShowWarningUI("돌이 캐질 때까지 기다려 주세요");
                return;
            }

            if (isFirst)
            {
                UIManager.Instance.GifController.ShowGif(GifType.NexusUpgrade);
                isFirst = false;
            }

            UIManager.Instance.ShowPanel("NexusUI", true);
            SignalHub.OnDefaultBuilingClickEvent?.Invoke();
            NexusManager.Instance.CanClick = true;
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