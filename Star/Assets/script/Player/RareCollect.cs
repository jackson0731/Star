using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RareCollect : MonoBehaviour
{
    public GameObject player;
    public bool collecting = false;
    public float time;
    public GameObject collectText;

    public GameObject rareText;
    private float plus; //每10秒加1次
    public float rareCollecting;
    public float rareCollected;

    [SerializeField] GameObject Bot;
    [SerializeField] GameObject BotModel;
    [SerializeField] GameObject BotPos;
    private void Awake()
    {
        player = GameObject.Find("Player");
        collectText.SetActive(false);
        rareText = GameObject.Find("Rare");

    }
    void Update()
    {
        plus = time / 10;
        if (collecting)
        {
            time += Time.deltaTime;
            rareCollecting = Mathf.Floor(plus)*5;
        }
        if(Mathf.Floor(time) >= 100)
        {
            collecting = false;
            Debug.Log("Collect End");
            time = 0;

            rareCollected += rareCollecting;
            rareCollecting = 0;
            Destroy(Bot);
        }
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

                Bot = Instantiate(BotModel, BotPos.transform);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !collecting)
        {
            collectText.SetActive(false);
        }
    }

    public void LoadData(GameData data)
    {
        rareCollected = data.rareCollected;
    }
    public void SaveData(GameData data)
    {
        data.rareCollected = rareCollected;
    }
}
