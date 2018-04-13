﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public float moveForce = 5;
    Rigidbody2D rgbd;

	void Start () {

        rgbd = this.GetComponent<Rigidbody2D>();
		
	}
	
	void FixedUpdate () {
#if UNITY_STANDALONE_WIN

        float h = Input.GetAxisRaw("Horizontal") * moveForce;
        float v = Input.GetAxisRaw("Vertical") * moveForce;

        rgbd.AddForce(new Vector2(h, v));
#endif
#if UNITY_ANDROID

        Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"),
             CrossPlatformInputManager.GetAxis("Vertical")) * moveForce;

         Vector3 lookVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal_2"),
             CrossPlatformInputManager.GetAxis("Vertical_2"), 4000);

         if(lookVec.x != 0 && lookVec.y != 0)
             transform.rotation = Quaternion.LookRotation(lookVec, Vector3.back);

         rgbd.AddForce(moveVec);
#endif


    }
}