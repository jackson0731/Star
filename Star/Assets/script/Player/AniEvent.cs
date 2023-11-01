using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniEvent : MonoBehaviour
{
    [SerializeField] Animator Ani;
    [SerializeField] Player Player;
    [SerializeField] Sound Sound;
    public bool IsAtk;
    public bool CanDealDamage;
    
    // Start is called before the first frame update
    void Start()
    {
               
        CanDealDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssDone()
    {
        Player.StateSwitch = false;
        Player.StateType = Player.State.CanMove;
    }

    public void ComputerDone()
    {
        Player.StateSwitch = false;
        Sound.currentSound = 0;
    }
    public void AtkMomantumStart()
    {
        Player.speed = 3.5f;
        
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
        //Debug.Log("SWOff");
    }
}
