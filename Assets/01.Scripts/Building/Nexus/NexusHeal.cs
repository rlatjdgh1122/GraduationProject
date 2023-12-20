using UnityEngine;

public class NexusHeal : MonoBehaviour
{
    [Header("���ȵ�")]
    [SerializeField] private float _rangeSize; //���� ũ��
    [SerializeField] private float _tick; //���ʵ���
    [SerializeField] private int _intensity; //��ŭ
    [Range(0f, 10f)]
    public float _noize; // ������

    [Header("�� ��")]
    [SerializeField] private LayerMask _healingTargetLayer;

    public float Tick
    {
        get
        {
            return _tick;
        }
        set
        {
            var noizeValue = value + Random.Range(-_noize, _noize);
            _tick = Mathf.Clamp(noizeValue, 0.1f, 30f);
        }
    }

    public int Intensity
    {
        get
        {
            return _intensity;
        }
        set
        {
            var noizeValue = value + Random.Range(-_noize, _noize);
            _intensity = Mathf.Clamp((int)noizeValue, 1, 100);
        }
    }

    private Collider[] _colls;

    private bool isChecked = false; //enter, stay, exit�� ���� ����(�ǵ� �ʿ� ����)
    private bool isHealing = false; //������ �� ���ΰ�

    private float curTime = 0f;
    private void Update()
    {

        if (WaveManager.Instance.IsPhase == false) //���̺갡 ������
        {
            _colls = Physics.OverlapSphere(transform.position, _rangeSize, _healingTargetLayer);
            if (_colls.Length > 0)
            {
                if (isChecked == true)
                {
                    OnPenguinInsideRangeStay();
                }
                else
                {
                    OnPenguinInsideRangeEnter();
                }
                isChecked = true;
            }
            else
            {
                if (isChecked == true)
                {
                    OnPenguinInsideRangeExit();
                }
                isChecked = false;
            }
        }
    }
    private void OnPenguinInsideRangeEnter()
    {
        Debug.Log("�ȿ����Դ�");
        curTime = 0f;
        isHealing = true;
    }

    private void OnPenguinInsideRangeStay()
    {

        Debug.Log("�ȿ��ִ�");
        curTime += Time.deltaTime;

        if (isHealing == true)
        {
            curTime += Time.deltaTime;

            if (curTime >= Tick)
            {
                foreach (Collider coll in _colls)
                {
                    if (isHealing == false) break;

                    if (coll.TryGetComponent<Penguin>(out var penguin))
                    {
                        //Ǯ�Ǹ�? �����ʿ䰡 ����!!!!!!!!!!!!!!!!!!!!!!!!!!
                        if (penguin.HealthCompo.IsMaxHP) continue;

                        //�����ִ� �ڵ�
                        penguin?.HealthCompo.ApplyHeal(Intensity);
                        penguin?.HealEffect.Play();
                    }
                }

                curTime = 0f;
            }
        }

    }

    private void OnPenguinInsideRangeExit()
    {
        curTime = 0f;
        isHealing = false;
        Debug.Log("�ȿ�������");
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _rangeSize);
    }
}
