using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceDeadController : MonoBehaviour, IDeadable
{
    protected ResourceObject _owner;
    protected Collider _collider;
    protected GameObject _visual;

    private void Awake()
    {
        _owner = GetComponent<ResourceObject>();
        _collider = GetComponent<Collider>();
        _visual = transform.Find("Visual").gameObject;
    }

    public virtual void OnDied()
    {
        _owner.IsDead = true;
        _owner.enabled = false;
        _collider.enabled = false;

        _visual.SetActive(false);

        if (WaveManager.Instance.CurrentWaveCount < 5)
        {
            CoroutineUtil.CallWaitForOneFrame(() => MaskingUIManager.Instance.SetMaskingImagePos());
        }
    }

    public void OnResurrected()
    {
        _owner.IsDead = false;
        _owner.enabled = true;
        _collider.enabled = true;

        _visual.SetActive(true);
    }
}
