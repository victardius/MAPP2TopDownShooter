using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour {

    public Sprite disabledSprite;
    public GameObject explosionCollider;

    private int damage = 1;
    private ParticleSystem particleEffect;
    private bool exploded = false;

    // Use this for initialization
    void Start () {
        particleEffect = GetComponent<ParticleSystem>();
        particleEffect.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void explode()
    {
        if (!exploded)
        {
            Debug.Log("sprängd");
            exploded = true;
            particleEffect.Stop();
            GetComponent<SpriteRenderer>().sprite = disabledSprite;
            explosionCollider.SetActive(true);
        }
    }

    
}
