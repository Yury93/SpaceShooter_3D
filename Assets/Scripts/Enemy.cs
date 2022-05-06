using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructible
{
    [SerializeField] private TextMesh hpTxt;
    public TextMesh HpTxt => hpTxt;
    private SpawnerPowerUp spawnerPW;
    public static event Action OnEnemyDeath;
    private void Start()
    {
        hpTxt.text = CurrentHP.ToString();
        OnDamage += EnemyOnDamage;
        Helpers.UI.TargetsShowHandler.instance.AddTarget(gameObject);
    }
    private void EnemyOnDamage()
    {
        hpTxt.text = CurrentHP.ToString();
        if (CurrentHP <= 0)
        {
            hpTxt.text = "0";
            OnEnemyDeath?.Invoke();
            spawnerPW.SpawnPowerUp(transform.position);
            Helpers.UI.TargetsShowHandler.instance.RemoveTarget(gameObject);
            OnDamage -= EnemyOnDamage;
        }
    }
    public void GetSpawnerPW(SpawnerPowerUp spawnerPower)
    {
        spawnerPW = spawnerPower;
    }
}
