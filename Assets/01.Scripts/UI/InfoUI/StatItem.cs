using TMPro;
using UnityEngine;

public interface IStatable
{
    public abstract void Modify(Stat stat, string statName);
}

public class StatItem : IStatable
{
    private TextMeshProUGUI _statName = null;
    private TextMeshProUGUI _percent = null;
    private TextMeshProUGUI _value = null;

    public StatItem(GameObject statItem, Transform parent)
    {
        GameObject obj = GameObject.Instantiate(statItem, parent);

        _statName = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _percent = obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _value = obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void Modify(Stat stat,string statName)
    {
        int percent = stat.GetStatPercent();
        SetPercentText(percent);

        _statName.text = statName;
        _value.text = stat._finalValue.ToString();
    }

    private void SetPercentText(int percent)
    {
        string arrow = "";
        if (percent > 0)
        {
            _percent.color = Color.green;
            arrow = "<size=20>¡ã</size>";
        }
        else if (percent < 0)
        {
            _percent.color = Color.green;
            arrow = "<size=20>¡å</size>";
        }
        else
        {
            _percent.color = Color.white;
            arrow = "";
        }
        _percent.text = $"({arrow} {percent.ToString()}%)";
    }
}
