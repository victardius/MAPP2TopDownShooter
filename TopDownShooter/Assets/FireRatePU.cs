using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePU : MonoBehaviour {

    public int fireRate;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("hitman1_gun");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            player.GetComponentInChildren<Weapon>().firePowerUp();
            //player.GetComponent<Weapon>().firePowerUp();
            DestroyObject(this.gameObject);
        }
    }
}
