using UnityEngine;
using UnityEngine.AI;

// 죽는걸 Dead상태로 처리하면 호출 순서 문제로
// DeadTarget에서 타겟을 찾을 때 죽은 엔티티도 찾는 문제가 생김
public abstract class EntityDeadController<T> : MonoBehaviour, IDeadable
    where T : Entity
{
   protected readonly int HASH_DEAD = Animator.StringToHash("Dead");

    protected T _owner;

    protected Animator _anim;
    protected NavMeshAgent _agent;
    protected CharacterController _character;

    protected void Awake()
    {
        _owner = GetComponent<T>();
        _agent = GetComponent<NavMeshAgent>();
        _character = GetComponent<CharacterController>();
        _anim = transform.Find("Visual").GetComponent<Animator>();
    }

    public virtual void OnDied()
    {
        // 모든 bool 변수를 순회하며 비활성화하기
        var parameters = _anim.parameters;
        foreach (var param in parameters)
            _anim.SetBool(param.name, false);

        //죽는 애니메이션 처리
        _anim.SetBool(HASH_DEAD, true);
        //엔티티 네브메쉬와 콜라이더 꺼줌
        _agent.enabled = false;
        _character.enabled = false;
        //엔티티 죽음 처리
        _owner.IsDead = true;
        //엔티티 스크립트 꺼줌
        _owner.enabled = false;
    }
}
