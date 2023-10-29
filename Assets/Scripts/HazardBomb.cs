using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBomb : MonoBehaviour
{
    public bool bombActivated = false;
    public Sprite explosionSprite;
    public float bombCountdown;
    public float explosionSize;
    public float explosionLifetime;
    public int explosionDamage;
    SpriteRenderer sr;
    CircleCollider2D radius;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        radius = gameObject.AddComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bombActivated)
        {
            bombCountdown -= Time.deltaTime;
        }

        if (bombCountdown <= 0)
        {
            sr.color = Color.red;
            sr.sprite = explosionSprite;
            sr.drawMode = SpriteDrawMode.Sliced;
            sr.size = new Vector2(explosionSize, explosionSize);
            radius.radius = explosionSize;
            radius.isTrigger = true;
        }

        if (bombCountdown <= -explosionLifetime)
        {
            Destroy(gameObject);
        }
    }

    public void ActivateBomb()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
        Debug.Log("Bomb is about to explode");
        bombActivated = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("something entered!");
        if (collision.gameObject.name == "Player")
        {
            PlayerHealth e = collision.GetComponent<PlayerHealth>();
            if (e != null)
            {
                e.TakeDamange(explosionDamage);
                Debug.Log(e.currentHealth);
                Debug.Log("attack hit player");
            }
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Enemy e = collision.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamange(explosionDamage, gameObject);
                Debug.Log(e.health);
                Debug.Log("attack hit enemy");
            }
        }

        if (collision.gameObject.name == "Obstacle")
        {
            Debug.Log("attack hit obstacle");
            GameObject obstacle = collision.gameObject;
            Destroy(obstacle);
        }
    }
}
