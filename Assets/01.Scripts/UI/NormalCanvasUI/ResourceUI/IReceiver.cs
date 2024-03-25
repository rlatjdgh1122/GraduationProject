public interface IReceiver
{
    void OnNotify<T>(T info);
}