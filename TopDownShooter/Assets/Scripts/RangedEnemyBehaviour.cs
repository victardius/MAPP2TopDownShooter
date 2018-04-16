﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class RangedEnemyBehaviour : MonoBehaviour
{

    public Transform playerTarget;
    public float speed, hitCooldownTime = 1.0f, pushbackForce, updateRate = 2.0f, nextWaypointDistance = 0.1f;
    public int health;
    public Path path;
    public ForceMode2D fMode;
    public Transform bullets;
    Transform firePoint;

    [HideInInspector]
    public bool pathIsEnded = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    private float startSpeed, distance, hitCooldown;
    private int currentWaypoint = 0;
    private bool fire = false;


    void Start()
    {

        startSpeed = speed;
        hitCooldown = hitCooldownTime;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        firePoint = transform.Find("FirePoint");

        if (playerTarget == null)
        {
            Debug.LogError("No Player found!");
        }

        seeker.StartPath(transform.position, playerTarget.position, OnPathComplete);

        StartCoroutine(UpdatePath());
        StartCoroutine(ShootPlayer());

    }

    void Update()
    {

        if (hitCooldown > 0)
            hitCooldown -= Time.deltaTime;

        distance = Vector3.Distance(transform.position, playerTarget.position);

        if (distance < 5)
        {
            speed = 0f;
            fire = true;
        }
        else
        {
            speed = startSpeed;
            fire = false;
        }
            

        //transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, speed * Time.fixedDeltaTime);


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

    IEnumerator ShootPlayer()
    {
        if (fire == true)
        {
            Instantiate(bullets, firePoint.position, firePoint.rotation);

            yield return new WaitForSeconds(1.0f / updateRate);
            Debug.Log("shooting working");

            StartCoroutine(ShootPlayer());
            
        }
        yield return new WaitForSeconds(1.0f / updateRate);

        StartCoroutine(ShootPlayer());

    }

    IEnumerator UpdatePath()
    {

        seeker.StartPath(transform.position, playerTarget.position, OnPathComplete);

        yield return new WaitForSeconds(1.0f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void takeDamage(int amount, Transform source)
    {
        health -= amount;
        Vector3 dir = source.position - transform.position;

        dir = -dir.normalized;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushbackForce);

    }
    private void FixedUpdate()
    {
        if (playerTarget == null)
        {
            return;
        }

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

        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }

    }

    
}
