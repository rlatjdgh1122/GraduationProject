using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Penguin/UI")]
public class PenguinUIDataSO : ScriptableObject
{
    public string PenguinName;
    public Sprite PenguinIcon;

    protected StringBuilder _stringBuilder = new StringBuilder();

    public virtual string GetDescription()
    {
        return string.Empty;
    }
}
