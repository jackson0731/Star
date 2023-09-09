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

    public float hp = 10f;
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

    public void TakeDamage(float Damage)
    {
        hp=hp-Damage;
        FOV.canSeePlayer = true;
    }
}
