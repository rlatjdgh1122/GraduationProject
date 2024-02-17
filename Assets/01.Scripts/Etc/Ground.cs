using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool isInstalledBuilding;

    public bool IsInstalledBuilding => isInstalledBuilding;

    private MaterialPropertyBlock _materialPropertyBlock;

    private MeshRenderer _meshRenderer;

    private Color _normalColor;

    private Color _greenColor = new Color(0, 250, 154, 255);
    private Color _redColor = new Color(255, 99, 71, 255);

    public bool IsRedMT => _materialPropertyBlock.GetColor("_Color") == _redColor;
    public bool IsGreenMT => _materialPropertyBlock.GetColor("_Color") == _greenColor;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _normalColor = _meshRenderer.material.color;
    }

    public void InstallBuilding() //땅에 설치되었다고 처리
    {
        isInstalledBuilding = true;

        _materialPropertyBlock.SetColor("_Color", _normalColor);
        _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
    }

    public void ShowInstallPossibility(bool canInstall)
    {
        if(canInstall) //설치가능하면
        {
            _materialPropertyBlock.SetColor("_Color", _greenColor);
            _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
        }

        else if(!canInstall) //설치 할 수 없으면
        {
            _materialPropertyBlock.SetColor("_Color", _redColor);
            _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
        }

        Debug.Log($"{canInstall}: {_materialPropertyBlock.GetColor("_Color")}");
    }
}
