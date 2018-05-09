using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    
    public AudioClip pistol;
    public Sprite handGun;
    public Sprite rifle;
    public Sprite shotgun;

    public static float bulletDamage;

    public Transform bulletTrailPrefab;

    float timeToFire = 0;
    Transform firePoint;

    private AudioSource source;
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;
    private int weaponChoice = 1;
    private SpriteRenderer sprite;
    private float damagePistol = 10;
    private float damageRifle = 6;
    private float damageShotgun = 15;
    private float fireRate = 0;



    void Awake () {
        bulletDamage = damagePistol;
        source = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firepoint");
        }
		
	}

    void increaseDamage()
    {
        PlayerPrefs.SetFloat("pistolDamage", (damagePistol = damagePistol * 1.1f));
        PlayerPrefs.SetFloat("riflDamage", (damageRifle = damageRifle * 1.1f));
        PlayerPrefs.SetFloat("shotgunDamage", (damageShotgun = damageShotgun * 1.1f));
    }
	
	// Update is called once per frame
	void Update () {
#if UNITY_STANDALONE_WIN
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
#endif

#if UNITY_ANDROID

        
            if (PlayerController.primaryShooting && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        
#endif
    }

    void Shoot()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(pistol, vol);
        //Vector2 fireTargetPosition = new Vector2(firePointDirection.position.x, firePointDirection.position.y);
        //Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        //RaycastHit2D hit = Physics2D.Raycast(firePointPosition, fireTargetPosition, 100, whatToHit);
        Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation);
        /*Debug.DrawLine(firePointPosition, (fireTargetPosition - firePointPosition) * 100);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage");
        }*/
    }

    void selectWeapon()
    {
        weaponChoice++;
        if (weaponChoice > 3)
            weaponChoice = 1;

        if (weaponChoice == 1)
        {
            bulletDamage = damagePistol;
            sprite.sprite = handGun;
            fireRate = 1;
        }
            
        else if (weaponChoice == 2)
        {
            bulletDamage = damageRifle;
            sprite.sprite = rifle;
            fireRate = 3;
        }
            
        else if (weaponChoice == 3)
        {
            bulletDamage = damageShotgun;
            sprite.sprite = shotgun;
            fireRate = 0.5f;
        }
       

    }

    
}
