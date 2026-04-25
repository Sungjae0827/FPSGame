using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PlayerMove : MonoBehaviour
{

    public float moveSpeed = 7f;

    CharacterController cc;

    float gravity = -20f;

    float yVelocity = 0;

    public float jumpPower = 10f;

    public bool isJumping = false;

    public int hp = 20;

    int maxHp = 20;

    public GameObject hitEffect;

    public Slider hpSlider;

    public void DamageAction(int damage)
    {
        if (hp <= 0) return;

        hp -= damage; //

        hpSlider.value = (float)hp / (float)maxHp; //

        if (hp > 0)
        {
            StartCoroutine(PlayHitEffect()); //
        }
        else
        {
            hp = 0;
            print("Player Die!");

            // 게임 매니저에게 게임 오버 알림
            GameManager.instance.OnGameOver();
        }
    }
    IEnumerator PlayHitEffect()
    {
        hitEffect.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        hitEffect.SetActive(false);
    }

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        // 1. 게임 시작 전이면 아래의 모든 로직을 실행하지 않고 리턴합니다.
        if (GameManager.instance == null || GameManager.instance.isGameStart == false)
        {
            return;
        }

        // 2. 이동 입력 처리
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;
        dir = Camera.main.transform.TransformDirection(dir);

        // 3. 중력 및 점프 처리
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            if (isJumping)
            {
                isJumping = false;
                yVelocity = 0;
            }
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // 4. 최종 이동 처리
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}