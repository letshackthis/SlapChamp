using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParams : MonoBehaviour
{
    private static int playerHealth = 100, playerDamage, enemyHealth = 100, enemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        int playerBonusHealth = ES3.Load("PlayerHealth", 0);
        int playerBonusDamage = ES3.Load("PlayerDamage", 0);
        playerHealth += playerBonusHealth;
        playerDamage += playerBonusDamage;

    }

    private void OnApplicationPause(bool pause)
    {

        ES3.Save("PlayerHealth", ES3.Load("PlayerHealth", 0));
        ES3.Save("PlayerDamage", ES3.Load("PlayerDamage", 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
