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
            && !UIManager.Instance.GifController.CanShow
            && !LegionInventoryManager.Instance.CanUI
            && !NexusManager.Instance.CanClick
            && !ResourceManager.Instance.OnResourceUI)
        {
            if (WaveManager.Instance.CurrentWaveCount <= 2
                || (WaveManager.Instance.CurrentWaveCount == 3 && _resourceObject.ResourceData.resourceType == ResourceType.Wood))
            {
                UIManager.Instance.ShowWarningUI("튜토리얼이 진행되지 않았습니다");
                return;
            }

            if (WaveManager.Instance.CurrentWaveCount == 4 && _resourceObject.ResourceData.resourceType == ResourceType.Stone)
            {
                UIManager.Instance.ShowWarningUI("나무를 캐주세요");
                return;
            }

            ResourceManager.Instance.SelectedResource = _resourceObject;
            UIManager.Instance.ShowPanel("ResourceUI", true);

            if (ResourceManager.Instance.IsGifFirst)
            {
                UIManager.Instance.GifController.ShowGif(GifType.WorkerPenguin);
                ResourceManager.Instance.IsGifFirst = false;
            }

            ResourceManager.Instance.OnResourceUI = true;

            if (WaveManager.Instance.CurrentWaveCount < 4)
            {
                SignalHub.OnDefaultBuilingClickEvent?.Invoke();
            }
        }
    }
}
