using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 2;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
