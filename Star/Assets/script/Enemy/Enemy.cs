using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [SerializeField] private FieldOfView FOV;
    [SerializeField] private Animator Ani;
    [SerializeField] int Stun;
    [SerializeField] int GetHit;
    [SerializeField] float StunTime;
    [SerializeField] float StunTimer;
    [SerializeField] BeeAttack BeeATK;

    bool StunAni;
    bool PlayDeadAni;

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
            if (!PlayDeadAni)
            {
                PlayDeadAni = true;
                Ani.SetTrigger("Dead");
            }
        }

        StunTimeCount();
    }

    void StunTimeCount()
    {
        if (GetHit == Stun)
        {
            Ani.SetTrigger("BeingHit");
            BeeATK.BeamAtkEnd();
            GetHit = 0;
            FOV.ActState = FieldOfView.ActionState.Stun;
        }

        if (GetHit != 0)
        {
            StunTimer += Time.deltaTime;
            if (StunTimer > StunTime)
            {
                GetHit = 0;
                StunTimer = 0;            }
        }
        
    }

    public void TakeDamage(float Damage)
    {
        hp=hp-Damage;
        FOV.canSeePlayer = true;
        GetHit++;
        
        
    }

    public void ReFromStun()
    {
        FOV.ActState = FieldOfView.ActionState.Standy;
        
    }

    public void Dead()
    {
        gameObject.SetActive(false);
        //GameObject.Find("BuffManager").GetComponent<BuffSelect>().enemyCount -= 1;
    }
}
