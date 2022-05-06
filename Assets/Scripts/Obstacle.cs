using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    [SerializeField] private float timeLive;
    private void Start()
    {
        Destroy(gameObject, timeLive);
    }

    void Update()
    {
        if(player)
        {
            rb.AddForce(-transform.forward * speed * Time.deltaTime);
        }
    }
    public void SetPlayer(Transform player)
    {
        this.player = player;
    }
    private void OnCollisionEnter(Collision collision)
    {
        var des = collision.gameObject.GetComponent<Destructible>();
        if(des)
        {
            des.ApplyDamage(10000);
        }
    }
}
