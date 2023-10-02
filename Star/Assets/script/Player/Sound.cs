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

    public float Soundloss = 3f;
    

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
        
        if (slider == null)
        {
             slider = sliderObject.GetComponent<Slider>();
        }

        UpdateSound();
    }

    public void minusSound(float amount)
    {
        currentSound -= amount;
        currentSound = Mathf.Clamp(currentSound, 0f, maxSound); // 限制能量值在0到最大值之間
        
    }

    public void addSound(float amount)
    {
        Soundloss = 3f;
        currentSound += amount;
        currentSound = Mathf.Clamp(currentSound, 0f, maxSound); // 限制能量值在0到最大值之間
        
    }

    void UpdateSound()
    {
        float fillAmount = currentSound / maxSound; // 計算填充值比例
        slider.value = fillAmount;
    }
}
