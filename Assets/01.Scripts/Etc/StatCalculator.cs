using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HDROutputUtils;

namespace StatOperator
{
    public static class StatCalculator
    {
        public static float Percent(int value, int per)
            => value + (value * (per / 100f));

        /// <summary>
        /// 합연산. 최종 결과 값
        /// </summary>
        /// <returns></returns>
        public static float SumOperValue(int value, List<int> percents)
        {
            float sum = 0; // 실수로 변경
            foreach (var per in percents)
            {
                sum += Percent(value, per) - value; // 합연산 수정
            }
            return value + sum;
        }

        /// <summary>
        /// 곱연산. 최종 결과 값
        /// </summary>
        /// <returns></returns>
        public static float MultiOperValue(int value, List<int> percents)
        {
            float sum = 1;
            foreach (var per in percents)
            {
                sum *= Percent(value, per) / 100f;
            }
            return value * sum;
        }

        /// <summary>
        /// 합연산. 기존 값에 몇배 증가했는지 리턴
        /// </summary>
        /// <param name="value"></param>
        ///  수정할 값.
        /// <param name="percents"></param>
        ///  합연산 할 퍼센트 리스트.
        /// <returns></returns>
        public static float SumOperTimes(int value, List<int> percents)
        {
            float sum = 0; // 실수로 변경
            foreach (var per in percents)
            {
                sum += Percent(value, per) - value; // 합연산 수정
            }
            return 1 + sum / value; // 기존 스탯 대비 몇 배 증가했는지 계산
        }

        /// <summary>
        /// 곱연산. 기존 값에 몇배 증가했는지 리턴
        /// </summary>
        /// <param name="value"></param>
        /// 수정할 값.
        /// <param name="percents"></param>
        /// 곱연산 할 퍼센트 리스트.
        /// <returns></returns>
        public static float MultiOperTimes(int value, List<int> percents)
        {
            float sum = 1;
            foreach (var per in percents)
            {
                sum *= Percent(value, per) / 100f;
            }
            return sum;
        }
    }

}


