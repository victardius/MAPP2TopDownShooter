using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerController : MonoBehaviour
{
    private static PlayerController control;
    public float moveForce = 20;
    Rigidbody2D rgbd;
    private Animator anim;
    private float speedPU = 0;
    private int health;
    private int shield;
    public static bool primaryShooting;
    public bool secondaryShooting;
    public GameObject player;

    private void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != null)
        {
            Destroy(gameObject);
        }
        player = GameObject.Find("hitman1_gun");
    }
void Start()
    {
       
        rgbd = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    public void increaseMovement(){
        StartCoroutine(movementPowerUp());
    }
    public IEnumerator movementPowerUp()
    {
        speedPU += 20f;
        Debug.Log(speedPU);
        yield return new WaitForSeconds(6f);
        speedPU -= 20;
        Debug.Log(speedPU);
    }

    void FixedUpdate()
    {
#if UNITY_STANDALONE_WIN

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = angle - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        float h = Input.GetAxisRaw("Horizontal") * (moveForce + speedPU);
        float v = Input.GetAxisRaw("Vertical") * (moveForce + speedPU);
        rgbd.AddForce(new Vector2(h, v));
#endif
#if UNITY_ANDROID

            Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"),
                 CrossPlatformInputManager.GetAxis("Vertical")) * (moveForce + speedPU);

             Vector3 primaryLookVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal_2"),
                 CrossPlatformInputManager.GetAxis("Vertical_2"), 4000);

            Vector3 secondaryLookVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"),
                 CrossPlatformInputManager.GetAxis("Vertical"), 4000);

       
        primaryShooting = primaryLookVec.x != 0 && primaryLookVec.y != 0;
        secondaryShooting = secondaryLookVec.x != 0 && secondaryLookVec.y != 0;

             if(primaryShooting){
                 transform.rotation = Quaternion.LookRotation(primaryLookVec, Vector3.back);
                 moveForce = 25;
        }
             else if(secondaryShooting){
                 transform.rotation = Quaternion.LookRotation(secondaryLookVec, Vector3.back);
                 moveForce = 40;
        }

        float speed = CrossPlatformInputManager.GetAxis("Horizontal") + CrossPlatformInputManager.GetAxis("Vertical");
        if (speed < 0)
            speed *= -1;
        anim.SetFloat("speed", speed);
        

             rgbd.AddForce(moveVec);
#endif

    }
    public void SavePlayer()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Stats.dat");
        Stats stats = new Stats(health, shield);
        if (player.GetComponent<PlayerVariables>().health < 20)
        {
            player.GetComponent<PlayerVariables>().health = 50;
        }
        stats.health = player.GetComponent<PlayerVariables>().health;
        stats.shield = player.GetComponent<PlayerVariables>().shield;
        bf.Serialize(file, stats);
        file.Close();
        Debug.Log("Saving");
    }
    public void LoadPlayer()
    {
        if (File.Exists(Application.persistentDataPath + "/Stats.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Stats.dat", FileMode.Open);
            Stats stats = (Stats)bf.Deserialize(file);
            file.Close();
            player.GetComponent<PlayerVariables>().health = stats.health;
            player.GetComponent<PlayerVariables>().shield = stats.shield;
            
            Debug.Log("Loading");
        }
    }
}

[Serializable]
class Stats
{
    public int health;
    public int shield;

    public Stats(int health, int shield)
    {
        this.health = health;
        this.shield = shield;
    }
    public int getHealth()
    {
        return health;
    }
    public int getShield()
    {
        return shield;
    }

}
