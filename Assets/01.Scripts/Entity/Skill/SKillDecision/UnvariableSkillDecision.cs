using SkillSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public abstract class UnvariableSkillDecision : MonoBehaviour, ISkillDecision
    {
        protected EntityActionData _entityActionData = null;
        protected SkillActionData _skillActionData = null;

        public virtual void SetUp(Transform parentRoot)
        {
            _entityActionData = parentRoot.GetComponent<EntityActionData>();
            _skillActionData = transform.parent.GetComponent<SkillActionData>();
        }

        public abstract bool MakeDecision();

        public virtual void OnUsed() { }
    }
}

