using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoriedSynergyBuildingUI : MonoBehaviour
{
    private bool isDead;

    private void OnMouseDown()
    {
        if (isDead)
        {
            // ���⼭ ���� Ŭ���ϸ� UI �߰� �츱�� ���� �ϸ� ��
        }
    }

    public void SetDeadBuilding()
    {
        isDead = true;

    }

    private void TempRebuild()
    {
        isDead = false; // ��ġ�Ǹ� isDead �ٽ� false�� ����� �ǰ�
    }
}
