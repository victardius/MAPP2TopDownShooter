using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour {

    public Transform[] spawnGate;
    public float timeBetweenWaves, spawnRate;
    public int[] waveSize;
    public GameObject monsterType;
    public Transform playerTarget;

    private int currentWave;

    void Start() {
        currentWave = 0;
        StartCoroutine(spawnMonster());
    }

    IEnumerator spawnMonster()
    {
        foreach (Transform t in spawnGate)
        {
            Instantiate(monsterType, t.position, Quaternion.identity);
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
