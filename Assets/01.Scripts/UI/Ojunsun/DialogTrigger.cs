using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public void Trigger(DialogSystem system, string[] strings)
    {
        system.Begin(strings);
    }
}
