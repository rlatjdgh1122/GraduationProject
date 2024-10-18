using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseElementsConfigurer
{
    private readonly float setposY = 2f;
    private readonly float groundRadius = 5f;
    private readonly float checkDistance = 2f;

    protected bool isBossWave => WaveManager.Instance.CurrentWaveCount % 5 == 0; // 보스 나올 웨이브인지
    protected bool isGeneralWave => WaveManager.Instance.CurrentWaveCount % 3 == 0; // 장군 나올 웨이브인지

    protected Transform transform;

    public BaseElementsConfigurer(Transform transform)
    {
        this.transform = transform;
    }

    protected Vector3 GetGroundRandomPosition(List<Vector3> previousElementsPositions)
    {
        Vector3 randomPos;
        bool positionFound = false;

        do
        {
            randomPos = Random.insideUnitSphere * groundRadius;
            randomPos.y = setposY;

            positionFound = true;
            foreach (Vector3 prevPos in previousElementsPositions)
            {
                if (Vector3.Distance(randomPos, prevPos) < checkDistance)
                {
                    positionFound = false;
                    break;
                }
            }

        } while (!positionFound);

        previousElementsPositions.Add(randomPos);

        //randomPos = Random.insideUnitSphere * groundRadius;
        // randomPos.y = setposY;                                                                                                                                  

        return randomPos;
    }

    protected virtual void SetGroundElementsPosition(GameObject spawnedElement, Transform transform, List<Vector3> previousElementsPositions)
    {
        spawnedElement.transform.SetParent(transform);

        Vector3 resourcePos = GetGroundRandomPosition(previousElementsPositions);

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
