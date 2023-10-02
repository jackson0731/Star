using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageQuest : MonoBehaviour
{
    public GameObject notBeFoundQuest;
    public GameObject noDamageQuest;

    public bool notBeFound = true;
    public bool noDamage = true;
    private float playerMaxHp;

    void Awake()
    {
        playerMaxHp = GameObject.Find("Player").GetComponent<Player>().hp;
    }

    void Update()
    {
        if (notBeFound)
        {
            notBeFoundQuest.GetComponent<Text>().color = Color.green;
        }
        else
        {
            notBeFoundQuest.GetComponent<Text>().color = Color.red;
        }
        if (noDamage)
        {
            noDamageQuest.GetComponent<Text>().color = Color.green;
        }
        else
        {
            noDamageQuest.GetComponent<Text>().color = Color.red;
        }

        QuestCheck();
    }

    private void QuestCheck()
    {
        if(GameObject.Find("Player").GetComponent<Sound>().currentSound >= 100f)
        {
            notBeFound = false;
        }

        if(playerMaxHp > GameObject.Find("Player").GetComponent<Player>().hp)
        {
            noDamage = false;
        }
    }
}
