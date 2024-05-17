using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainInterfaceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _workerText;
    [SerializeField] private TextMeshProUGUI _stoneText;
    [SerializeField] private TextMeshProUGUI _woodText;

    private void Awake()
    {
        WorkerManager.Instance.OnUIUpdate += UpdateWorkerUI;
        ResourceManager.Instance.OnUIUpdate += UpdateResourceUI;
    }

    private void Start()
    {
        UpdateWorkerUI(WorkerManager.Instance.MaxWorkerCount);
        UpdateResourceUI(null, 0);
    }

    private void UpdateWorkerUI(int count)
    {
        _workerText.text = $"{count}";
    }

    private void UpdateResourceUI(Resource resource, int count)
    {
        if (resource == null)
        {
            _stoneText.text = $"{count}";
            _woodText.text = $"{count}";
            return;
        }

        if (resource.resourceData.resourceType == ResourceType.Stone)
        {
            _stoneText.text = $"{resource.stackSize}";
        }
        else if (resource.resourceData.resourceType == ResourceType.Wood)
        {
            _woodText.text = $"{resource.stackSize}";
        }
    }

    public void ShowViewGifsUI()
    {
        UIManager.Instance.ShowPanel("ViewGifsUI");
    }

    public void ShowEscUI()
    {
        UIManager.Instance.ShowPanel("EscUI");
    }
}
