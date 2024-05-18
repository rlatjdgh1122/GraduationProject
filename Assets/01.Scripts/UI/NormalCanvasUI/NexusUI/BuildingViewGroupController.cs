using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingViewGroupController : MonoBehaviour
{
    private List<BuildingViewGroup> _buildingViewGroups;
    private BuildingViewGroup _selectedGroup;

    private void Awake()
    {
        _buildingViewGroups = GetComponentsInChildren<BuildingViewGroup>().ToList();
    }

    private void Start()
    {
        SwapElements(_buildingViewGroups[0]);
    }

    public void SwapElements(BuildingViewGroup viewGroup)
    {
        UIManager.Instance.ShowPanel($"{viewGroup.Category}");

        foreach (BuildingViewGroup group in _buildingViewGroups)
        {
            if (group.Category != viewGroup.Category)
            {
                UIManager.Instance.HidePanel($"{group.Category}");
            }    
        }
    }
}
