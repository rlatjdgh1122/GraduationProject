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
            //웨이브 돌때마다 스킬 사용 가능하게
            SignalHub.OnBattlePhaseStartEvent -= WaveStart;
            Debug.Log("아초기화!!!!");
            _canUsedSkill = true;
        }

        public override void OnUsed()
        {
            OnSkillUsedEvent?.Invoke();

            saveValue = WaveManager.Instance.CurrentWaveCount;

            SignalHub.OnBattlePhaseStartEvent += WaveStart;
            Debug.Log("아초기화할겁니다이제");

            //한번 사용하면 못쓰게
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

