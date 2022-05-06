using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private int currentHp;
    public int CurrentHP => currentHp;
    [SerializeField] private int startHP;
    [SerializeField] private bool increadible;
    public bool Increadible => increadible;

    public event Action OnDamage;
    public virtual void Start()
    {
        startHP = currentHp;
    }
    public void ApplyDamage(int damage)
    {
        if (!increadible)
        {
            currentHp -= damage;
            OnDamage?.Invoke();
            if (currentHp <= 0)
            {
                EffectContainer.Instance.EffectExplosion(gameObject.transform.position);
                Destroy(gameObject, 0.1f);
            }
        }
    }
    public void RestoreHP()
    {
        currentHp = startHP;
    }
    public void CheckIncreadible(bool incr)
    {
        increadible = incr;
    }
    public void RandomEnemyHp()
    {
        currentHp = UnityEngine.Random.Range(2, 20);
    }
}
