using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

    public int moveSpeed = 230;
    public GameObject target;

    private Rigidbody2D rgbd;

    private void Start()
    {
     //   firePointDirection = gameObject.Find("FirePointDirection");
        rgbd = this.gameObject.GetComponent<Rigidbody2D>();
        Shoot();
    }

    // Update is called once per frame
    void Update () {

       

        //rgbd.velocity = (target.transform.position - transform.position).normalized * moveSpeed;

    }

    private void Shoot()
    {
        rgbd.velocity = (target.transform.position - transform.position).normalized * moveSpeed;

        Debug.Log("bullet gets speed");
        //rgbd.AddForce((target.transform.position - transform.position) * moveSpeed * Time.smoothDeltaTime);


    }
}
