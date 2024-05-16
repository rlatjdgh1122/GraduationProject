using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseElementsConfigurer
{
    private readonly float setposY = 1.9f;
    private readonly float groundRadius = 5f;
    private readonly float raftRadius = 2f;
    private readonly float checkDistance = 2f;

    protected bool isBossWave => WaveManager.Instance.CurrentWaveCount % 5 == 0; // ���� ���� ���̺�����

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

    protected void SetGroundElementsPosition(GameObject spawnedElement, Transform transform, List<Vector3> previousElementsPositions)
    {
        spawnedElement.transform.SetParent(transform);

        Vector3 resourcePos = GetGroundRandomPosition(previousElementsPositions);

        spawnedElement.transform.localPosition = resourcePos;
        spawnedElement.transform.localScale = Vector3.one;
    }

    protected void SetRaftElementsPosition(GameObject spawnedElement, Transform transform)
    {
        spawnedElement.transform.SetParent(transform);

        Vector3 elementPos = Vector3.one;
        elementPos *= 0.2f;

        spawnedElement.transform.localPosition = elementPos;
        spawnedElement.transform.localScale = Vector3.one;
    }

    protected int GetRandomElementsCount(int minCount, int maxCount, float countProportion)
    {
        int elementsCount = Mathf.RoundToInt(WaveManager.Instance.CurrentWaveCount * countProportion);
        elementsCount = Mathf.Clamp(elementsCount, minCount, maxCount);
        return elementsCount;
    }

}
