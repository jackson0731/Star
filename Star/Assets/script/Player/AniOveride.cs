using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoBehaviours
{
    public class AniOveride : MonoBehaviour
    {
        public Animator Ani;

        private void Awake()
        {
            Ani = GetComponent<Animator>();
        }

        public void SetAnimations(AnimatorOverrideController overrideController)
        {
            Ani.runtimeAnimatorController = overrideController;
        }
    }
}

