using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniEvent : MonoBehaviour
{
    public Animator Ani;
    public Player Player;
    public bool IsAtk;
    public bool CanDealDamage;
    // Start is called before the first frame update
    void Start()
    {
        Ani = GetComponent<Animator>();
        Player = GetComponent<Player>();

        
        CanDealDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtkMomantumStart()
    {
        Player.speed = 3.5f;
        //Player.rb.AddForce(Player.transform.forward * 20, ForceMode.Impulse);
    }
    public void AtkMomantumStop()
    {
        Player.speed = 1.5f;
    }

    public void AtkColiderSwitchOn()
    {
        CanDealDamage = true;
    }

    public void AtkColiderSwitchOff()
    {
        CanDealDamage = false;
    }
}
