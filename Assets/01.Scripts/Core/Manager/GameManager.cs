using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    public int GetPenguinCount => FindObjectsOfType<BasicPenguin>().Length;
    public int GetEnemyPenguinCount => FindObjectsOfType<EnemyBasicPenguin>().Length;

    [SerializeField] private InitBuildingList buildingList = null;
    private Dictionary<string, Building> _buildingDictionary = new();

    [Header("스크립트들")]
    [SerializeField] private MainUI mainUI = null;

    //private void Start()
    //{
    //    buildingList.BuildingItems.ForEach(item =>
    //    {
    //        mainUI.SetBuildingItemUI(item.Name, item.Image);
    //        _buildingDictionary.Add(item.Name, item.BuildItem);
    //    });
    //    Init();
    //}

    public Building GetBuildingFormName(string buildingName)
    {
        if (_buildingDictionary.ContainsKey(buildingName))
        {
            Debug.Log($"{_buildingDictionary[buildingName]} 오브젝트가 성공적으로 소환됨");
            return _buildingDictionary[buildingName];
        }

        Debug.LogError("해당하는 이름의 건물은 존재하지 않습니다.");
        return null;
    }

    public Ray RayPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        return ray;
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }
}
