using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LegionInventory : InitLegionInventory
{
    public List<LegionInventoryData> _beforeSaveLegionList;

    public Dictionary<int, LegionInventoryData> _saveDictionary;
    public List<LegionInventoryData> _savedLegionList;



    public void LegionRegistration(LegionInventoryData data)
    {
        if(_saveDictionary.TryGetValue(data.IndexNumber, out LegionInventoryData saveData))
        {
            if(data == saveData)
            {

            }
        }       
    }


    public void SaveLegion()
    {
        foreach(var data in _beforeSaveLegionList)
        {

        }
    }
}