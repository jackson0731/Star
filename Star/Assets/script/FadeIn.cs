using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    public Animation[] anim;
    private bool canFadeOut;
    public GameObject text;

    void Awake()
    {
        StartCoroutine(WaitSce());
    }
    void Update()
    {
        if (canFadeOut && Input.anyKey)
        {
            anim[5].Play("Fade in");
            text.SetActive(true);
            SceneManager.LoadSceneAsync("1-1");
        }
    }

    private IEnumerator WaitSce()
    {
        anim[0].Play("Fade in");
        yield return new WaitForSeconds(3);
        anim[1].Play("Fade in");
        yield return new WaitForSeconds(3);
        anim[2].Play("Fade in");
        yield return new WaitForSeconds(3);
        anim[3].Play("Fade in");
        yield return new WaitForSeconds(3);
        anim[4].Play("Fade in");
        yield return new WaitForSeconds(1);
        canFadeOut = true;
    }
}
