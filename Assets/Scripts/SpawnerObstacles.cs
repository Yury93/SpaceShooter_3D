using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObstacles : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distanceOfPlayer;
    [SerializeField] private float radius, startTimer, time;
    [SerializeField] private bool loop;
    [SerializeField] private GameObject prefab;

    private void Start()
    {
        if (!loop)
        {
            Spawn();
        }
    }
    private void Update()
    {
        if (player)
        {
            var posSpawner = new Vector3(player.position.x
                , player.position.y,
                player.position.z + distanceOfPlayer);

            transform.position = posSpawner;

            if (loop)
            {
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    Spawn();
                    time = startTimer;
                }
            }
        }
    }
    private void Spawn()
    {
        var rnd = RandomCircle(transform.position, Random.Range(0, radius));
        var obj = Instantiate(prefab, rnd, Quaternion.identity);
        obj.GetComponent<Obstacle>().SetPlayer(player);
    }
    public Vector3 RandomCircle(Vector3 center, float radius)
    {
        var ang = Random.value * 360;
        Vector3 pos = new Vector3();
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
}
