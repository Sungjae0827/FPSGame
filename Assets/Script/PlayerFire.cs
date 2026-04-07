using UnityEngine;

<<<<<<< HEAD
public class PlayerFire : MonoBehaviour
=======
public class Playerfire : MonoBehaviour
>>>>>>> 466d5db38f545fed74f39a49a70e3aa54807ef01
{
    public GameObject firePosition;

    public GameObject bombFactory;

    public float throwPower = 15f;

    public GameObject bulletEffect;

    ParticleSystem ps;

    void Start()
    {
<<<<<<< HEAD
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
=======
        ps = bulletEffect.GetComponent<ParticleSystem>();    
    }

>>>>>>> 466d5db38f545fed74f39a49a70e3aa54807ef01
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;

            Rigidbody rb = bomb.GetComponent<Rigidbody>();

            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }
<<<<<<< HEAD
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
=======
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

>>>>>>> 466d5db38f545fed74f39a49a70e3aa54807ef01
            RaycastHit hitInfo = new RaycastHit();

            if(Physics.Raycast(ray, out hitInfo))
            {
<<<<<<< HEAD
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    
                }
                else
                {
                    bulletEffect.transform.position = hitInfo.point;

                    bulletEffect.transform.forward = hitInfo.normal;

                    ps.Play();
                }
=======
                bulletEffect.transform.position = hitInfo.point;

                bulletEffect.transform.forward = hitInfo.normal;

                ps.Play();
>>>>>>> 466d5db38f545fed74f39a49a70e3aa54807ef01
            }
        }
    }
}
