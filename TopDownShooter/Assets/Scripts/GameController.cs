﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Text amountOfEnemies, levelEnd, waveAnnouncement, gameOver;
    public GameObject spawner, levelEndPanel, pauseScreen;
    public Button play;
    public Sprite pausedImage, unpausedImage;

    [HideInInspector]
    public static bool missionFailed = false;

    private int wave;
    private GameObject player;

    private void Start()
    {
        player = player = GameObject.Find("hitman1_gun");
        player.GetComponent<PlayerVariables>().LoadPlayer();
        Time.timeScale = 1;
        levelEnd.gameObject.SetActive(false);
        waveAnnouncement.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        levelEndPanel.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);
        wave = 0;
        StartCoroutine(waveControl());
        missionFailed = false;
    }

    private void FixedUpdate()
    {
        if (MonsterSpawn.monstersSpawned && MonsterSpawn.numberOfMonsters == 0)
        {
            levelEnd.gameObject.SetActive(true);
            Currency.CurrencyValueMultiplier +=1;
            levelEnded();
        }

        amountOfEnemies.text = "" + MonsterSpawn.numberOfMonsters;

        if (wave < spawner.GetComponent<MonsterSpawn>().getCurrentWave())
        {
            StartCoroutine(waveControl());
            wave = spawner.GetComponent<MonsterSpawn>().getCurrentWave();
        }

        if (missionFailed)
        {
            gameOver.gameObject.SetActive(true);
            levelEnded();
        }

    }

    public void levelEnded()
    {
        levelEndPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        player.GetComponent<PlayerVariables>().SavePlayer();
    }

    public void changeLevel(int n)
    {

        SceneManager.LoadScene(n);
    }

    public void pauseContinueGame()
    {
        if (pauseScreen.activeSelf)
        {
            if (Time.timeScale == 0.0f)
                Time.timeScale = 1.0f;
            play.image.sprite = unpausedImage;
        }
        else
        {
            Time.timeScale = 0.0f;
            play.image.sprite = pausedImage;
        }
        pauseScreen.SetActive(!pauseScreen.activeSelf);

    }

    public void abandonMission()
    {
        changeLevel(0);
    }

    IEnumerator waveControl()
    {
        waveAnnouncement.gameObject.SetActive(true);
        waveAnnouncement.text = "Wave " + (spawner.GetComponent<MonsterSpawn>().getCurrentWave() + 1) + " incoming!";
        yield return new WaitForSeconds(2.0f);
        waveAnnouncement.gameObject.SetActive(false);
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && !pauseScreen.activeSelf)
        {   

            pauseContinueGame();
        }
    }

}
