using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;
    
    public void SpawnPowerUp(Vector3 pos)
    {
        var power = Instantiate(powerUpPrefab, pos, Quaternion.identity);
    }
}
