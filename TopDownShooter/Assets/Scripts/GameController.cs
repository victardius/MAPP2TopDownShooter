using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
    public Text amountOfEnemies, levelEnd, waveAnnouncement, gameOver;
    public GameObject spawner, levelEndPanel, pauseScreen;
    public Button play;
    public Sprite pausedImage, unpausedImage;
    private static GameController control;
    [HideInInspector]
    public static bool missionFailed = false;

    private int wave;
    public GameObject player;
    

    /*private void Awake()
    {
        
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != null)
        {
            Destroy(gameObject);
        }
        
    }*/

    private void Start()
    {
        
        player = GameObject.Find("hitman1_gun");
        
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
        player.GetComponent<PlayerController>().SavePlayer();
        Time.timeScale = 0f;
       
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
