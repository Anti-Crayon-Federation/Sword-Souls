using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage;
    public float lifeTime;


    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("something entered!");
        if (collision.gameObject.name == "Player")
        {
            PlayerHealth e = collision.GetComponent<PlayerHealth>();
            if (e != null)
            {
                e.TakeDamange(damage);
                Debug.Log(e.currentHealth);
                Debug.Log("attack hit player");
            }
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Enemy e = collision.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamange(damage,gameObject);
                Debug.Log(e.health);
                Debug.Log("attack hit enemy");
            }
        }        

        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("attack hit obstacle");
            GameObject obstacle = collision.gameObject;
            Destroy(obstacle);
        }
    }
}
