using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehaviour : MonoBehaviour {

    public Transform playerTarget;
    public float speed, chargeSpeed, hitCooldownTime = 1.0f, pushbackForce;
    public int health;
    


    private float startSpeed, distance, hitCooldown;

	void Start () {

        startSpeed = speed;
        hitCooldown = hitCooldownTime;

    }

    void Update () {
        
        if (hitCooldown > 0)
        hitCooldown -= Time.deltaTime;

        distance = Vector3.Distance(transform.position, playerTarget.position);

        if (distance < 5)
            speed = chargeSpeed - (distance/10);
        else
            speed = startSpeed;

        transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, speed * Time.fixedDeltaTime);
        

	}

    public void takeDamage(int amount, Transform source)
    {
        health -= amount;
        Vector3 dir = source.position - transform.position;

        dir = -dir.normalized;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushbackForce);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (hitCooldown <= 0)
            {
                other.GetComponent<PlayerVariables>().takeDamage(10, transform);
                hitCooldown = hitCooldownTime;
            }
        }
    }
}
