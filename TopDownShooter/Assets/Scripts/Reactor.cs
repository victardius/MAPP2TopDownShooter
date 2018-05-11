using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour {

    public Sprite disabledSprite;
    public GameObject explosionCollider;
    public GameObject explosionEffect;

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
            explosionEffect.SetActive(true);
            explosionCollider.SetActive(true);
            explosionCollider.GetComponent<ReactorExplosion>().explosionTrigger();
        }
    }

    
}
