using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    Transform target;
    public GameObject player; 
        
	// Use this for initialization
	void Start () {
        target = player.transform; //GameObject.Find ("").transform;
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.position + new Vector3 (0, 0, -10);
	}
}
