using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damage = 10;
    public LayerMask whatToHit;
    //public Transform bulletTrailPrefab;

    
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
            hit.collider.gameObject.GetComponent<MeleeEnemyBehaviour>().takeDamage(damage, transform);
            DestroyObject(this.gameObject);
            //hit.collider.gameObject.GetComponent<MeleeEnemyBehaviour>().takeDamage((int)Damage, transform);
            //Debug.DrawLine(firePointPosition, hit.point, Color.red);
           // Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage");
        }
    }
}
