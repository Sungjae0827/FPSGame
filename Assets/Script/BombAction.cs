using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect;


    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(bombEffect);

        eff.transform.position = transform.position;
<<<<<<< HEAD

=======
    
>>>>>>> 466d5db38f545fed74f39a49a70e3aa54807ef01
        Destroy(gameObject);
    }
}
