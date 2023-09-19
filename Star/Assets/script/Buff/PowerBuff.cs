using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff/PlayerSpeedBuff")]

public class PowerBuff : Buff
{
    public float amount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().speed += amount;
    }
}
