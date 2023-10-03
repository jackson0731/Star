using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [SerializeField] private FieldOfView FOV;
    [SerializeField] private Animator Ani;

    bool PlayDeadAni;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public float hp = 10f;
    public bool enemyDead = false;

    private void Awake()
    {
        if(id == null)
        {
            GenerateGuid();
        }
    }

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
            if (!PlayDeadAni)
            {
                PlayDeadAni = true;
                Ani.SetTrigger("Dead");
            }
        }
    }

    public void TakeDamage(float Damage)
    {
        hp=hp-Damage;
        FOV.canSeePlayer = true;
        if (FOV.ActState != FieldOfView.ActionState.CloseCombat && FOV.ActState != FieldOfView.ActionState.LongRangeAttack)
        {
            Ani.SetTrigger("BeingHit");
        }
    }

    public void Dead()
    {
        gameObject.SetActive(false);
        GameObject.Find("BuffManager").GetComponent<BuffSelect>().enemyCount -= 1;
    }
}
