using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    [SerializeField] private int score;
    [SerializeField] private Text hpText,scoreTxt;
    private Destructible destructible;
    
    private void Start()
    {
        destructible = ship.GetComponent<Destructible>();

        destructible.OnDamage += PlayerOnDamage;
        hpText.text = $"HP: {destructible.CurrentHP}";

        Enemy.OnEnemyDeath += EnemyDeath;
        scoreTxt.text = $"SCORE: {score}";
    }
    private void EnemyDeath()
    {
        score++;
        scoreTxt.text = $"SCORE: {score}";
        PlayerPrefs.SetInt("Score", score);
    }
    private void PlayerOnDamage()
    {
        hpText.text = $"HP:{destructible.CurrentHP}";
       if( destructible.CurrentHP<= 0)
        {
            hpText.text = $"HP:0";
        }
    }
}
