using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePenguinBtn : MonoBehaviour
{
    [SerializeField] private PenguinUIDataSO data;

    public void CreatePenguin()
    {
        LegionInventory.Instance.AddPenguin(data);
    }
}
