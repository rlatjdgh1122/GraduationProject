using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUltimateHandler : MonoBehaviour
{
    [SerializeField] private GameObject _cagePrefab;
    [SerializeField] private float _range;
    [SerializeField] private float _delayBetweenDrops = 0.5f; //�� ������Ʈ�� ������ �ð��� ����
    [SerializeField] private float _stunTime = 4f; //öâ �°� ���� �� �ð�

    public void PlaySkill()
    {
        StartCoroutine(SkillPlay());
    }

    private IEnumerator SkillPlay()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _range);

        foreach (var collider in hitColliders)
        {
            Enemy script = collider.GetComponent<Enemy>();
            if (script != null)
            {
                GameObject obj = Instantiate(_cagePrefab, new Vector3(script.transform.position.x, 15, script.transform.position.z), transform.rotation);
                obj.transform.DOMoveY(script.transform.position.y, 1).OnComplete(() => script.HealthCompo.Stun(_stunTime)).SetEase(Ease.OutQuart);

                yield return new WaitForSeconds(_delayBetweenDrops); 
            }
        }
    }
}
