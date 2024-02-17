using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public enum GroundOutlineColorType
{
    Green,
    Red,
    None
}

public class Ground : MonoBehaviour
{
    private bool isInstalledBuilding;

    public bool IsInstalledBuilding => isInstalledBuilding;

    private Outline _outline;
    public Outline OutlineCompo =>_outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    public void InstallBuilding() //땅에 설치되었다고 처리
    {
        isInstalledBuilding = true;
        _outline.enabled = false;
    }

    public void UpdateOutlineColor(GroundOutlineColorType type)
    {
        _outline.enabled = true;

        switch (type)
        {
            case GroundOutlineColorType.Green:
                _outline.OutlineColor = Color.green;
                break;
            case GroundOutlineColorType.Red:
                _outline.OutlineColor = Color.red;
                break;
            case GroundOutlineColorType.None:
                _outline.enabled = false;
                break;
        }
    }
}
