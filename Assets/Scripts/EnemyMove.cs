using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform transfEnemy;
    [SerializeField] private float step;
    private GameObject player;
    [SerializeField] private float distance;
    private void Start()
    {
        transfEnemy = GetComponent<Transform>();
    }
    void Update()
    {
        if (player)
        {
            var dir = player.transform.position - transform.position;
            if (dir.magnitude > distance)
            {
                transfEnemy.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            }
            transform.LookAt(player.transform.position);
        }
        else
        {
            gameObject.transform.Translate(Vector3.forward * 30 * Time.deltaTime);
        }
    }
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
