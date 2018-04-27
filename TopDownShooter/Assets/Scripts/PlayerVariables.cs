using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVariables : MonoBehaviour {

    public int health = 100;
    public int shield = 3;
    public GameObject shieldSprite;
    public Slider healthSlider;
    public Slider shieldSlider;
    public float pushbackForce = 10;
    public static Transform playerTarget;

	void Start () {
        playerTarget = transform;
        shieldSprite.SetActive(true);

        health = 100;
	}
	

	void Update () {

        healthSlider.value = health;
        shieldSlider.value = shield;

        if (health <= 0)
            GameController.missionFailed = true;
        
	}

    public void healthPack(int x)
    {
        if (health <= 90)
            health += x;
    }

    public void shieldPack(int x)
    {
        if(shield < 3)
        {
            shield ++;
            shieldSprite.SetActive(true);
        }
    }

    public void takeDamage(int amount, Transform enemy)
    {
        if (shield >= 1)
        {
            Debug.Log("Shield" + shield);
            shield --;
        }
        else
        {
            shieldSprite.SetActive(false);
            health -= amount;
        }
        Vector3 dir = enemy.position - transform.position;
        dir = -dir.normalized;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushbackForce);




    }

}
