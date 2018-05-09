using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariables : MonoBehaviour {

    public float health;
    public float pushbackForce, hitCooldownTime = 1.0f;
    public int damage;
    public AudioClip damageSound;
    public GameObject player, healthPack;
    
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;
    private float vol;
    private float hitCooldown;
    private float distance;
    private AudioSource aSource;

    void Start ()
    {
        hitCooldown = hitCooldownTime;

        aSource = GetComponent<AudioSource>();

        player = GameObject.Find("hitman1_gun");
    }


    void FixedUpdate () {
        if (hitCooldown > 0)
            hitCooldown -= Time.deltaTime;

        distance = Vector3.Distance(transform.position, player.transform.position);

        if  (distance < 1.7 && hitCooldown <= 0)
        {
            player.GetComponent<PlayerVariables>().takeDamage(damage, transform);
            hitCooldown = hitCooldownTime;
        }

    }

    public void takeDamage(float amount, Transform source)
    {
        vol = Random.Range(volLowRange, volHighRange);

        aSource.PlayOneShot(damageSound, 1);

        health -= amount;
        Vector3 dir = source.position - transform.position;

        dir = -dir.normalized;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushbackForce);

        if (health <= 0)
        {
            if (Random.Range(0.0f, 1.0f) > 0.85f)
                Instantiate(healthPack, transform.position, Quaternion.identity);
            Debug.Log(transform.position);
            MonsterSpawn.numberOfMonsters--;
            DestroyObject(this.gameObject);
        }

    }
   
}
