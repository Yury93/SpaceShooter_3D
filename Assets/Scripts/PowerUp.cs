using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUp : MonoBehaviour
{
    private Ship ship;
    [SerializeField] private bool restoreHp, armor,speedProjectile,killAll,allTurrets;
    [SerializeField] private GameObject restoreHpGO, armorGO, speedProjectileGO, killAllGO, allTurretsGO;
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private int rnd;
    private void Start()
    {
        rnd = Random.Range(0, 10);
        switch(rnd)
        {
            case 1:
                restoreHpGO.SetActive(true);
                restoreHp = true;
                    break;
            case 2:
                armorGO.SetActive(true);
                armor = true;
                break;
            case 3:
                speedProjectileGO.SetActive(true);
                speedProjectile = true;
                break;
            case 4:
                killAllGO.SetActive(true);
                killAll = true;
                break;
            case 5:
                allTurretsGO.SetActive(true);
                allTurrets = true;
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        ship = other.gameObject.GetComponentInParent<Ship>();
        if(ship)
        {
            AddHP();
            AddIncreadible();
            AddSpeedProjectile();
            AddBonusKillAll();
            AddTurrets();
            Destroy(gameObject);
        }
    }
    private void AddHP()
    {
        if (restoreHp)
        {
            ship.RestoreHP();
        }
        Destroy(gameObject);
    }
    public void AddIncreadible()
    {
        if(armor)
        {
            ship.CheckIncreadible(true);
        }
    }
    private void AddSpeedProjectile()
    {
        if (speedProjectile)
        {
            ship.gameObject.GetComponentInChildren<Turret>().CheckSpeedProjectile();
        }
    }
    private void AddBonusKillAll()
    {
        if(killAll)
        {
            enemies = FindObjectsOfType<Enemy>();
            //if(enemies.Length > 0)
            foreach (var enemy in enemies)
            {
                enemy.ApplyDamage(100000);
            }
        }
    }
    private void AddTurrets()
    {
        if(allTurrets)
        {
            ship.CheckAllTurret();
        }
    }

}
