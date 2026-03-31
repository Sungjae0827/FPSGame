using UnityEngine;

public class Playerfire : MonoBehaviour
{
    public GameObject firePosition;

    public GameObject bombFactory;

    public float throwPower = 15f;

    public GameObject bulletEffect;

    ParticleSystem ps;

    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();    
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;

            Rigidbody rb = bomb.GetComponent<Rigidbody>();

            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo = new RaycastHit();

            if(Physics.Raycast(ray, out hitInfo))
            {
                bulletEffect.transform.position = hitInfo.point;

                bulletEffect.transform.forward = hitInfo.normal;

                ps.Play();
            }
        }
    }
}
