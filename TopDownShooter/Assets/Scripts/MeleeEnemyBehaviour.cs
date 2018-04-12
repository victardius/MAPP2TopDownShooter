using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehaviour : MonoBehaviour {

    public Transform playerPosition;
    public float speed;

    private float xPosition, yPosition;

	// Use this for initialization
	void Start () {
        xPosition = playerPosition.position.x - this.transform.position.x;

        yPosition = playerPosition.position.y - this.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (playerPosition.position.x - this.transform.position.x > 1)
            xPosition = 1;
        else if (playerPosition.position.x - this.transform.position.x < -1)
            xPosition = -1;
        else
            xPosition = playerPosition.position.x - this.transform.position.x;



        yPosition = playerPosition.position.y - this.transform.position.y;
        
        

        transform.Translate(new Vector3(xPosition, yPosition, 0.0f) * speed * Time.deltaTime);

	}
}
