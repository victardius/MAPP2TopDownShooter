using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    //public bool ricochet;

    public int damage = 10;
    public LayerMask whatToHit;
    public int moveSpeed = 230;
    public Transform target;
    public float timeToLive;
    //public PhysicsMaterial2D bounce;

    private float dot;
    private Rigidbody2D rgbd;
    Vector3 reflection;

    Transform firePoint;
    Transform firePointDirection;
    ContactPoint2D contact;

    private void Start()
    {
        //ricochet = false;
        rgbd = this.gameObject.GetComponent<Rigidbody2D>();
        Shoot();
        firePoint = transform.Find("FirePoint");
        firePointDirection = transform.Find("FirePointDirection");
    }

    


    void FixedUpdate()
    {
        Vector2 fireTargetPosition = new Vector2(firePointDirection.position.x, firePointDirection.position.y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, fireTargetPosition, 0.3f, whatToHit);

        //Debug.DrawLine(firePointPosition, hit.point );
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Walls"))
                DestroyObject(this.gameObject);
            else if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<EnemyVariables>().takeDamage(damage, transform);
                DestroyObject(this.gameObject);
            }
            //hit.collider.gameObject.GetComponent<MeleeEnemyBehaviour>().takeDamage((int)Damage, transform);
            //Debug.DrawLine(firePointPosition, hit.point, Color.red);
           // Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage");
        }
    }

    


    private void Shoot()
    {
        /*if (ricochet)
            rgbd.sharedMaterial = bounce;
        else
            rgbd.sharedMaterial = null;*/
        rgbd.velocity = (target.transform.position - transform.position).normalized * moveSpeed;
        DestroyObject(this.gameObject, timeToLive);
    }
}
