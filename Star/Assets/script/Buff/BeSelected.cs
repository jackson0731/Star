using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeSelected : MonoBehaviour
{
    public GameObject BuffSelectIcon;

    public void PointerEnter()
    {
        BuffSelectIcon.SetActive(true);
    }

    public void PointerExit()
    {
        BuffSelectIcon.SetActive(false);
    }
}
