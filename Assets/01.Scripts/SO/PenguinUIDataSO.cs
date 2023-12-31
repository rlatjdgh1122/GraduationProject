using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum PenguinUniqueType
{
    Fight,
    Production
}

public enum PenguinJobType
{
    WarLoard,
    Solider
}

[CreateAssetMenu(menuName = "SO/Penguin/UI")]
public class PenguinUIDataSO : ScriptableObject
{
    public PenguinUniqueType UniqueType;
    public PenguinJobType JobType;
    public PenguinTypeEnum PenguinType;
    public string PenguinName;
    public Sprite PenguinIcon;

    protected StringBuilder _stringBuilder = new StringBuilder();

    public virtual string GetDescription()
    {
        return string.Empty;
    }
}
