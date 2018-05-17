﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class TankEnemyBehaviour : MonoBehaviour {

    public float speed, updateRate = 2.0f, nextWaypointDistance = 0.1f, hitCooldownTime = 2.0f;
    //public int health;
    public Path path;
    public ForceMode2D fMode;
    public int damage;

    [HideInInspector]
    public bool pathIsEnded = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    private float startSpeed, distance, updateRateStart;
    private int currentWaypoint = 0;
    private Transform playerTarget;
    private GameObject player;
    private Animator anim;
    private float hitCooldown;


    void Start () {

        hitCooldown = hitCooldownTime;

        playerTarget = PlayerVariables.playerTarget;
        startSpeed = speed;
        MonsterSpawn.numberOfMonsters++;
        updateRateStart = updateRate;

        player = GameObject.Find("hitman1_gun");

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (playerTarget == null)
        {
            Debug.LogError("No Player found!");
        }

        seeker.StartPath(transform.position, playerTarget.position, OnPathComplete);

        StartCoroutine(UpdatePath());

        anim = GetComponent<Animator>();
    }


    public void OnPathComplete(Path p)
    {
        //Debug.Log("Got path, error? " + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    IEnumerator UpdatePath()
    {

        seeker.StartPath(transform.position, playerTarget.position, OnPathComplete);

        yield return new WaitForSeconds(1.0f / updateRate);
        StartCoroutine(UpdatePath());
    }

    /*public void takeDamage(int amount, Transform source)
    {
        health -= amount;
        Vector3 dir = source.position - transform.position;

        dir = -dir.normalized;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushbackForce);

        if (health <= 0)
        {
            MonsterSpawn.numberOfMonsters--;
            DestroyObject(this.gameObject);
        }

    }*/
    private void FixedUpdate()
    {

        if (hitCooldown > 0)
            hitCooldown -= Time.deltaTime;


        distance = Vector3.Distance(transform.position, playerTarget.position);

        
            speed = startSpeed;
            updateRate = updateRateStart;
        

        var lookDir = playerTarget.position - transform.position;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if (!anim.GetBool("death"))
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (path != null)
        {


            if (currentWaypoint >= path.vectorPath.Count)
            {
                if (pathIsEnded)
                    return;
                //Debug.Log("End of path reached.");
                pathIsEnded = true;
                return;
            }

            pathIsEnded = false;

            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            dir *= speed * Time.fixedDeltaTime;

            if (distance > 1.5 && !anim.GetBool("death"))
                rb.AddForce(dir, fMode);

            float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

            if (dist < nextWaypointDistance)
            {
                currentWaypoint++;
                return;
            }
        }

        if (distance < 2.5 && hitCooldown <= 0 && !anim.GetBool("death"))
        {
            player.GetComponent<PlayerVariables>().takeDamage(damage, transform);
            hitCooldown = hitCooldownTime;
        }

    }

    
}