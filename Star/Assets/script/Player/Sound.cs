using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Sound : MonoBehaviour
{
    public Slider slider;
    public float maxSound = 100f;

    public float currentSound;

    private float Soundloss = 3f;

    public GameObject normalLight;
    public GameObject redLight;
    public Animation[] anim;

    // Start is called before the first frame update
    void Start()
    {
        currentSound = 0;
        UpdateSound();
    }

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        GameObject sliderObject = GameObject.Find("Player Sound");
        if(currentScene != "2")
        {
            if (slider == null)
            {
                slider = sliderObject.GetComponent<Slider>();
            }
        }
        if (currentSound != 100)
        {
            if (Soundloss <= 0)
            {
                currentSound -= 5f;
                UpdateSound();
                Soundloss = 3f;
            }
            else
            {
                Soundloss -= Time.deltaTime;
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

    public void minusSound(float amount)
    {
        currentSound -= amount;
        currentSound = Mathf.Clamp(currentSound, 0f, maxSound); // 限制能量值在0到最大值之間
        UpdateSound();
    }

    public void addSound(float amount)
    {
        Soundloss = 3f;
        currentSound += amount;
        currentSound = Mathf.Clamp(currentSound, 0f, maxSound); // 限制能量值在0到最大值之間
        UpdateSound();
    }

    private void UpdateSound()
    {
        float fillAmount = currentSound / maxSound; // 計算填充值比例
        slider.value = fillAmount;
    }
}
