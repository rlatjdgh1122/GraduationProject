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

    //private void Start()
    //{
    //    SwapElements(_buildingViewGroups[0]);
    //}

    public void SwapElements(BuildingViewGroup viewGroup)
    {
        UIManager.Instance.ShowPanel($"{viewGroup.Category}");

        foreach (BuildingViewGroup group in _buildingViewGroups)
        {
            if (group.Category != viewGroup.Category)
            {
                group.HidePanel();
            }    
        }

        foreach (BuildingViewGroup group in _buildingViewGroups) //< -이전에 했던 움직이는 코드
        {
            if (group.Category != viewGroup.Category && group.IsArrived)
            {
                UIManager.Instance.MovePanel($"{group.Category}", 0, -1400, 0.75f);
                group.IsArrived = false;
            }

            if (group.Category == viewGroup.Category && !group.IsArrived)
            {
                _selectedGroup = group;
                _selectedGroup.IsArrived = true;
                UIManager.Instance.MovePanel($"{_selectedGroup.Category}", 0, -100, 0.75f);
            }
        }
    }
}
