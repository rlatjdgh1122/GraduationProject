using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PassiveData")]
public class PassiveDataSO : ScriptableObject
{
    //��� ����������
    public bool IsAttackEvent = false;
    public int AttackCount = 3;

    //�� �� ����
    public bool IsSecondEvent = false;
    public float EverySecond = 10f;

    //�ڿ��� ������
    public bool IsBackAttack = false;

    //���� �ȿ� �ֺ��� ���� ����ΰ�
    public bool IsAroundEnemyCountEventEvent = false; 
    public float AroundRadius = 3;
    public int AroundEnemyCount = 3;

    public LayerMask CheckTarget;

    private Entity Owner = null;

    private float curTime = 0f;
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

    public void SetOwner(Entity obj)
    {
        Owner = obj;
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

    /// <summary>
    /// �� �ʸ��� �нú� Ȱ��ȭ Ȯ�� ����
    /// </summary>
    /// <returns> ���</returns>
    public bool CheckSecondEventPassive(float curTime) => IsSecondEvent;

    /// <summary>
    /// ��ġ�� �нú� Ȱ��ȭ Ȯ�� ����
    /// </summary>
    /// <returns> ���</returns>
    public bool CheckBackAttackEventPassive() => IsAttackEvent;

    /// <summary>
    /// �ֺ��� �� �� ��� �нú� Ȱ��ȭ Ȯ�� ����
    /// </summary>
    /// <returns> ���</returns>
    public bool CheckAroundEnemyCountEventPassive() => IsAttackEvent;

}
