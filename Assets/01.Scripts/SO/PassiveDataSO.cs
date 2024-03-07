using UnityEditor;
using UnityEngine;


//[ExecuteInEditMode]
[CreateAssetMenu(menuName = "SO/PassiveData")]
public class PassiveDataSO : ScriptableObject
{
    //몇대 때릴때마다
    public bool IsAttackEvent = false;
    public int AttackCount = 3;

    //몇 초 마다
    public bool IsSecondEvent = false;
    public float EverySecond = 10f;

    //뒤에서 때릴때
    public bool IsBackAttack = false;

    //범위 안에 주변의 적이 몇명인가
    public bool IsAroundEnemyCountEventEvent = false; 
    public float AroundRadius = 3;
    public int AroundEnemyCount = 3;

    public LayerMask CheckTarget;

    private General Owner = null;

    private float curTime = 0f;

    private void Awake()
    {
        EditorUtility.SetDirty(this);
    }
    public void Start()
    {
        
    }
    public void Update()
    {
        if (IsSecondEvent)
        {
            curTime += Time.deltaTime;
            if (curTime > EverySecond)
            {
                Owner.OnPassiveSecondEvent();
                curTime = 0;
            }
        }

        if (IsAroundEnemyCountEventEvent)
        {
/*            var colls = Physics.OverlapSphere(Owner.transform.position, AroundRadius, CheckTarget);

            if (colls.Length == AroundEnemyCount)
            {
                if (colls.Length == AroundEnemyCount)
                    Owner.OnPassiveAroundEvent();
            }
            else
            {

            }*/
        }
    }

    public void SetOwner(General obj)
    {
        Owner = obj;
    }

    /// <summary>
    /// 체력이 50이하 일 때 페시브 활성화 확인 여부
    /// </summary>>
    /// <returns> 결과</returns>
    public bool CheckStunEventPassive(float maxHp, float currentHP)
    {
        if(maxHp / 2 > currentHP)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 몇 대마다 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckAttackEventPassive(int curAttackCount)
    {

        Debug.Log(IsAttackEvent);
        if (IsAttackEvent == false)
            return false;

        if (curAttackCount % AttackCount == 0)
            return true;

        return false;
    }

    /// <summary>
    /// 몇 초마다 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckSecondEventPassive(float curTime) => IsSecondEvent;

    /// <summary>
    /// 뒤치기 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckBackAttackEventPassive() => IsAttackEvent;

    /// <summary>
    /// 주변의 적 수 비례 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckAroundEnemyCountEventPassive() => IsAttackEvent;
}
