using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace StatOperator
{
    public static class StatCalculator
    {
        public static float ReturnPercent(int maxValue, int curValue) //300 105 => 35%
            => curValue / maxValue * 100;
        public static float Percent(int value, int per) // 10 10 => 1, // 100 10 => 10
            => value * (per / 100f);
        public static float OperTimes(float result, int value)
            => result / value;
        public static int GetValue(int plusValue, int minusValue)
            => plusValue - minusValue;
        /// <summary>
        /// �տ���. ���� ��� ��
        /// </summary>
        /// <returns></returns>
        public static int SumOperValue(int value, List<int> percents)
        {
            if (percents.Count <= 0) return 0;

            float sum = 0; // �Ǽ��� ����
            foreach (var per in percents)
            {
                sum += Percent(value, per); // �տ��� ����
            }
            return (int)(sum);
        }

        /// <summary>
        /// ������. ���� ��� ��
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
        /// �տ���. ���� ���� ��� �����ߴ��� ����(�Ҽ� 2�ڸ����� #�ݿø�)
        /// </summary>
        /// <param name="value"></param>
        ///  ������ ��.
        /// <param name="percents"></param>
        ///  �տ��� �� �ۼ�Ʈ ����Ʈ.
        /// <returns></returns>
        public static float SumOperTimes(int value, List<int> percents)
        {
            float sum = 0; // �Ǽ��� ����
            foreach (var per in percents)
            {
                sum += Percent(value, per); // �տ��� ����
            }
            return Mathf.Round((1 + sum / value) * 100.0f) / 100.0f;// ���� ���� ��� �� �� �����ߴ��� ���
        }

        /// <summary>
        /// ������. ���� ���� ��� �����ߴ��� ����(�Ҽ� 2�ڸ����� #�ݿø�)
        /// </summary>
        /// <param name="value"></param>
        /// ������ ��.
        /// <param name="percents"></param>
        /// ������ �� �ۼ�Ʈ ����Ʈ.
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
    }

}


