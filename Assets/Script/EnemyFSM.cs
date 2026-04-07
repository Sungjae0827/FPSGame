using System.Collections;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    EnemyState m_State;

    public float attackDistance = 3.0f;

    public float moveSpeed = 5.0f;

    CharacterController cc;

    //플레이어 발견 범위
    public float findDistance = 8.0f;

    float currentTime = 0;

    float attackDelay = 2.0f;

    public int attackPower = 3;

    public int hp = 15;

    //플레이어 트랜스폼
    Transform player;

    Vector3 originPos;

    public float moveDistance = 20f;

    public int weaponPower = 5;

    void Start()
    {
        m_State = EnemyState.Idle;

        player = GameObject.Find("Player").transform;

        cc = player.GetComponent<CharacterController>();

        //초기 위치 저장하기
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) <  findDistance)
        {
            m_State = EnemyState.Move;
            print("상태 전환: idle -> Move");
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            print("상태 전환: Move -> Return");
        }
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            m_State = EnemyState.Attack;
            print("상태 전환: Move -> Attck");

            currentTime = attackDelay;
        }
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("공격");
                currentTime = 0;
            }
        }
        else
        {
            m_State = EnemyState.Move;
            print("상태 전환: Attack -> Move");
            currentTime = 0;
        }
    }

    void Return()
    {
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = originPos;

            hp = 15;
            m_State = EnemyState.Idle;
            print("상태 전환: return -> Idle");
        }
    }

    public void HitEnemy(int hitPower)
    {
        hp -= hitPower;

        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("상태 전환: Any state -> Damaged");
            Damaged();
        }
        else
        {
            m_State = EnemyState.Die;
            print("상태 전환: Ant state -> Die");
            Die();
        }
    }
    void Damaged()
    {
        StartCoroutine(DamageProcess());
    }

    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(0.5f);

        m_State = EnemyState.Move;
        print("상태 전환: Damaged -> Move");
    }
    void Die()
    {

    }
}
