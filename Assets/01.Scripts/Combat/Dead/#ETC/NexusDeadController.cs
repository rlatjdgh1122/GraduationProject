using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDeadController : MonoBehaviour, IDeadable
{
    public void OnDied()
    {
        UIManager.Instance.ShowPanel("DefeatUI");
    }

    public void OnResurrected()
    {

    }
}
