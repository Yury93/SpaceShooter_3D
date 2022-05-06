using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rbProjectile;
    [SerializeField] private float speed;
    [SerializeField] private float timeLives;
    [SerializeField] private int damage;
    private Turret turret;
    void Update()
    {
        if (turret && turret.Shooter)
        {
            var dir = turret.transform.position - turret.Shooter.transform.position;
            dir.Normalize();

            rbProjectile.AddForce(dir * speed * Time.deltaTime);
            timeLives -= Time.deltaTime;
            if (timeLives <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetTurret(GameObject obj)
    {
        turret = obj.GetComponent<Turret>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != turret.Shooter.gameObject.tag)
        {
            var des = other.gameObject.GetComponentInParent<Destructible>();
            if (des)
            {
                des.ApplyDamage(damage);
                if(des.Increadible)
                {
                    des.CheckIncreadible(false);
                }
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
