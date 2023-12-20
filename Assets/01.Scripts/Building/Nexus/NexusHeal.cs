using UnityEngine;

public class NexusHeal : MonoBehaviour
{
    [Header("스탯들")]
    [SerializeField] private float _rangeSize; //범위 크기
    [SerializeField] private float _tick; //몇초동안
    [SerializeField] private int _intensity; //얼만큼
    [Range(0f, 10f)]
    public float _noize; // 노이즈

    [Header("그 외")]
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

    private bool isChecked = false; //enter, stay, exit를 위한 변수(건들 필요 없음)
    private bool isHealing = false; //힐링을 할 것인가

    private float curTime = 0f;
    private void Update()
    {

        if (WaveManager.Instance.IsPhase == false) //웨이브가 끝나면
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
        Debug.Log("안에들어왔다");
        curTime = 0f;
        isHealing = true;
    }

    private void OnPenguinInsideRangeStay()
    {

        Debug.Log("안에있다");
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
                        //풀피면? 해줄필요가 없다!!!!!!!!!!!!!!!!!!!!!!!!!!
                        if (penguin.HealthCompo.IsMaxHP) continue;

                        //힐해주는 코드
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
        Debug.Log("안에나갔다");
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _rangeSize);
    }
}
