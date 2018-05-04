using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text amountOfEnemies, levelEnd, waveAnnouncement, gameOver;
    public GameObject spawner, levelEndPanel;

    [HideInInspector]
    public static bool missionFailed = false;

    private int wave, level;

    private void Start()
    {
        Time.timeScale = 1;
        levelEnd.gameObject.SetActive(false);
        waveAnnouncement.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        levelEndPanel.gameObject.SetActive(false);
        wave = 0;
        StartCoroutine(waveControl());
        missionFailed = false;
    }

    private void FixedUpdate()
    {
        if (MonsterSpawn.monstersSpawned && MonsterSpawn.numberOfMonsters == 0)
        {
            levelEnd.gameObject.SetActive(true);
            levelEnded();
            level = 0;
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
            level = 0;
        }

    }

    public void levelEnded()
    {
        levelEndPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void changeLevel()
    {

        SceneManager.LoadScene(level);
    }

    IEnumerator waveControl()
    {
        waveAnnouncement.gameObject.SetActive(true);
        waveAnnouncement.text = "Wave " + (spawner.GetComponent<MonsterSpawn>().getCurrentWave() + 1) + " incoming!";
        yield return new WaitForSeconds(2.0f);
        waveAnnouncement.gameObject.SetActive(false);
    }

}
