using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectRandom : MonoBehaviour {

    public GameObject[] objects = new GameObject[3];
    public GameObject box;
    public GameObject blomma;
    public GameObject lyse;
    static int ammountOfFlowers = 0;
    private float boxScale, randXPos, randYPos;

    private int spawnChance = 35;

	// Use this for initialization
	void Start () {
        int spawnCheck = (int)Random.Range(0,100);
        int objectSpawn = (int)Random.Range(0, 3);
        boxScale = Random.Range(0.0f,0.3f);
        box.transform.localScale = new Vector3(0.1f, 0.1f, 0f);
        randXPos = Random.Range(0.0f, 2.0f) - 1.0f;
        randYPos = Random.Range(0.0f, 2.0f) - 1.0f;
        Vector3 position = new Vector3(this.transform.position.x + randXPos, this.transform.position.y + randYPos, this.transform.position.z);

        if (spawnCheck <= spawnChance)
        {
            if(objectSpawn == 0)
            {
                //Debug.Log(boxScale);
                
                box.transform.localScale += new Vector3(boxScale, boxScale, 0f);
                Instantiate(this.box, position, Quaternion.identity);
            }
            else if(objectSpawn == 1)
            {
                Instantiate(lyse, position, Quaternion.identity);
            }
            else
            {
                if(ammountOfFlowers < 5)
                {
                    Instantiate(blomma, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(box, position, Quaternion.identity);
                }

            }

        }

    }
}
