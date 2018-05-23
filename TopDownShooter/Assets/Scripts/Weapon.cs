using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    
    public AudioClip pistol;
    public AudioClip rifleBlast;
    public AudioClip shotgunBlast;
   // public Sprite handGun;
   // public Sprite rifle;
   // public Sprite shotgun;
    

    public static float bulletDamage;
    public static int shotgunAmmo = 10;
    public static int rifleAmmo = 50;
    public static int currentAmmo = 1337;

    public Transform bulletTrailPrefab;
    public Transform shotgunBullets;

    float timeToFire = 0;
    Transform firePoint;
    Transform firePointShotgun1;
    Transform firePointShotgun2;
    Transform firePointShotgun3;
    Transform firePointShotgun4;
    Transform firePointPistol;

    private float fireBuff = 0;

    private AudioSource source;
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;
    private int weaponChoice = 1;
    private SpriteRenderer sprite;
    private float fireRate = 1.4f;
    private Animator anim;
    



    void Awake () {
        
        bulletDamage = PlayerPrefs.GetFloat("pistolDamage", 10f);
        source = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        firePoint = transform.Find("FirePoint");
        firePointPistol = transform.Find("FirePointPistol");
        firePointShotgun1 = transform.Find("FirePointShotgun1");
        firePointShotgun2 = transform.Find("FirePointShotgun2");
        firePointShotgun3 = transform.Find("FirePointShotgun3");
        firePointShotgun4 = transform.Find("FirePointShotgun4");
        anim = GetComponentInParent<Animator>();
        if (firePoint == null)
        {
            Debug.LogError("No firepoint");
        }
        anim.SetInteger("weaponChoice", weaponChoice);
		
	}

	
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
                timeToFire = Time.time + 1 / (fireRate + fireBuff);
                Shoot();
            }
        }
#endif

#if UNITY_ANDROID

        
            if (PlayerController.primaryShooting)
            {
            anim.SetBool("shooting", true);
            if (Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / (fireRate + fireBuff);
                Shoot();
            }
        }
        else
        {
            anim.SetBool("shooting", false);
        }
        
#endif
    }

    void Shoot()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        //Vector2 fireTargetPosition = new Vector2(firePointDirection.position.x, firePointDirection.position.y);
        //Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        //RaycastHit2D hit = Physics2D.Raycast(firePointPosition, fireTargetPosition, 100, whatToHit);
        if (weaponChoice == 3)
        {
            if (shotgunAmmo != 0)
            {
                source.PlayOneShot(shotgunBlast, vol);
                Instantiate(shotgunBullets, firePoint.position, firePoint.rotation);
                Instantiate(shotgunBullets, firePointShotgun1.position, firePointShotgun1.rotation);
                Instantiate(shotgunBullets, firePointShotgun2.position, firePointShotgun2.rotation);
                Instantiate(shotgunBullets, firePointShotgun3.position, firePointShotgun3.rotation);
                Instantiate(shotgunBullets, firePointShotgun4.position, firePointShotgun4.rotation);
                shotgunAmmo--;
                currentAmmo = shotgunAmmo;
            }
        }
        
        else if (weaponChoice == 2)
        {
            if (rifleAmmo != 0)
            {
                source.PlayOneShot(rifleBlast, vol);
                Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation);
                rifleAmmo--;
                currentAmmo = rifleAmmo;
            }
        }
        else if (weaponChoice == 1)
        {
            source.PlayOneShot(pistol, vol);
            Instantiate(bulletTrailPrefab, firePointPistol.position, firePointPistol.rotation);
            currentAmmo = 1337;
        }
        /*Debug.DrawLine(firePointPosition, (fireTargetPosition - firePointPosition) * 100);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage");
        }*/
    }

    public void selectWeapon()
    {
        weaponChoice++;
        if (weaponChoice > 3)
        {
            weaponChoice = 1;
        }

        if (weaponChoice == 1)
        {
            bulletDamage = PlayerPrefs.GetFloat("pistolDamage", 10f);
            //sprite.sprite = handGun;
            fireRate = 1.4f;
            currentAmmo = 1337;
            anim.SetInteger("weaponChoice", weaponChoice);
        }
            
        else if (weaponChoice == 2)
        {
            bulletDamage = PlayerPrefs.GetFloat("rifleDamage", 6f);
            //sprite.sprite = rifle;
            fireRate = 3.4f;
            currentAmmo = rifleAmmo;
            anim.SetInteger("weaponChoice", weaponChoice);
        }
            
        else if (weaponChoice == 3)
        {
            bulletDamage = PlayerPrefs.GetFloat("shotgunDamage", 12f);
            //sprite.sprite = shotgun;
            fireRate = 0.7f;
            currentAmmo = shotgunAmmo;
            anim.SetInteger("weaponChoice", weaponChoice);
        }
       

    }

    public void firePowerUp(float fireRateBuff)
    {
        StartCoroutine(firePUHelpCode(fireRateBuff));
    }
    public IEnumerator firePUHelpCode(float fireRateBuff) {
        fireBuff += fireRateBuff;
        Debug.Log(fireBuff);
        yield return new WaitForSeconds(6f);
        fireBuff -= fireRateBuff;
        Debug.Log(fireBuff);
    }

    
}
