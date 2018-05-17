using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class PlayerVariables : MonoBehaviour {
    public static PlayerVariables control;
    public int health = 100;
    public int shield = 3;
    public GameObject shieldSprite;
    public Slider healthSlider;
    public Slider shieldSlider;
    public float pushbackForce = 10;
    public static Transform playerTarget;
    public AudioClip damageSound;

    private AudioSource source;
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;

    private void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if( control != this)
        {
            Destroy(gameObject);
        }
            if (health < 20)
        {
            health = 40;
        }
    }
    void Start () {
        
        source = GetComponent<AudioSource>();
        playerTarget = transform;
        shieldSprite.SetActive(true);
      
        
        
	}
	

	void Update () {

        healthSlider.value = health;
        shieldSlider.value = shield;

        if (health <= 0)
            GameController.missionFailed = true;

        if (shield <= 0)
            shieldSprite.SetActive(false);

    }

    public void healthPack(int x)
    {
        if (health <= 90)
            health += x;
    }

    public void shieldPack(int x)
    {
        if(shield < 3)
        {
            shield ++;
            shieldSprite.SetActive(true);
        }
    }

    public void takeDamage(int amount, Transform enemy)
    {
        if (shield >= 1)
        {
            Debug.Log("Shield" + shield);
            shield --;
        }
        else
        {
            float vol = UnityEngine.Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(damageSound, vol);
            health -= amount;
        }
        Vector3 dir = enemy.position - transform.position;
        dir = -dir.normalized;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushbackForce);




    }

    public GameObject getPlayer()
    {
        return this.gameObject;
    }
    public void SavePlayer()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerStats.dat", FileMode.Open);
        PlayerStats stats = new PlayerStats(health, shield);
        stats.health = health;
        stats.shield = shield;
        bf.Serialize(file, stats);
        file.Close();
    }
    public void LoadPlayer()
    {
        if(File.Exists(Application.persistentDataPath + "/playerStats.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerStats.dat", FileMode.Open);
            PlayerStats stats = (PlayerStats)bf.Deserialize(file);
            file.Close();
            health = stats.health;
            shield = stats.shield;
        }
    }
}

[Serializable]
class PlayerStats
{
    public int health;
    public int shield;

    public PlayerStats(int health, int shield)
    {
        this.health = health;
        this.shield = shield;
    }
    public int GetHealth()
    {
        return health;
    }
    public int GetShield()
    {
        return shield;
    }
    public void SetHealth()
    {
        health = this.health;
    }
}