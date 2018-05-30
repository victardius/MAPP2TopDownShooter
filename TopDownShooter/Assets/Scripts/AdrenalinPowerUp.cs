using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalinPowerUp : MonoBehaviour {

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("hitman1_gun");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().increaseMovement();
            player.GetComponent<PlayerVariables>().healthPack(10);
            DestroyObject(this.gameObject);
        }
    }
}
