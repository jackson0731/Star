using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float life = 2;
    [SerializeField]private ParticleSystem ImpactPar;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    private void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(2);
            Instantiate(ImpactPar, transform.position, Quaternion.LookRotation(transform.forward));
            Destroy(gameObject);
        }

        if (other.tag == "Ground")
        {
            
            Instantiate(ImpactPar, transform.position, Quaternion.LookRotation(-transform.forward));
            Destroy(gameObject);
        }
    }

}
