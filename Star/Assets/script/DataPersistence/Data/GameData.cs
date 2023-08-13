using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public Vector3 playerPosition;

    public GameData()
    {
        this.level = 0;
        playerPosition = Vector3.zero;
    }
}
