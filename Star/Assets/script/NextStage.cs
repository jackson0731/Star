using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    public GameObject ResultWindow;
    public GameObject clearText;

    void Awake()
    {
        ResultWindow.SetActive(false);
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            clearText.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                clearText.SetActive(false);
                ResultWindow.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            clearText.SetActive(false);
        }
    }

    public void nextStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("1-2");
    }
}
