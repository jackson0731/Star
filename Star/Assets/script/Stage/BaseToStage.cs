using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseToStage : MonoBehaviour
{
    public GameObject nextStage;
    public GameObject player;
    public GameObject plotKillStory;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nextStage = GameObject.Find("EnterStageText");
        nextStage.SetActive(false);
    }
    private void Update()
    {
        if (player.GetComponent<Player>().firstToBase)
        {
            plotKillStory.SetActive(true);
            if (Input.anyKey)
            {
                plotKillStory.SetActive(false);
                player.GetComponent<Player>().firstToBase = false;
            }
        }
        else
        {
            plotKillStory.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            nextStage.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene("1-3");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            nextStage.SetActive(false);
        }
    }
}
