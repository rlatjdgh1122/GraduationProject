using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Random = UnityEngine.Random;

public enum OutlineColorType
{
    Green,
    Red,
    None
}

[RequireComponent(typeof(Outline))]
public class Ground : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _resourcePrefabs = new List<GameObject>();

    [SerializeField]
    private List<GameObject> _enemyPrefabs = new List<GameObject>();

    [SerializeField]
    private List<GameObject> _rewardPrefabs = new List<GameObject>();

    private bool isInstalledBuilding;

    public bool IsInstalledBuilding => isInstalledBuilding;

    private Outline _outline;
    public Outline OutlineCompo =>_outline;

    private GroundMove _groundMove;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _groundMove = GetComponent<GroundMove>();
    }

    public void InstallBuilding() //���� ��ġ�Ǿ��ٰ� ó��
    {
        isInstalledBuilding = true;
        UpdateOutlineColor(OutlineColorType.None);
    }

    public void UpdateOutlineColor(OutlineColorType type)
    {
        _outline.enabled = true;
        _outline.OutlineWidth = 2.0f;
        _outline.OutlineMode = Outline.Mode.OutlineAll;

        switch (type)
        {
            case OutlineColorType.Green:
                _outline.OutlineColor = Color.green;
                break;
            case OutlineColorType.Red:
                _outline.OutlineColor = Color.red;
                break;
            case OutlineColorType.None:
                _outline.enabled = false;
                break;
        }
    }

    public void SetGroundInfo(Transform parentTransform, Vector3 position)
    {
        SetEnemy();
        SetReward();
        SetResource();
        _groundMove.SetGroundInfo(parentTransform,
                                  position);
    }

    private void SetResource()
    {
        float resourceCountProportion = 0.5f;
        int minResourceCount = 0;
        int maxResourceCount = 3;

        int resourceCount = Mathf.RoundToInt(WaveManager.Instance.CurrentWaveCount * resourceCountProportion);
        resourceCount = Mathf.Clamp(resourceCount, minResourceCount, maxResourceCount);

        for (int i = 0; i < resourceCount; i++)
        {
            Debug.Log("�ڿ� ����");
        }

        if (resourceCount == 0)
        {
            Debug.Log("�ڿ� ����X");
        }
    }

    private void SetEnemy()
    {
        float enemyCountProportion = 0.5f;
        int minEnemyCount = 1;
        int maxEnemyCount = 5;

        int enemyCount = Mathf.RoundToInt(WaveManager.Instance.CurrentWaveCount * enemyCountProportion);
        enemyCount = Mathf.Clamp(enemyCount, minEnemyCount, maxEnemyCount);

        for(int i = 0; i < enemyCount; i++)
        {
            Debug.Log("�� ����");
        }

    }

    private void SetReward()
    {
        if (Random.Range(0, 5) == 0)
        {
            // ���𰡰� �߻��� ���, ���ϴ� �۾��� ����
            Debug.Log("�Žñ� ����ڽ� ����");
        }
        else
        {
            Debug.Log("����ڽ� ����X");
        }
    }

    public void SetMoveTarget(Transform trm)
    {
        _groundMove.SetMoveTarget(trm);
    }
}
