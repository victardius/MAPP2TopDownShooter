using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoorScript : MonoBehaviour {

    public GameObject openDoor;
    public GameObject lockedDoor;
    private bool open = false;
    private BoxCollider2D collider;



	// Use this for initialization
	void Start () {
        collider = lockedDoor.GetComponent<BoxCollider2D>();
        openDoor.SetActive(false);
        open = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void openTheDoor()
    {
        if (!open)
        {
            lockedDoor.SetActive(false);
            openDoor.SetActive(true);
            collider.enabled = !collider.enabled;
            open = true;
        }
    }
}
