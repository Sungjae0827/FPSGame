using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect;


    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(bombEffect);

        eff.transform
    
        Destroy(gameObject);
    }
}
