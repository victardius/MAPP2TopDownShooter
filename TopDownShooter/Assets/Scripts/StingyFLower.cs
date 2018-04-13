using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingyFLower : MonoBehaviour {

    private int damage = 1;
    private int hitPoints = 3;
    public GameObject player;
    Rigidbody2D playerRigd;

    private void Start()
    {
        playerRigd = player.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update () {
        if (hitPoints <= 0)
        {
            this.gameObject.SetActive(false);

        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerVariables>().takeDamage(10);          
            Debug.Log("Damage taken, plant hp left: " + hitPoints);
            hitPoints -= 1;

            
        }
    }

}
