using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour {

    public Transform[] spawnGate;
    public float timeBetweenWaves, spawnRate;
    public int[] waveSize;
    public GameObject[] monsterType;
    public static int numberOfMonsters = 0;

    private int currentWave;
    private bool spawnPause;

    void Start() {
        currentWave = 0;
        StartCoroutine(spawnMonster());
    }

    IEnumerator spawnMonster()
    {
        foreach (Transform t in spawnGate)
        {
            while (numberOfMonsters > 10)
            {
                yield return null;
            }
            Instantiate(monsterType[0], t.position, Quaternion.identity);

        }

        yield return new WaitForSeconds(spawnRate);
        waveSize[currentWave]--;
        if (waveSize[currentWave] > 0)
            StartCoroutine(spawnMonster());
        else
        {
            currentWave++;
            if (currentWave < waveSize.Length)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
                StartCoroutine(spawnMonster());
            }
        }
    }
}
