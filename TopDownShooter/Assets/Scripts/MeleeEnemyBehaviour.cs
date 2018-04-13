using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehaviour : MonoBehaviour {

    public Transform playerTarget;
    public float speed, chargeSpeed, hitCooldownTime = 1.0f, hitCooldown;


    private float startSpeed, distance;

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
