using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObjectClickTrigger : MonoBehaviour
{
    ResourceObject _resourceObject;

    private void Awake()
    {
        _resourceObject = transform.GetComponent<ResourceObject>();
    }
    private void OnMouseDown()
    {
        if (!WaveManager.Instance.IsBattlePhase 
            && !LegionInventoryManager.Instance.CanUI
            && !NexusManager.Instance.CanClick)
        {
            if (WaveManager.Instance.CurrentWaveCount <= 2)
            {
                UIManager.Instance.ShowWarningUI("Ʃ�丮���� ������� �ʾҽ��ϴ�");
                return;
            }

            ResourceManager.Instance.SelectedResource = _resourceObject;
            UIManager.Instance.ShowPanel("ResourceUI", true);

            if (ResourceManager.Instance.IsFirst)
            {
                UIManager.Instance.GifController.ShowGif(GifType.WorkerPenguin);
                ResourceManager.Instance.IsFirst = false;
            }

            if (WaveManager.Instance.CurrentWaveCount < 4)
            {
                SignalHub.OnDefaultBuilingClickEvent?.Invoke();
            }
        }
    }
}
