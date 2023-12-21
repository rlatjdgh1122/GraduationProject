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
        /// �տ���. ���� ��� ��
        /// </summary>
        /// <returns></returns>
        public static float SumOperValue(int value, List<int> percents)
        {
            float sum = 0; // �Ǽ��� ����
            foreach (var per in percents)
            {
                sum += Percent(value, per) - value; // �տ��� ����
            }
            return value + sum;
        }

        /// <summary>
        /// ������. ���� ��� ��
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
        /// �տ���. ���� ���� ��� �����ߴ��� ����
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
                sum += Percent(value, per) - value; // �տ��� ����
            }
            return 1 + sum / value; // ���� ���� ��� �� �� �����ߴ��� ���
        }

        /// <summary>
        /// ������. ���� ���� ��� �����ߴ��� ����
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
                sum *= Percent(value, per) / 100f;
            }
            return sum;
        }
    }

}


