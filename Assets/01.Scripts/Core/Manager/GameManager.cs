using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private InitBuildingList buildingList = null;
    private Dictionary<string, Build> _buildingDictionary = new();

    [Header("��ũ��Ʈ��")]
    [SerializeField] private MainUI mainUI = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else Destroy(this);

        buildingList.BuildItems.ForEach(item =>
        {
            mainUI.SetBuildItemUI(item.Name, item.Image);
            _buildingDictionary.Add(item.Name, item.BuildItem);
        });
    }
    public Build GetBuildFormName(string buildingName)
    {
        if (_buildingDictionary.ContainsKey(buildingName))
        {
            Debug.Log($"{_buildingDictionary[buildingName]} ������Ʈ�� ���������� ��ȯ��");
            return _buildingDictionary[buildingName];
        }

        Debug.LogError("�ش��ϴ� �̸��� �ǹ��� �������� �ʽ��ϴ�.");
        return null;
    }
}
