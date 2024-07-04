using System.Collections.Generic;

public interface IValueChanger<T>
{

    public List<IValueChangeUnit<T>> Units { get; set; }

    public T Value
    {
        get;
    }

    public void ChangedValue()
    {

        foreach (var item in Units)
        {

            item.ChangedValue(Value);

        }
    }

    public void Add(IValueChangeUnit<T> v)
    {

        Units.Add(v);

    }

    public void Remove(IValueChangeUnit<T> v)
    {

        Units.Remove(v);

    }

}