using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text amountOfEnemies, levelEnd, waveAnnouncement;
    public GameObject spawner;

    private int wave;

    private void Start()
    {
        levelEnd.gameObject.SetActive(false);
        waveAnnouncement.gameObject.SetActive(false);
        wave = 0;
        StartCoroutine(waveControl());
    }

    private void FixedUpdate()
    {
        if (MonsterSpawn.monstersSpawned && MonsterSpawn.numberOfMonsters == 0)
            StartCoroutine(levelComplete());

        amountOfEnemies.text = "" + MonsterSpawn.numberOfMonsters;

        if (wave < spawner.GetComponent<MonsterSpawn>().getCurrentWave())
        {
            StartCoroutine(waveControl());
            wave = spawner.GetComponent<MonsterSpawn>().getCurrentWave();
        }

    }

    IEnumerator levelComplete()
    {
        levelEnd.gameObject.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(0);
    }

    IEnumerator waveControl()
    {
        waveAnnouncement.gameObject.SetActive(true);
        waveAnnouncement.text = "Wave " + (spawner.GetComponent<MonsterSpawn>().getCurrentWave() + 1) + " incoming!";
        yield return new WaitForSeconds(2.0f);
        waveAnnouncement.gameObject.SetActive(false);
    }

}
