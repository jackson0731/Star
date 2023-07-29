using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public bool speedup = false;
    // Update is called once per frame
    void Update()
    {
        if (speedup)
        {
            gameObject.GetComponent<Player>().speed += 2;
        }
    }
}
