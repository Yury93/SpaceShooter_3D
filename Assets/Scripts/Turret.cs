using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject shooter;
    public GameObject Shooter => shooter;
    [SerializeField] private Projectile projectile;
    private float speedShot;
    [SerializeField] private float rateShot;
    private bool powerUp;
    
    private void Update()
    {
        speedShot -= Time.deltaTime;
        if(speedShot <= 0)
        {
            var p = Instantiate(projectile, transform.position, Quaternion.identity);
            p.SetTurret(gameObject);
            if (!powerUp)
            {
                speedShot = rateShot;
            }
            else
            {
                speedShot = 0.5f;
            }
        }
    }
    public void CheckSpeedProjectile()
    {
        powerUp = true;
        StartCoroutine(CorReturnSpeed());

        IEnumerator CorReturnSpeed()
        {
            yield return new WaitForSeconds(30f);
            powerUp = false;
        }
    }
}
