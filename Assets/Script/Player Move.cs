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
        hp -= damage;

        hpSlider.value = (float)hp / (float)maxHp;

        if (hp > 0)
        {
            StartCoroutine(PlayHitEffect());
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
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);

        //transform.position += dir * moveSpeed * Time.deltaTime;

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);

        if(cc.collisionFlags == CollisionFlags.Below)
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

        yVelocity = gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.captureDeltaTime);
    }
}
