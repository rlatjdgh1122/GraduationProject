using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObjectClickTrigger : MonoBehaviour
{
    ResourceObject _resourceObject;

    private bool isFirst = true;

    private void Awake()
    {
        _resourceObject = transform.GetComponent<ResourceObject>();
    }
    private void OnMouseDown()
    {
        if (!WaveManager.Instance.IsBattlePhase && (UIManager.Instance.currentPopupUI.Count <= 0
            || UIManager.Instance.currentPopupUI.Peek().name == "Masking"))
        {
            ResourceManager.Instance.SelectedResource = _resourceObject;
            UIManager.Instance.ShowPanel("ResourceUI");

            if (isFirst)
            {
                UIManager.Instance.GifController.ShowGif(GifType.WorkerPenguin);
                isFirst = false;
            }

            if (WaveManager.Instance.CurrentWaveCount < 4)
            {
                SignalHub.OnDefaultBuilingClickEvent?.Invoke();
            }
        }
    }
}
