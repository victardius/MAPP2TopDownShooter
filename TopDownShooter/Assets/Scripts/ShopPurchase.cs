using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPurchase : MonoBehaviour {

    public static float shopPriceMultiplier = 1;

    public void increaseDamage()
    {

        PlayerPrefs.SetFloat("pistolDamage", (PlayerPrefs.GetFloat("pistolDamage", 10f) * 1.1f));
        PlayerPrefs.SetFloat("rifleDamage", (PlayerPrefs.GetFloat("rifleDamage", 6f) * 1.1f));
        PlayerPrefs.SetFloat("shotgunDamage", (PlayerPrefs.GetFloat("shotgunDamage", 15f) * 1.1f));
        shopPriceMultiplier += 0.1f;
    }

    public void increaseHealth()
    {
        PlayerPrefs.SetFloat("playerHealth", (PlayerPrefs.GetFloat("playerHealth", 100) * 1.1f));
        shopPriceMultiplier += 0.1f;

    }

    public void increaseShield()
    {
        PlayerPrefs.SetInt("playerShield", (PlayerPrefs.GetInt("playerShield", 3) + 1));
        shopPriceMultiplier += 0.1f;

    }

    public void buyEnergyCells()
    {

    }
}
