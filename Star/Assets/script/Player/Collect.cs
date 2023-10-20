using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collect : MonoBehaviour
{
    public GameObject player;
    public bool collecting = false;
    public float time;
    public GameObject collectText;

    public GameObject normalText;
    public GameObject rareText;
    public float normalCollecting;
    public float rareCollecting;
    public float normalCollected;
    public float rareCollected;

    private void Awake()
    {
        player = GameObject.Find("Player");
        collectText.SetActive(false);
        normalText = GameObject.Find("Normal");
        rareText = GameObject.Find("Rare");

    }
    void Update()
    {
        if (collecting)
        {
            time += Time.deltaTime;
            normalCollecting = Mathf.Floor(time);
            rareCollecting = Mathf.Floor(Mathf.Floor(time) / 2);
        }
        if(Mathf.Floor(time) >= 120)
        {
            collecting = false;
            Debug.Log("Collect End");
            time = 0;

            normalCollected += normalCollecting;
            rareCollected += rareCollecting;
            normalCollecting = 0;
            rareCollecting = 0;
        }
        normalText.GetComponent<Text>().text = "普通資源：" + (normalCollected + normalCollecting);
        rareText.GetComponent<Text>().text = "稀有資源：" + (rareCollected + rareCollecting);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !collecting)
        {
            collectText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                collectText.SetActive(false);
                collecting = true;
            }
        }
    }
}
