using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorExplosion : MonoBehaviour {

    public int damage = 20;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyVariables>().takeDamage(damage, transform);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerVariables>().takeDamage(damage, transform);
        }
    }

    public void explosionTrigger()
    {
        StartCoroutine(explosion());
    }

    private IEnumerator explosion()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

}
