using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShots : MonoBehaviour {

    public int moveSpeed = 230;
    public float timeToLive;

    private Transform target;
    private Rigidbody2D rgbd;

    private void Start()
    {
        target = PlayerVariables.playerTarget;
        rgbd = this.gameObject.GetComponent<Rigidbody2D>();
        Shoot();
    }


    private void Shoot()
    {
        rgbd.velocity = (target.position - transform.position).normalized * moveSpeed;
        Destroy(this.gameObject, timeToLive);
    }
}
