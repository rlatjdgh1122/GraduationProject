using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSynergyBuilding : DeadBuilding
{
    private bool isDead;
    public override void DisolveBuilding()
    {
        isDead = true;
    }

    private void OnMouseDown()
    {
        if (isDead)
        {
            // ���⼭ ���� Ŭ���ϸ� UI �߰� �츱�� ���� �ϸ� ��
        }
    }

    private void TempRebuild()
    {
        isDead = false; // ��ġ�Ǹ� isDead �ٽ� false�� ����� �ǰ�
    }
}
