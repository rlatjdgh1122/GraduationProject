using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePenguinBtn : MonoBehaviour
{
    [SerializeField] private PenguinStat data;

    public void CreatePenguin()
    {
        LegionInventory.Instance.AddPenguin(data);
    }
}
