using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private InitBuildingList buildingList = null;
    private Dictionary<string, Building> _buildingDictionary = new();

    [Header("��ũ��Ʈ��")]
    [SerializeField] private MainUI mainUI = null;
    private void Start()
    {
        buildingList.BuildingItems.ForEach(item =>
        {
            mainUI.SetBuildingItemUI(item.Name, item.Image);
            _buildingDictionary.Add(item.Name, item.BuildItem);
        });
        Init();
    }
    public Building GetBuildingFormName(string buildingName)
    {
        if (_buildingDictionary.ContainsKey(buildingName))
        {
            Debug.Log($"{_buildingDictionary[buildingName]} ������Ʈ�� ���������� ��ȯ��");
            return _buildingDictionary[buildingName];
        }

        Debug.LogError("�ش��ϴ� �̸��� �ǹ��� �������� �ʽ��ϴ�.");
        return null;
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }
}
