using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalCollect : MonoBehaviour
{
    public GameObject player;
    public bool collecting = false;
    public float time;
    public GameObject collectText;

    public GameObject normalText;
    private float plus; //每10秒加1次
    public float normalCollecting;
    public float normalCollected;

    [SerializeField] GameObject Bot;
    [SerializeField] GameObject BotModel;
    [SerializeField] GameObject BotPos;
    private void Awake()
    {
        player = GameObject.Find("Player");
        collectText.SetActive(false);
        normalText = GameObject.Find("Normal");

    }
    void Update()
    {
        plus = time / 10;
        if (collecting)
        {
            time += Time.deltaTime;
            normalCollecting = Mathf.Floor(plus)*10;
        }
        if(Mathf.Floor(time) >= 100)
        {
            collecting = false;
            Debug.Log("Collect End");
            time = 0;
            Destroy(Bot);
            normalCollected += normalCollecting;
            normalCollecting = 0;
        }
        normalText.GetComponent<Text>().text = "普通資源：" + (normalCollected + normalCollecting);
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
                
                Bot= Instantiate(BotModel, BotPos.transform);
                
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
        normalCollected = data.normalCollected;
    }
    public void SaveData(GameData data)
    {
        data.normalCollected = normalCollected;
    }
}
