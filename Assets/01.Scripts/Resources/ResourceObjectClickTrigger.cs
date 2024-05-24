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
        if (WaveManager.Instance.CurrentWaveCount <= 2
                || (WaveManager.Instance.CurrentWaveCount == 3 && _resourceObject.ResourceData.resourceType == ResourceType.Wood))
        {
            UIManager.Instance.ShowWarningUI("Ʃ�丮���� ������� �ʾҽ��ϴ�");
            return;
        }

        if (WaveManager.Instance.CurrentWaveCount == 3 && ResourceManager.Instance.OnlyGetOneStone)
        {
            UIManager.Instance.ShowWarningUI("���� ���̺꿡���� �� �ϳ��� ĳ�� �� �ֽ��ϴ�");
            return;
        }

        if (WaveManager.Instance.CurrentWaveCount == 4 && ResourceManager.Instance.OnlyGetOneWood)
        {
            UIManager.Instance.ShowWarningUI("���� ���̺꿡���� ���� �ϳ��� ĳ�� �� �ֽ��ϴ�");
            return;
        }

        if (WaveManager.Instance.CurrentWaveCount == 4 && _resourceObject.ResourceData.resourceType == ResourceType.Stone)
        {
            UIManager.Instance.ShowWarningUI("������ ĳ�ּ���");
            return;
        }

        if (!WaveManager.Instance.IsBattlePhase
            && !UIManager.Instance.GifController.CanShow
            && !LegionInventoryManager.Instance.CanUI
            && !NexusManager.Instance.CanClick
            && !ResourceManager.Instance.OnResourceUI
            && !TutorialManager.Instance.ShowTutorialQuest)
        {
            ResourceManager.Instance.SelectedResource = _resourceObject;
            UIManager.Instance.ShowPanel("ResourceUI", true);

            if (ResourceManager.Instance.IsGifFirst)
            {
                UIManager.Instance.GifController.ShowGif(GifType.WorkerPenguin);
                ResourceManager.Instance.IsGifFirst = false;
            }
            if (WaveManager.Instance.CurrentWaveCount == 3)
                ResourceManager.Instance.OnlyGetOneStone = true;
            if (WaveManager.Instance.CurrentWaveCount == 4)
                ResourceManager.Instance.OnlyGetOneWood = true;

            ResourceManager.Instance.OnResourceUI = true;

            if (WaveManager.Instance.CurrentWaveCount < 5)
            {
                SignalHub.OnDefaultBuilingClickEvent?.Invoke();
            }
        }
    }
}
