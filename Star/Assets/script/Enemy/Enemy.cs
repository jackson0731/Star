using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [SerializeField] private FieldOfView FOV;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public int hp = 5;
    public bool enemyDead = false;

    public void LoadData(GameData data)
    {
        data.enemyLeft.TryGetValue(id, out enemyDead);
        if (enemyDead)
        {
            gameObject.SetActive(false);
        }
    }
    public void SaveData(GameData data)
    {
        if (data.enemyLeft.ContainsKey(id))
        {
            data.enemyLeft.Remove(id);
        }
        data.enemyLeft.Add(id, enemyDead);
    }

    void Update()
    {
        if(hp <= 0)
        {
            enemyDead = true;
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage()
    {
        hp--;
        FOV.canSeePlayer = true;
    }
}
