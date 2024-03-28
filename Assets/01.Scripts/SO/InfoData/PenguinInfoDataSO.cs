using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/InfoData/Penguin")]
public class PenguinInfoDataSO : EntityInfoDataSO
{
    public int Price;

    public Penguin _owner { get; private set; }

}
