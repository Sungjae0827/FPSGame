using UnityEngine;

public class EnemtFMS : MonoBehaviour
{
    //에너미 상태 상수
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    //에너미 상태 변수
    EnemyState m_State;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_State = EnemyState.Idle;
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
                Return ();
                break;
            case EnemyState Die;
                Die();
                break;
        }
    }

    void idle()
    {

    }
    void move()
    {

    }
    void Attack()
    {

    }
    void Return()
    {

    }
    void Damaged()
    {

    }
    void Die()
    {

    }
}
