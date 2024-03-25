using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUIReceiver : MonoBehaviour, IReceiver
{
    [SerializeField] private TextMeshProUGUI _resourceNameText;
    [SerializeField] private TextMeshProUGUI _recieveCountText;
    [SerializeField] private TextMeshProUGUI _needWorkerCountText;
    [SerializeField] private TextMeshProUGUI _currentWorkerCountText;
    [SerializeField] private Image _resourceIcon;
    [SerializeField] private TextMeshProUGUI _warningText;
    [SerializeField] private CanvasGroup _warningPanel;

    public void OnNotify<T>(T info)
    {
        ResourceObject resource = info as ResourceObject;

        _resourceNameText.text = resource.ResourceName;
        _resourceIcon.sprite = resource.ResourceImage;
        _recieveCountText.text = resource.ReceiveCountWhenCompleted.ToString();
        _needWorkerCountText.text = $"�ּ� �ϲ� {resource.RequiredWorkerCount}���� �ʿ�";
        _currentWorkerCountText.text = resource.CurrentWorkerCount.ToString();
    }
}
