using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject[] tutorials;
    public int i = 1;

    void Awake()
    {
        Time.timeScale = 0;
        tutorials[0].SetActive(true);
        tutorials[1].SetActive(false);
        tutorials[2].SetActive(false);
        tutorials[3].SetActive(false);
        tutorials[4].SetActive(false);
        tutorials[5].SetActive(false);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (i >= tutorials.Length)
            {
                Time.timeScale = 1;
                Destroy(this.gameObject);
            }
            else
            {
                tutorials[i].SetActive(true);
                tutorials[i - 1].SetActive(false);
            }
            i++;
        }
    }
}
