using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResPoi : MonoBehaviour
{
    public bool Collecting;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GainResouce();
    }

    public void GainResouce()
    {
        
        if (Collecting)
        {
            Debug.Log("ResourceGain");
        }
    }
}
