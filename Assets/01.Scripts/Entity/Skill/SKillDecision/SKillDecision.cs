using System;
using UnityEngine;

namespace SkillSystem
{
    public abstract class SKillDecision : MonoBehaviour, ISkillDecision
    {
        [SerializeField] private bool _canUsedskillInitially = true;
        [SerializeField] private int _initMaxVaue = 5;

        public Action OnSkillReadyEvent = null;
        public Action OnSkillUsedEvent = null;
        public Action OnSkillActionEnterEvent = null;
        public Action<int> OnChangedMaxValueEvent = null;

        protected EntityActionData entityActionData = null;
        protected SkillActionData skillActionData = null;

        private int _maxValue = 0;
        protected float saveValue = 0;

        protected int maxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                OnChangedMaxValueEvent?.Invoke(_maxValue);
            }
        }

        private void Start()
        {
            maxValue = _initMaxVaue;

            if (_canUsedskillInitially) //처음엔 사용가능하게
                saveValue = -maxValue;

        }

        private void Awake()
        {
            SignalHub.OnBattlePhaseStartEvent += Init;
        }

        protected virtual void OnDestory()
        {
            SignalHub.OnBattlePhaseStartEvent -= Init;

            if (entityActionData)
            {
                OffRegister();

            }//end if
        }

        protected abstract void Init();

        //start보다 먼저 실행되더라

        public virtual void SetUp(Transform parentRoot)
        {
            entityActionData = parentRoot.GetComponent<EntityActionData>();
            skillActionData = transform.parent.GetComponent<SkillActionData>(); //스킬 트렌지션에서 갖고옴

            OnRegister();
            Init();
        }

        private void OnRegister()
        {
            entityActionData.OnHitCountUpdated += OnHit;
            entityActionData.OnAttackCountUpdated += OnAttack;
        }

        private void OffRegister()
        {
            entityActionData.OnHitCountUpdated -= OnHit;
            entityActionData.OnAttackCountUpdated -= OnAttack;
        }

        #region EventHandler

        /// <summary>
        /// 맞을때마다 실행됨
        /// </summary>
        /// <param name="hitCount"></param>
        protected virtual void OnHit(int hitCount) { }

        /// <summary>
        /// 때릴때마다 실행됨
        /// </summary>
        /// <param name="hitCount"></param>
        protected virtual void OnAttack(int attackCount) { }

        #endregion



        public abstract bool MakeDecision();

        public virtual void OnUsed() { }   //스킬 사용한 후 실행됨

        public virtual void LevelUp(int value) { }  //레벨업하면 어케해줄 것인가

    }//end cs
}



