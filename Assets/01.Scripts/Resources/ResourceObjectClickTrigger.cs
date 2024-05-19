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
        if (!WaveManager.Instance.IsBattlePhase && UIManager.Instance.currentPopupUI.Count <= 0)
        {
            ResourceManager.Instance.SelectedResource = _resourceObject;
            UIManager.Instance.ShowPanel("ResourceUI");

            if (WaveManager.Instance.CurrentWaveCount < 4)
            {
                SignalHub.OnDefaultBuilingClickEvent?.Invoke();
            }
        }
    }
}
