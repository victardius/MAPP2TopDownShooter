using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text amountOfEnemies, levelEnd, waveAnnouncement, gameOver;
    public GameObject spawner;

    [HideInInspector]
    public static bool missionFailed = false;

    private int wave;

    private void Start()
    {
        Time.timeScale = 1;
        levelEnd.gameObject.SetActive(false);
        waveAnnouncement.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        wave = 0;
        StartCoroutine(waveControl());
    }

    private void FixedUpdate()
    {
        if (MonsterSpawn.monstersSpawned && MonsterSpawn.numberOfMonsters == 0)
        {
            levelEnd.gameObject.SetActive(true);
            StartCoroutine(levelEnded(0));
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
            StartCoroutine(levelEnded(0));
        }

    }

    IEnumerator levelEnded(int n)
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(n);
    }

    IEnumerator waveControl()
    {
        waveAnnouncement.gameObject.SetActive(true);
        waveAnnouncement.text = "Wave " + (spawner.GetComponent<MonsterSpawn>().getCurrentWave() + 1) + " incoming!";
        yield return new WaitForSeconds(2.0f);
        waveAnnouncement.gameObject.SetActive(false);
    }

}
