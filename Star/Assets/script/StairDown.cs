using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairDown : MonoBehaviour
{
    public bool TD = true;
    [SerializeField] private MeshCollider TDCollider;
    [SerializeField] private Stair Stair;
    // Start is called before the first frame update
    void Start()
    {
        TD = true;
    }

    // Update is called once per frame
    void Update()
    {
        TrapdoorOn();
    }

    void TrapdoorOn()
    {
        if (TD == false || Stair.StairTD == false)
        {
            TDCollider.enabled = false;
        }
        else
        {
            TDCollider.enabled = true;
        }
    }
}
