using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildlItemUI
{
    private string buildingName = null;
    public BuildlItemUI(VisualElement _root, string _buildingName, Sprite _image)
    {
        buildingName = _buildingName;
        _root.Q<Label>("builditem-name").text = buildingName;

        var btn = _root.Q<Button>("btn-builditem");
        btn.style.backgroundImage = new StyleBackground(_image);
        btn.RegisterCallback<ClickEvent>(OnButtonClick);

    }

    private void OnButtonClick(ClickEvent evt)
    {
        GameManager.Instance.GetBuildFormName(buildingName);
    }
}
