using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private static WaveManager _instance;

    public static WaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WaveManager>();

                if (_instance == null)
                {
                    Debug.LogError("WaveManager �ν��Ͻ��� ã�� �� �����ϴ�.");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
