using UnityEngine;

[CreateAssetMenu(menuName = "SO/PassiveData")]
public class PassiveDataSO : ScriptableObject
{
    //몇대 때릴때마다
    public bool IsAttackEvent = false;
    public int AttackCount = 3;

    //몇 초 마다
    public bool IsSecondEvent = false;
    public int Second = 10;

    //뒤에서 때릴때
    public bool IsBackAttack = false;

    //범위 안에 주변의 적이 몇명인가
    public bool IsAroundEnemyCountEvent = false;
    public float AroundRadius = 3;
    public int AroundEnemyCount = 3;
    public LayerMask CheckTarget;

    //현재체력이 몇퍼센트 이하일 경우
    public bool IsHealthRatioEvent = false;
    [Range(10f, 90f)]
    public float Ratio = 50f;

    //몇대 맞을때마다
    public bool IsHitEvent = false;
    public float HitCount = 5;


    private float _curTime = 0;
    private bool _isOverTime = false;

    /// <summary>
    /// 체력이 50이하 일 때 패시브 활성화 확인 여부
    /// </summary>>
    /// <returns> 결과</returns>

    /// ratio가 50일경우 현재체력 50% 이하일때 발동
    public bool CheckHealthRatioEventPassive(float maxHp, float currentHP, float ratio = -1)
    {
        // 현재 체력의 퍼센트 계산
        float healthPercent = (currentHP / maxHp) * 100f;

        if (ratio < 0) //임시
        {
            ratio = Ratio;
        }
        // 현재 체력이 지정된 비율보다 낮으면 true 반환
        if (healthPercent <= ratio)
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
        if (IsAttackEvent == false)
            return false;

        if (curAttackCount % AttackCount == 0)
            return true;

        return false;
    }

    /// <summary>
    /// 몇 대마다 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckHitEventPassive(int curHitCount)
    {
        if (IsHitEvent == false)
            return false;

        if (curHitCount % HitCount == 0)
            return true;

        return false;
    }

    public bool CheckSecondEventPassive()
    {
        bool result = _isOverTime;
        if (result)
            _isOverTime = false;
        return result;
    }

    public void Update()
    {
        if (!_isOverTime)
        {
            if (_curTime >= Second)
            {
                _curTime = 0;
                _isOverTime = true;
            }
            else
            {
                _curTime += Time.deltaTime;
            }
        }
    }
}
