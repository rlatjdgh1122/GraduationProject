using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public Dialog info;

    public void Trigger()
    {
        var system = FindObjectOfType<DialogSystem>();
        system.Begin(info);
    }
}
