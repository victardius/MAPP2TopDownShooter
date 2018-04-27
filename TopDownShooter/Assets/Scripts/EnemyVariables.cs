using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariables : MonoBehaviour {

    public int health;
    public float pushbackForce, hitCooldownTime = 1.0f;
    public int damage;
    public AudioClip damageSound;
    public GameObject player;
    
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;
    private float vol;
    private float hitCooldown;
    private AudioSource aSource;

    void Start ()
    {
        hitCooldown = hitCooldownTime;

        aSource = GetComponent<AudioSource>();
    }


    void FixedUpdate () {
        if (hitCooldown > 0)
            hitCooldown -= Time.deltaTime;

    }

    public void takeDamage(int amount, Transform source)
    {
        vol = Random.Range(volLowRange, volHighRange);

        aSource.PlayOneShot(damageSound, 1);

        health -= amount;
        Vector3 dir = source.position - transform.position;

        dir = -dir.normalized;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushbackForce);

        if (health <= 0)
        {
            MonsterSpawn.numberOfMonsters--;
            DestroyObject(this.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (hitCooldown <= 0)
            {
                other.GetComponent<PlayerVariables>().takeDamage(damage, transform);
                hitCooldown = hitCooldownTime;
            }
        }
    }
}
