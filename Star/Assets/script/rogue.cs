using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rogue : MonoBehaviour
{
    private GameObject[] Cards;
    private int one;
    private int two;
    private int three;

    // Start is called before the first frame update
    void Start()
    {
        Cards = GameObject.FindGameObjectsWithTag("card");
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
        if(two == one)
        {
            two = Random.Range(0, 5);
        }
        if(three == two || three == one)
        {
            three = Random.Range(0, 5);
        }
        if(one != two && one != three)
        {
            Cards[one].transform.localPosition = new Vector3(-250,0,0);
            Cards[two].transform.localPosition = new Vector3(0,0,0);
            Cards[three].transform.localPosition = new Vector3(250,0,0);
            Cards[one].SetActive(true);
            Cards[two].SetActive(true);
            Cards[three].SetActive(true);
        }
    }
}
