using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBullet : MonoBehaviour
{

    float life = 2;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //other.gameObject.GetComponent<Enemy>().TakeDamage();
            Destroy(gameObject);
        }

        if (other.tag == "Ground")
        {
            
            Destroy(gameObject);
        }
    }
}
