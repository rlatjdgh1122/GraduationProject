using System.Collections.Generic;
using UnityEngine;

namespace StatOperator
{
    public static class StatCalculator
    {
        // 10 10 => 1, // 100 10 => 10
        public static float Percent(int value, int per)
            => value * (per / 100f);
        public static float OperTimes(float result, int value)
            => result / value;
        public static int GetValue(int plusValue, int minusValue)
            => plusValue - minusValue;

        /// <summary>
        /// 합연산. 최종 결과 값
        /// </summary>
        /// <returns></returns>
        public static int SumOperValue(int value, List<int> percents)
        {
            if (percents.Count <= 0) return 0;

            float sum = 0; // 실수로 변경
            foreach (var per in percents)
            {
                sum += Percent(value, per); // 합연산 수정
            }
            return (int)(sum);
        }

        /// <summary>
        /// 곱연산. 최종 결과 값
        /// </summary>
        /// <returns></returns>
        public static int MultiOperValue(int value, List<int> percents)
        {
            float sum = 1;
            foreach (var per in percents)
            {
                sum *= 1 + (per / 100.0f);
            }
            return (int)(value * sum);
        }

        /// <summary>
        /// 합연산. 기존 값에 몇배 증가했는지 리턴(소수 2자리까지 #반올림)
        /// </summary>
        /// <param name="value">수정할 값.</param>
        /// <param name="percents">합연산 할 퍼센트 리스트.</param>
        /// <returns></returns>
        public static float SumOperTimes(int value, List<int> percents)
        {
            float sum = 0; // 실수로 변경
            foreach (var per in percents)
            {
                sum += Percent(value, per); // 합연산 수정
            }
            return Mathf.Round((1 + sum / value) * 100.0f) / 100.0f;// 기존 스탯 대비 몇 배 증가했는지 계산
        }

        /// <summary>
        /// 곱연산. 기존 값에 몇배 증가했는지 리턴(소수 2자리까지 #반올림)
        /// </summary>
        /// <param name="value">수정할 값.</param>
        /// <param name="percents">곱연산 할 퍼센트 리스트.</param>
        /// <returns></returns>
        public static float MultiOperTimes(int value, List<int> percents)
        {
            float sum = 1;
            foreach (var per in percents)
            {
                // sum *= (value + Percent(value, per)) / 100f;
                sum *= 1 + (per / 100.0f);
            }
            return Mathf.Round(sum * 100.0f) / 100.0f;
        }

        /// <summary>
        /// 방어력 포함하여 최종적으로 받을 데미지 계산(롤 방어력 계산과 같음)
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="armor"></param>
        /// <returns></returns>
        public static int GetDamage(int damage, int armor)
        {
            return Mathf.RoundToInt(damage * 1 / (1 + armor * 0.01f));
        }

    }//end class
}//end namespace


