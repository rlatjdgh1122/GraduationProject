using UnityEngine;
using UnityEngine.Events;

namespace SkillSystem
{
    public class CheckAttackCountDecision : SKillDecision
    {

        private bool _checkSkillReady = true;

        protected override void Init()
        {
            if (entityActionData == null) return;

            saveValue = entityActionData.AttackCount - maxValue;
            _checkSkillReady = true;
        }

        protected override void OnAttack(int attackCount)
        {
            if (!_checkSkillReady) return;

            OnSkillActionEnterEvent?.Invoke();

            if (MakeDecision()) // ��ų ��� ������ ó�� ������ �� �� �� ����
            {
                OnSkillReadyEvent?.Invoke();
                _checkSkillReady = false;
            }
        }

        public override void OnUsed()
        {
            OnSkillUsedEvent?.Invoke();

            skillActionData.AddSkillUsedCount();
            saveValue = entityActionData.AttackCount;
            _checkSkillReady = true;
        }

        public override bool MakeDecision()
        {
            return maxValue + saveValue <= entityActionData.AttackCount;
        }

        public override void LevelUp(int value)
        {
            maxValue -= value;
        }
    }
}


