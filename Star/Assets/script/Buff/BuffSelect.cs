using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuffSelect : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] enemy;
    public int enemyCount;

    public GameObject BuffWindow;
    public GameObject[] Cards;
    public int one;
    public int two;
    public int three;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemy.Length;

        Cards = GameObject.FindGameObjectsWithTag("card");
        BuffWindow.SetActive(false);
        one = Random.Range(0, 5);
        two = Random.Range(0, 5);
        three = Random.Range(0, 5);
        for(int i = 0; i < Cards.Length; i++)
        {
            Cards[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (BuffWindow.activeSelf)
        {
            if (two == one)
            {
                two = Random.Range(0, 5);
            }
            else if (three == two || three == one)
            {
                three = Random.Range(0, 5);
            }
            else if (one != two && one != three)
            {
                Cards[one].transform.localPosition = new Vector3(-250, 0, 0);
                Cards[two].transform.localPosition = new Vector3(0, 0, 0);
                Cards[three].transform.localPosition = new Vector3(250, 0, 0);
                Cards[one].SetActive(true);
                Cards[two].SetActive(true);
                Cards[three].SetActive(true);
            }
        }
        if(enemyCount < enemy.Length)
        {
            BuffWindow.SetActive(true);
            enemyCount = enemy.Length;
        }
    }

    public void SpeedupBuff()
    {
        Player.GetComponent<Player>().speed += 1;
        BuffWindow.SetActive(false);
    }
    public void JumpBuff()
    {
        Player.GetComponent<Player>().jumpForce += 0.5f;
        BuffWindow.SetActive(false);
    }

    public void ChooseOtherCard()
    {
        BuffWindow.SetActive(false);
    }
}
