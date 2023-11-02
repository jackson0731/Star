using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] Sound Sound;
    [SerializeField] Animation[] anim;
    [SerializeField] GameObject normalLight;
    [SerializeField] GameObject redLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Sound == null)
        {
            GameObject Player = GameObject.Find("Player");
            Sound = Player.GetComponent<Sound>();
        }

        TurnOnAlarm();
    }

    void TurnOnAlarm()
    {
        if (Sound.currentSound != 100)
        {
            if (Sound.Soundloss <= 0)
            {
                Sound.currentSound -= 10f;
                Sound.enemyWarning -= 5f;
                
                Sound.Soundloss = 3f;
            }
            else
            {
                Sound.Soundloss -= Time.deltaTime;
            }
            normalLight.SetActive(true);
            redLight.SetActive(false);
        }
        else
        {
            normalLight.SetActive(false);
            redLight.SetActive(true);
            anim[0].Play("Flash");
            anim[1].Play("Flash");
            anim[2].Play("Flash");
            anim[3].Play("Flash");
            anim[4].Play("Flash");
        }
    }
}
