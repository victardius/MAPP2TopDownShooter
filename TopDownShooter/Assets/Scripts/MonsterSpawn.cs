﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour {

    public Transform[] spawnGate;
    public float timeBetweenWaves, spawnRate;
    public int[] waveSize;
    public GameObject[] monsterType;
    public int monsterLimit;

    [HideInInspector]
    public static int numberOfMonsters = 0;
    [HideInInspector]
    public static bool monstersSpawned;

    private int currentWave, monsterIndex = 0, monsterCounter = 0;
    private bool spawnPause;
    private int[] divideMonster;

    void Start() {
        monstersSpawned = false;
        currentWave = 0;
        StartCoroutine(spawnMonster());
        divideMonster = new int[monsterType.Length];

        for (int i = 0; i < divideMonster.Length; i++)
        {
            divideMonster[i] = i * 4 -1;
        }
        
    }

    IEnumerator spawnMonster()
    {
        foreach (Transform t in spawnGate)
        {
            
            yield return new WaitUntil(() => numberOfMonsters < monsterLimit);
            
            if (waveSize[currentWave] > 0)
            {
                if (monsterCounter != 0)
                    for (int i = 0; i < divideMonster.Length; i++)
                        if (divideMonster[i] == -1 || monsterCounter % divideMonster[i] == 0)
                            monsterIndex = i;

                

                Instantiate(monsterType[monsterIndex], t.position, Quaternion.identity);
                monsterCounter++;
                waveSize[currentWave]--;

            }
            
            yield return new WaitForSeconds(0.1f);

        }

        yield return new WaitForSeconds(spawnRate);

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
            else
            {
                monstersSpawned = true;
            }
        }
    }
}
