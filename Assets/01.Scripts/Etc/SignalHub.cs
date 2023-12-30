using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ChangedArmy(Army prevArmy, Army newArmy);
public static class SignalHub
{
    public static ChangedArmy OnArmyChanged;

}
