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

            if (_canUsedskillInitially) //ó���� ��밡���ϰ�
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

        //start���� ���� ����Ǵ���

        public virtual void SetUp(Transform parentRoot)
        {
            entityActionData = parentRoot.GetComponent<EntityActionData>();
            skillActionData = transform.parent.GetComponent<SkillActionData>(); //��ų Ʈ�����ǿ��� �����

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
        /// ���������� �����
        /// </summary>
        /// <param name="hitCount"></param>
        protected virtual void OnHit(int hitCount) { }

        /// <summary>
        /// ���������� �����
        /// </summary>
        /// <param name="hitCount"></param>
        protected virtual void OnAttack(int attackCount) { }

        #endregion



        public abstract bool MakeDecision();

        public virtual void OnUsed() { }   //��ų ����� �� �����

        public virtual void LevelUp(int value) { }  //�������ϸ� �������� ���ΰ�

    }//end cs
}



