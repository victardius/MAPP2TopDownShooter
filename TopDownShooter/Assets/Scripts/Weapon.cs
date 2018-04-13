﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 0;
    public float Damage = 10;
    public LayerMask whatToHit;

    float timeToFire = 0;
    Transform firePoint;
    Transform firePointDirection;


    void Awake () {
        firePoint = transform.Find("FirePoint");
        firePointDirection = transform.Find("FirePointDirection");
        if (firePoint == null)
        {
            Debug.LogError("No firepoint");
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        Shoot();
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1")){
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
		
	}

    void Shoot()
    {
        Vector2 fireTargetPosition = new Vector2(firePointDirection.position.x, firePointDirection.position.y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, fireTargetPosition, 100, whatToHit);
        Debug.DrawLine(firePointPosition, (fireTargetPosition - firePointPosition) * 100);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage");
        }
    }
}
