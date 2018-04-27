using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

    public int health = 10;
    public GameObject Player;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log(Player.GetComponent<PlayerVariables>().health);
            Player.GetComponent<PlayerVariables>().healthPack(health);
            Debug.Log(Player.GetComponent<PlayerVariables>().health);
        }
    }

}
