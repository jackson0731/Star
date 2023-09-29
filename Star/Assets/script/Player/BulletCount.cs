using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCount : MonoBehaviour
{
    public GameObject bulletText;
    public int bulletCount = 21;
    public int bullet = 7;
    private int maxBullet = 7;

    void Start()
    {
        bulletText = GameObject.Find("BulletText");
    }

    void Update()
    {
        if(bulletCount <= 0)
        {
            bulletCount = 0;
        }
        if (Input.GetKeyDown("r") && bulletCount + bullet >= 7)
        {
            bulletCount -= maxBullet - bullet;
            bullet = maxBullet;
        }else if(Input.GetKeyDown("r") && bulletCount + bullet < 7 && bulletCount > 0)
        {
            bullet = bulletCount;
            bulletCount = 0;
        }

        bulletText.GetComponent<Text>().text = bullet + "/" + bulletCount;
    }
}
