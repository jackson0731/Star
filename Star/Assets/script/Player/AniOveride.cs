using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoBehaviours
{
    public class AniOveride : MonoBehaviour
    {
        public Animator Ani;
        public Player Player;

        private void Awake()
        {
            Ani = GetComponent<Animator>();
            Player = GetComponent<Player>();
        }

        public void SetAnimations(AnimatorOverrideController overrideController)
        {
            Ani.runtimeAnimatorController = overrideController;
        }

        public void AtkMomantumStart()
        {
            Player.speed = 3.5f;
            Player.rb.AddForce(Player.transform.forward * 20, ForceMode.Impulse);
        }
        public void AtkMomantumStop()
        {
            Player.speed = 1.5f;
        }
    }
}

