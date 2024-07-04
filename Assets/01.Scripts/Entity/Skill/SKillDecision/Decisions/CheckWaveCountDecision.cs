using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkillSystem
{
    public class CheckWaveCountDecision : SKillDecision
    {
        private bool _canUsedSkill = true;

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        public void WaveStart()  
        {
            //���̺� �������� ��ų ��� �����ϰ�
            SignalHub.OnBattlePhaseStartEvent -= WaveStart;
            Debug.Log("���ʱ�ȭ!!!!");
            _canUsedSkill = true;
        }

        public override void OnUsed()
        {
            OnSkillUsedEvent?.Invoke();

            saveValue = WaveManager.Instance.CurrentWaveCount;

            SignalHub.OnBattlePhaseStartEvent += WaveStart;
            Debug.Log("���ʱ�ȭ�Ұ̴ϴ�����");

            //�ѹ� ����ϸ� ������
            _canUsedSkill = false;
        }

        public override bool MakeDecision()
        {
            if (!_canUsedSkill) return false;

            return maxValue + saveValue <= WaveManager.Instance.CurrentWaveCount;
        }

        public override void LevelUp(int value)
        {

            maxValue -= value;
        }

        
    }
}

