using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnyAtk : MonoBehaviour
{
    [SerializeField] private bool HasDealDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !HasDealDamage)
        {
            //HasDealDamage = true;
            other.gameObject.GetComponent<Animator>().SetTrigger("BeingHit");

        }

    }
}
