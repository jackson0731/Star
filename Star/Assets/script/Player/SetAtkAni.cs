using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoBehaviours
{
    public class SetAtkAni : MonoBehaviour
    {
        [SerializeField] private AnimatorOverrideController[] OverrideControllers;
        [SerializeField] private AniOveride overrider;
        [SerializeField] private RuntimeAnimatorController[] Animators;
        [SerializeField] Player player;

        public GameObject weaponHolderR;
        public GameObject weaponHolderL;

        public GameObject CurrentweaponR;
        public GameObject CurrentweaponL;
        public GameObject[] Weapon;

        void Start()
        {
            CurrentweaponR = null;
            CurrentweaponL = null;
            overrider.Ani.runtimeAnimatorController = Animators[0] as RuntimeAnimatorController;
        }
        void Update()
        {
            AnimatorSwitch();
        }

        void AnimatorSwitch()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                overrider.Ani.runtimeAnimatorController = Animators[0] as RuntimeAnimatorController;
                player.CanAss = true;
                Destroy(CurrentweaponR);
                Destroy(CurrentweaponL);

                CurrentweaponR = Instantiate(Weapon[0], weaponHolderR.transform);
                CurrentweaponR.transform.localPosition = new Vector3(0f, 0.076f, 0.012f);
                CurrentweaponR.transform.localRotation = Quaternion.Euler(-180f, -1f, 1.4f);

                CurrentweaponL = Instantiate(Weapon[0], weaponHolderL.transform);
                CurrentweaponL.transform.localPosition = new Vector3(0.004f, 0.072f, 0.023f);
                CurrentweaponL.transform.localRotation = Quaternion.Euler(6f, 2.1f, -177f);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                overrider.Ani.runtimeAnimatorController = Animators[1] as RuntimeAnimatorController;
                player.CanAss = false;
                Destroy(CurrentweaponR);
                Destroy(CurrentweaponL);
                CurrentweaponR = Instantiate(Weapon[1], weaponHolderR.transform);
                CurrentweaponR.transform.localPosition = new Vector3(0.05f, -0.2f, 0.05f);
                CurrentweaponR.transform.localRotation = Quaternion.Euler(-180f, 4f, 89f);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                overrider.Ani.runtimeAnimatorController = Animators[2] as RuntimeAnimatorController;
                player.CanAss = true;

                Destroy(CurrentweaponR);
                Destroy(CurrentweaponL);

                CurrentweaponR = Instantiate(Weapon[0], weaponHolderR.transform);
                CurrentweaponR.transform.localPosition = new Vector3(0f, 0.076f, 0.012f);
                CurrentweaponR.transform.localRotation = Quaternion.Euler(-180f, -1f, 1.4f);

                CurrentweaponL = Instantiate(Weapon[0], weaponHolderL.transform);
                CurrentweaponL.transform.localPosition = new Vector3(0.004f, 0.072f, 0.023f);
                CurrentweaponL.transform.localRotation = Quaternion.Euler(6f, 2.1f, -177f);
            }
        }
    }
}

