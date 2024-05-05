using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public enum OutlineColorType
{
    Green,
    Red,
    None
}

[RequireComponent(typeof(Outline))]
public class Ground : MonoBehaviour
{
    private bool isInstalledBuilding;

    public bool IsInstalledBuilding => isInstalledBuilding;

    private Outline _outline;
    public Outline OutlineCompo =>_outline;

    private GroundMove _groundMove;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _groundMove = GetComponent<GroundMove>();
    }

    public void InstallBuilding() //땅에 설치되었다고 처리
    {
        isInstalledBuilding = true;
        UpdateOutlineColor(OutlineColorType.None);
    }

    public void UpdateOutlineColor(OutlineColorType type)
    {
        _outline.enabled = true;
        _outline.OutlineWidth = 2.0f;
        _outline.OutlineMode = Outline.Mode.OutlineAll;

        switch (type)
        {
            case OutlineColorType.Green:
                _outline.OutlineColor = Color.green;
                break;
            case OutlineColorType.Red:
                _outline.OutlineColor = Color.red;
                break;
            case OutlineColorType.None:
                _outline.enabled = false;
                break;
        }
    }

    public void SetGroundInfo(Transform parentTransform, Vector3 position)
    {
        _groundMove.SetGroundInfo(parentTransform,
                                  position);
    }

    public void SetMoveTarget(Transform trm)
    {
        _groundMove.SetMoveTarget(trm);
    }
}
