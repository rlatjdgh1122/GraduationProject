using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSystem : MonoBehaviour
{
    [SerializeField] private Transform _spawnStartPostion = null;
    [SerializeField] private Transform _spawnEndPostion = null;
    [Range(1f, 10f)] public float DetectionRange = 3f;
    public int InitMaxHealingTime = 30;
    public bool IsHealing = false;

    private HealingController _controller = null;
    private Coroutine _healingTimeCorouine = null;
    private Army _seletedArmy = null;

    private int _maxHealingTime = 0;

    private void Start()
    {
        _maxHealingTime = InitMaxHealingTime;

        _controller = new HealingController(_spawnStartPostion, _spawnEndPostion);
    }

    private void Update()
    {

    }

    public void SetArmy(Army army)
    {
        _seletedArmy = army;
        _controller.SetArmy(army);
    }

    private void HealingStart()
    {
        IsHealing = true;

        if (_healingTimeCorouine != null)
        {
            StopCoroutine(_healingTimeCorouine);
        }

        _healingTimeCorouine = StartCoroutine(HealingTimeCorou());
    }

    private void HealingEnd()
    {
        IsHealing = false;
        _controller.LeaveBuilding();

        UIManager.Instance.ShowWarningUI($"{_seletedArmy.LegionName}군단 회복 완료됨");
    }

    private IEnumerator HealingTimeCorou()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _maxHealingTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        HealingEnd();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }

}
