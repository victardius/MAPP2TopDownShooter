using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVariables : MonoBehaviour {

    public int health = 100;
    public Slider healthSlider;
    public float pushbackForce = 10;
    public static Transform playerTarget;

	void Start () {
        playerTarget = transform;
	}
	

	void Update () {

        healthSlider.value = health;

        if (health <= 0)
            GameController.missionFailed = true;
        
	}

    public void takeDamage(int amount, Transform enemy)
    {
        health -= amount;
        Vector3 dir = enemy.position - transform.position;

        dir = -dir.normalized;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushbackForce);

    }

}
