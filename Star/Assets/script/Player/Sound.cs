using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Sound : MonoBehaviour
{
    public Slider playerSound;
    public Slider warning;
    public float maxSound = 100f;

    public float currentSound;
    public float enemyWarning;

    public float Soundloss = 3f;
    

    // Start is called before the first frame update
    void Start()
    {
        currentSound = 0;
        enemyWarning = 0;
        UpdateSound();
    }

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        GameObject sliderObject = GameObject.Find("Player Sound");
        GameObject sliderObject2 = GameObject.Find("Warning");
        
        if (warning == null)
        {
             warning = sliderObject2.GetComponent<Slider>();
        }
        if(playerSound == null)
        {
            playerSound = sliderObject.GetComponent<Slider>();
        }
        if(currentSound < 0)
        {
            currentSound = 0;
        }
        if(enemyWarning < 0)
        {
            enemyWarning = 0;
        }
        if (warning != null && playerSound != null)
        {
            UpdateSound();
        }
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
        enemyWarning += amount;
        currentSound = Mathf.Clamp(currentSound, 0f, maxSound); // 限制能量值在0到最大值之間
        enemyWarning = Mathf.Clamp(enemyWarning, 0f, maxSound); // 限制能量值在0到最大值之間
        
    }

    void UpdateSound()
    {
        float fillAmount = enemyWarning / maxSound; // 計算填充值比例
        float fillAmount2 = currentSound / maxSound; // 計算填充值比例
        warning.value = fillAmount;
        playerSound.value = fillAmount2;
    }
}
