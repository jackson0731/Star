using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Sound sound;
    public GameObject bulletPrefab; // 子彈的預製體
    public Transform firePoint; // 子彈發射的起點
    public float bulletForce = 20f;
    public GameObject GunSpot;
    public Animator ani;
    public float noCombo;

    private bool CanShoot;

    void Start()
    {
        sound = GameObject.Find("Player").GetComponent<Sound>();
        ani = GameObject.Find("Player").GetComponent<Animator>();
    }

    void Update()
    {
        noCombo -= Time.deltaTime;

        Fire();
    }

    //Gun
    void Fire()
    {
        if(ani.GetCurrentAnimatorStateInfo(0).IsName("Falling To Landing") || ani.GetCurrentAnimatorStateInfo(0).IsName("Jumping Up") || ani.GetCurrentAnimatorStateInfo(0).IsName("2ndJump"))
        {
            CanShoot = false;
        }
        else
        {
            CanShoot = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && CanShoot == true)
        {
            Invoke("Shoot", 0.4f);

            noCombo = 0.5f;
            ani.SetBool("IsAtk", true);
            float amount = 5f;
            sound.addSound(amount);
        }
        else if (noCombo<0)
        {
            ani.SetBool("IsAtk", false);
        }
    }
    void Shoot()
    {
        // 產生子彈
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 取得子彈的剛體元件
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // 設定子彈的發射力量
        bulletRigidbody.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }
}
