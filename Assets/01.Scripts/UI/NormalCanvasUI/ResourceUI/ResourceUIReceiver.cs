using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUIReceiver : MonoBehaviour, IReceiver
{
    [SerializeField] private TextMeshProUGUI _resourceNameText;
    [SerializeField] private TextMeshProUGUI _recieveCountText;
    [SerializeField] private TextMeshProUGUI _needWorkerCountText;
    [SerializeField] private TextMeshProUGUI _currentWorkerCountText;
    [SerializeField] private Image _workerIcon;
    [SerializeField] private Image _resourceIcon;

    public void OnNotify<T>(T info)
    {
        ResourceObject resource = info as ResourceObject;

        _resourceNameText.text = resource.ResourceName;
        _workerIcon.sprite = resource.WorkerIcon;
        _resourceIcon.sprite = resource.ResourceImage;
        _recieveCountText.text = resource.ReceiveCountWhenCompleted.ToString();
        _needWorkerCountText.text = $"최소 일꾼 {resource.RequiredWorkerCount}마리 필요";
        _currentWorkerCountText.text = resource.CurrentWorkerCount.ToString();
    }
}
