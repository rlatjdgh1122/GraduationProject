using TMPro;
using UnityEngine;

public interface IStatable
{
    public abstract void Modify(Stat stat, string statName);
}

public class StatItem : IStatable
{
    private TextMeshProUGUI _statName = null;
    private TextMeshProUGUI _value = null;

    public StatItem(GameObject statItem, Transform parent)
    {
        GameObject obj = GameObject.Instantiate(statItem, parent);

        _statName = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _value = obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void Modify(Stat stat, string statName)
    {
        int percent = stat.GetStatPercent();
        string value = stat.GetValue().ToString();
        string percentTxt = SetPercentText(percent);

        _value.text = $"{percentTxt} {value}";
        _statName.text = statName;
    }

    private string SetPercentText(int percent)
    {
        string color = "";
        string arrow = "";

        if (percent > 0)
        {
            color = "green";
            arrow = "▲";
        }
        else if (percent < 0)
        {
            color = "red";
            arrow = "▼";
            percent *= -1; //마이너스 부호 빼기
        }
        else
            return string.Empty;

        return $"<size=30><color={color}>(<size=20>{arrow}</size> {percent}%)</color></size>";
    }
}
