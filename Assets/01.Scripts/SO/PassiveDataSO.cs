using UnityEngine;

[CreateAssetMenu(menuName = "SO/PassiveData")]
public class PassiveDataSO : ScriptableObject
{
    //��� ����������
    public bool IsAttackEvent = false;
    public int AttackCount = 3;

    //�� �� ����
    public bool IsSecondEvent = false;
    public int Second = 10;

    //�ڿ��� ������
    public bool IsBackAttack = false;

    //���� �ȿ� �ֺ��� ���� ����ΰ�
    public bool IsAroundEnemyCountEventEvent = false;
    public float AroundRadius = 3;
    public int AroundEnemyCount = 3;
    public LayerMask CheckTarget;

    //����ü���� ���ۼ�Ʈ ������ ���
    public bool IsHealthRatioEventEvent = false;
    [Range(10f, 90f)]
    public float Ratio = 50f;


    private float _curTime = 0;
    private bool _isOverTime = false;

    /// <summary>
    /// ü���� 50���� �� �� ��ú� Ȱ��ȭ Ȯ�� ����
    /// </summary>>
    /// <returns> ���</returns>

    /// ratio�� 50�ϰ�� ����ü�� 50% �����϶� �ߵ�
    public bool CheckHealthRatioEventPassive(float maxHp, float currentHP)
    {
        // ���� ü���� �ۼ�Ʈ ���
        float healthPercent = (currentHP / maxHp) * 100f;

        // ���� ü���� ������ �������� ������ true ��ȯ
        if (healthPercent <= Ratio)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// �� �븶�� �нú� Ȱ��ȭ Ȯ�� ����
    /// </summary>
    /// <returns> ���</returns>
    public bool CheckAttackEventPassive(int curAttackCount)
    {
        if (IsAttackEvent == false)
            return false;

        if (curAttackCount % AttackCount == 0)
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
