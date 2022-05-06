using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectContainer : Singleton<EffectContainer>
{
    [SerializeField] private GameObject explosion;
    public void EffectExplosion(Vector3 pos)
    {
        var effect = Instantiate(explosion, pos, Quaternion.identity);
        Destroy(effect, 5f);
    }
}
