using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour {

    public Transform[] spawnGate;
    public float timeBetweenWaves;

    private float timeBetweenWavesStart;

	// Use this for initialization
	void Start () {
        timeBetweenWavesStart = timeBetweenWaves;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeBetweenWaves > 0)
        {
            timeBetweenWaves -= Time.deltaTime;
        }
	}
}
