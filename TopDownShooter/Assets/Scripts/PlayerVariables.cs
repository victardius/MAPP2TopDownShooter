using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVariables : MonoBehaviour {

    public int health;
    public Slider healthSlider;

	void Start () {
		
	}
	

	void Update () {

        healthSlider.value = health;
        
	}

    public void takeDamage(int amount)
    {
        health -= amount;

    }

}
