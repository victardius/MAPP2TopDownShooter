using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float Damage = 10;
    public LayerMask whatToHit;
    public Transform bulletTrailPrefab;

    
    private float dot;
    Vector3 reflection;

    Transform firePoint;
    Transform firePointDirection;
    ContactPoint2D contact;

    void Awake () {
        firePoint = transform.Find("FirePoint");
        firePointDirection = transform.Find("FirePointDirection");
    }


    void FixedUpdate()
    {
        Vector2 fireTargetPosition = new Vector2(firePointDirection.position.x, firePointDirection.position.y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, fireTargetPosition, 100, whatToHit);

        Debug.DrawLine(firePointPosition, fireTargetPosition );
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        contact = collision.contacts[0];
        dot = Vector3.Dot(contact.normal, (-transform.forward));
        dot *= 2;
        reflection = contact.normal * dot;
        reflection = reflection + transform.forward;
    }


    void Update () {
		
	}
}
