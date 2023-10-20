using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public int deathCount;
    public int level;
    public float normalCollected;
    public float rareCollected;
    public SerializableDictionary<string, bool> enemyLeft;

    public GameData()
    {
        deathCount = 0;
        this.level = 0;
        normalCollected = 0;
        rareCollected = 0;
        enemyLeft = new SerializableDictionary<string, bool>();
    }
}
