using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public GameObject player;
    public bool collecting = false;
    public float time;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if (collecting)
        {
            time += Time.deltaTime;
            Debug.Log("Collecting:" + Mathf.Floor(time) + "s");
        }
        if(Mathf.Floor(time) >= 5)
        {
            collecting = false;
            Debug.Log("Collect End");
            time = 0;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                collecting = true;
            }
        }
    }
}
