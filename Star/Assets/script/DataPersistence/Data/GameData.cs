using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public int deathCount;
    public int level;
    public Vector3 playerPosition;

    public GameData()
    {
        deathCount = 0;
        this.level = 0;
        playerPosition = Vector3.zero;
    }
}
