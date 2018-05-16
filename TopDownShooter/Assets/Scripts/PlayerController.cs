using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    public float moveForce = 20;
    Rigidbody2D rgbd;
    private Animator anim;
    private float speed = 0;

    public static bool primaryShooting;
    public bool secondaryShooting;

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
        speed += 10f;
        Debug.Log(speed);
        yield return new WaitForSeconds(6f);
        speed -= 10;
        Debug.Log(speed);
    }

    void FixedUpdate()
    {
#if UNITY_STANDALONE_WIN

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = angle - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        float h = Input.GetAxisRaw("Horizontal") * (moveForce + speed);
        float v = Input.GetAxisRaw("Vertical") * (moveForce + speed);
        rgbd.AddForce(new Vector2(h, v));
#endif
#if UNITY_ANDROID

            Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"),
                 CrossPlatformInputManager.GetAxis("Vertical")) * moveForce;

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
}