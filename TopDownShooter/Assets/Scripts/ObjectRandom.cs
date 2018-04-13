using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectRandom : MonoBehaviour {

    public GameObject[] objects = new GameObject[3];
    public GameObject box;
    public GameObject blomma;
    public GameObject lyse;
    static int ammountOfFlowers = 0;
    private float boxScale;

    private int spawnChance = 35;

	// Use this for initialization
	void Start () {
        int spawnCheck = (int)Random.Range(0,100);
        int objectSpawn = (int)Random.Range(0, 3);
        boxScale = Random.Range(0.0f,1.0f);

        if (spawnCheck <= spawnChance)
        {
            if(objectSpawn == 0)
            {
                Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z);
                Debug.Log(boxScale);
                box.transform.localScale += new Vector3(boxScale, boxScale, 0f);
                Instantiate(box, position, Quaternion.identity);
            }
            else if(objectSpawn == 1)
            {
                Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                Instantiate(lyse, position, Quaternion.identity);
            }
            else
            {
                if(ammountOfFlowers < 5)
                {
                    Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    Instantiate(blomma, position, Quaternion.identity);
                }
                else
                {
                    Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    Instantiate(box, position, Quaternion.identity);
                }

            }

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
