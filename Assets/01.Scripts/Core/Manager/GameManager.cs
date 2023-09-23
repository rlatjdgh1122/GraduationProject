using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private InitBuildingList buildingList = null;
    private Dictionary<string, Build> _buildingDictionary = new();

    [Header("스크립트들")]
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
            Debug.Log($"{_buildingDictionary[buildingName]} 오브젝트가 성공적으로 소환됨");
            return _buildingDictionary[buildingName];
        }

        Debug.LogError("해당하는 이름의 건물은 존재하지 않습니다.");
        return null;
    }
}
