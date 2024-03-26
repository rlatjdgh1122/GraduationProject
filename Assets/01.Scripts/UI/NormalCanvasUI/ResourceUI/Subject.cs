using System.Collections.Generic;

public class Subject
{
    private List<IReceiver> _receiver = new List<IReceiver>();

    public void RegisterReceiver(IReceiver observer)
    {
        if (!_receiver.Contains(observer))
        {
            _receiver.Add(observer);
        }
    }

    public void RemoveReceiver(IReceiver observer)
    {
        if (_receiver.Contains(observer))
        {
            _receiver.Remove(observer);
        }
    }

    public void Notify<T>(T info)
    {
        foreach (var receiver in _receiver)
        {
            receiver.OnNotify(info);
        }
    }
}