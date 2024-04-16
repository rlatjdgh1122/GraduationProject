using UnityEngine;

public class AttackEffectFeedback : Feedback
{
    [SerializeField] private float _endTime;

    private int _calledNum = 0;

    public override void CreateFeedback()
    {
        _calledNum++;

        if (_calledNum > 2)
            _calledNum = 1;

        EffectPlayer effect = PoolManager.Instance.Pop($"SlashEffect0{_calledNum}") as EffectPlayer;
        effect.transform.position = ownerTrm.position;

        if (_calledNum == 1)
            effect.transform.rotation = 
                Quaternion.Euler(new Vector3(ownerTrm.rotation.x, ownerTrm.rotation.y - 180f, ownerTrm.rotation.z - 150f));
        else if (_calledNum == 2)
            effect.transform.rotation =
                Quaternion.Euler(new Vector3(ownerTrm.rotation.x, ownerTrm.rotation.y - 180f, ownerTrm.rotation.z));
        effect.StartPlay(_endTime);
    }

    public override void FinishFeedback()
    {
        
    }
}
