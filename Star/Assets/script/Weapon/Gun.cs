using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Sound sound;
    //[SerializeField] GameObject GunSpot;
    [SerializeField] GameObject bulletPrefab; // 子彈的預製體
    [SerializeField] Transform firePoint; // 子彈發射的起點
    [SerializeField] Animator ani;
    [SerializeField] BulletCount BC;

    [SerializeField] float bulletForce = 20f;
    [SerializeField] float noCombo;

    private bool CanShoot;

    void Start()
    {
        sound = GameObject.Find("Player").GetComponent<Sound>();
        ani = GameObject.Find("Player").GetComponent<Animator>();
        BC = GameObject.Find("Player").GetComponent<BulletCount>();
    }

    void Update()
    {
        noCombo -= Time.deltaTime;

        Fire();
    }

    //Gun
    void Fire()
    {
        if(ani.GetCurrentAnimatorStateInfo(0).IsName("PistolIdle1") || ani.GetCurrentAnimatorStateInfo(0).IsName("AimRun") || ani.GetCurrentAnimatorStateInfo(0).IsName("AimFall"))
        {
            CanShoot = true;
        }
        else
        {
            CanShoot = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetBool("IsAtk", true);
            noCombo = 1f;
            Invoke("Shoot", 0.2f);

        }
        else if (noCombo<0)
        {
            ani.SetBool("IsAtk", false);
        }
    }
    void Shoot()
    {
        
        if (CanShoot == true && BC.bullet != 0)
        {
            // 產生子彈
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // 取得子彈的剛體元件
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            // 設定子彈的發射力量
            bulletRigidbody.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);

            BC.bullet -= 1;

            float amount = 5f;
            sound.addSound(amount);
        }
        
    }
}
