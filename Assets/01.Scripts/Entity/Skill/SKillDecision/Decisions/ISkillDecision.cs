using UnityEngine;

namespace SkillSystem
{
    public interface ISkillDecision
    {
        public virtual void SetUp(Transform parentRoot) { }
        public abstract bool MakeDecision();
        public virtual void OnUsed() { }
        public virtual void LevelUp(DecisionType type, int value) { }

    }

}
