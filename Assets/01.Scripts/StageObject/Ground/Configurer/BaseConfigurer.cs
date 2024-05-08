using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseConfigurer
{
    private readonly float D_setposY = 1.9f;
    private readonly float D_groundRadius = 5f;
    private readonly float D_checkDistance = 1.5f;

    protected bool isBossWave => WaveManager.Instance.CurrentWaveCount % 5 == 0; // ���� ���� ���̺�����

    protected Transform transform;

    public BaseConfigurer(Transform transform)
    {
        this.transform = transform;
    }

    public void SetElements()
    {

    }

    protected Vector3 GetRandomPosition(List<Vector3> previousElementsPositions)
    {
        Vector3 randomPos;
        bool positionFound = false;

        do
        {
            randomPos = (Vector3)Random.insideUnitCircle * D_groundRadius;
            randomPos.y = D_setposY;

            positionFound = true;
            foreach (Vector3 prevPos in previousElementsPositions)
            {
                if (Vector3.Distance(randomPos, prevPos) < D_checkDistance)
                {
                    positionFound = false;
                    break;
                }
            }

        } while (!positionFound);

        previousElementsPositions.Add(randomPos);
        return randomPos;
    }

    protected void SetPosition(GameObject spawnedElement, Transform transform, List<Vector3> previousElementsPositions)
    {
        spawnedElement.transform.SetParent(transform);

        Vector3 resourcePos = GetRandomPosition(previousElementsPositions);

        spawnedElement.transform.localPosition = resourcePos;
        spawnedElement.transform.localScale = Vector3.one;
    }

    protected int GetRandomElementsCount(int minCount, int maxCount, float countProportion)
    {
        int elementsCount = Mathf.RoundToInt(WaveManager.Instance.CurrentWaveCount * countProportion);
        elementsCount = Mathf.Clamp(elementsCount, minCount, maxCount);
        return elementsCount;
    }

}