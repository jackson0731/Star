using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    public GameObject ResultWindow;

    void Awake()
    {
        ResultWindow.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ResultWindow.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void nextStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("1-2");
    }
}
