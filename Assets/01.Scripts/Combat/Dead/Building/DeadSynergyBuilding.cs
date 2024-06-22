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
            // 여기서 이제 클릭하면 UI 뜨고 살릴지 말지 하면 됨
        }
    }

    private void TempRebuild()
    {
        isDead = false; // 설치되면 isDead 다시 false로 해줘야 되고
    }
}
