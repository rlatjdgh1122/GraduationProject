using System.Collections;
using UnityEngine;

public class MortarRock : Arrow
{
    private float timer;
    private MortarExplosionEffect _attackFeedback;

    [SerializeField] // 일단 여기다가 넣음
    private int damage;

    private bool isDestoried;

    [SerializeField]
    private GameObject _mortarAttackRangeSprite;

    private Vector3 _endPos;

    private void Awake()
    {
        //_damageCaster = GetComponent<DamageCaster>();
        _attackFeedback = transform.GetChild(0).GetComponent<MortarExplosionEffect>();
    }

    public override void Setting(TargetObject owner, LayerMask layer)
    {
        base.Setting(owner, layer);
    }

    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        float parabolaHeight = ParabolaFunc(height, t);

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, parabolaHeight + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    private float ParabolaFunc(float height, float t)
    {
        return  -4 * height * t * t + 4 * height * t;
    }

    public IEnumerator BulletMove(Vector3 startPos, Vector3 endPos)
    {
        timer = 0;
        _endPos = endPos;
        isDestoried = false;

        _mortarAttackRangeSprite.SetActive(true);
        _mortarAttackRangeSprite.transform.position = endPos;
        _mortarAttackRangeSprite.transform.SetParent(null);

        float distance = Vector3.Distance(startPos, endPos);

        float height = distance * 0.2f;

        while (!isDestoried)
        {
            if (transform.position.y <= -3f) // -1보다 작아지면 바다에 빠진 것
            {
                break;
            } 
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, height, timer);
            transform.position = tempPos;
            transform.rotation = Quaternion.Euler(0, timer * 720f, 0);
            yield return new WaitForEndOfFrame();
        }

        if (!isDestoried)
        {
            isDestoried = true;
            _mortarAttackRangeSprite.transform.SetParent(transform);
            _mortarAttackRangeSprite.SetActive(false);
            PoolManager.Instance.Push(this);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyRock();
    }

    private void DestroyRock()
    {
        if (!isDestoried)
        {
            Debug.Log("닿아서 부서짐");
            isDestoried = true;
            _mortarAttackRangeSprite.transform.SetParent(transform);
            _mortarAttackRangeSprite.SetActive(false);
            _damageCaster.CastBuildingAoEDamage(transform.position, _damageCaster.TargetLayer, damage);
            _attackFeedback.CreateFeedback(_endPos);
            SoundManager.Play3DSound(SoundName.MortarExplosion, transform.position);

            CoroutineUtil.CallWaitForSeconds(0.7f, null, () =>
            {
                PoolManager.Instance.Push(this);
            });
        }
    }

    private void OnDisable()
    {
    }
}
