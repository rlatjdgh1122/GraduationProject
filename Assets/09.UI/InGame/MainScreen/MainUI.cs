using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class MainUI : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset buildItem_template;

    private VisualElement _root = null;
    private ScrollView _buildItemContainer;
    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _buildItemContainer = _root.Q<ScrollView>("builditem-container"); 
    }
    public void SetBuildItemUI(string name, Sprite image)
    {
        Debug.Log("����");
        VisualElement template = buildItem_template.Instantiate().Q<VisualElement>("builditem-template");

        _buildItemContainer.Add(template);
        Debug.Log(template);    

        BuildlItemUI buidlitemUI = new(template, name, image);
    }
}
