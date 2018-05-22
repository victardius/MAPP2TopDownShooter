﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public static Currency CurrencyControl;
    bool loadingGame = false;
    public static int credits;

    public static float currencyValueMultiplier = 1f;
    public void StartCurrency()
    {
        credits = 0;
        PlayerPrefs.SetFloat("Currency", 0f);
    }
    public void Awake()
    {
        if (CurrencyControl == null)
        {
            DontDestroyOnLoad(gameObject);
            CurrencyControl = this;
        }
        else if (CurrencyControl != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (loadingGame == true)
        {
           
            currencyValueMultiplier = PlayerPrefs.GetFloat("CurrencyValue");
        }
        currencyValueMultiplier = 1f;
    }

    
    void Update()
    {

    }
    public void GainCurrency()
    {
        PlayerPrefs.SetFloat("Currency", (PlayerPrefs.GetFloat("Currency", 0f) + 2f * currencyValueMultiplier));
    }

    /*public void EndGame()
    {
        if (GameInstance.SaveGame == true)
        {
            PlayerPrefs.SetFloat("CurrencyValueMultiplier", CurrencyValueMultiplier);
        } 
    }*/
}
