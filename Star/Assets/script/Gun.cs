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

    private Vector3 mousePos;

    void Start()
    {
        sound = GameObject.Find("Player").GetComponent<Sound>();
    }

    void Update()
    {
        transform.position = GunSpot.transform.position;
        transform.rotation = GunSpot.transform.rotation;
        if (Input.GetKeyDown("f"))
        {
            Shoot();

            float amount = 5f;
            sound.addSound(amount);
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
