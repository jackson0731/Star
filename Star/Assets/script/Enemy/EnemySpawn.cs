using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Camera mainCamera;
    public GameObject spawn;

    void Update()
    {
        if (IsOutsideCameraView())
        {
            Debug.Log("Spawn");
            //SpawnEnemy();
        }
    }

    bool IsOutsideCameraView()
    {
        Vector3 enemyScreenPos = mainCamera.WorldToScreenPoint(spawn.transform.position);
        return enemyScreenPos.x < 0 || enemyScreenPos.x > Screen.width || enemyScreenPos.y < 0 || enemyScreenPos.y > Screen.height;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawn.transform.position, Quaternion.identity);
    }
}
