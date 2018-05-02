using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor: MonoBehaviour {

    private int damage = 1;
    private int hitPoints = 3;
    public GameObject player;
    public Sprite disabled;
    //Rigidbody2D playerRigd;
    private ParticleSystem particleEffect;

    private void Start()
    {
        //playerRigd = player.GetComponent<Rigidbody2D>();
        particleEffect = GetComponent<ParticleSystem>();
        particleEffect.Play();
    }
    // Update is called once per frame
    void Update () {
        

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerVariables>().takeDamage(10, transform);          
            Debug.Log("Damage taken, plant hp left: " + hitPoints);
            hitPoints -= 1;
            if (hitPoints <= 0)
            {
                GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;
                GetComponent<SpriteRenderer>().sprite = disabled;
                particleEffect.Stop();

            }

        }
    }

}
