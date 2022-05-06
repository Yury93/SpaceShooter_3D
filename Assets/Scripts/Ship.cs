using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Destructible
{
    [SerializeField] private List<GameObject> turrets;

    public override void Start()
    {
        base.Start();
        foreach (var tur in turrets)
        {
            tur.gameObject.SetActive(false);
        }
    }
    public void CheckAllTurret()
    {
        foreach (var tur in turrets)
        {
            tur.gameObject.SetActive(true);
        }
        IEnumerator CorDisable()
        {
            yield return new WaitForSeconds(30f);
            foreach (var tur in turrets)
            {
                tur.gameObject.SetActive(false);
            }
        }
    }
}
