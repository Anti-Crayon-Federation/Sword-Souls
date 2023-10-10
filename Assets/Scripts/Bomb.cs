using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionCountdown;
    bool activated=false;
    public Sprite explosionSprite;
    public float explosionSize;
    public float explosionTime;
    public float explosionDamage;

    // Update is called once per frame
    void Update()
    {
        if(activated)
        {
            explosionCountdown -= Time.deltaTime;
        }

        if(explosionCountdown<=0)
        {
            GameObject explosion= new GameObject ("explosion");
            explosion.transform.position = gameObject.transform.position;
            Explosion hitbox = explosion.AddComponent<Explosion>();
            hitbox.damage = explosionDamage;
            hitbox.lifeTime = explosionTime;
            SpriteRenderer sr = explosion.AddComponent<SpriteRenderer>();
            sr.color = Color.red;
            sr.sprite = explosionSprite;
            sr.drawMode = SpriteDrawMode.Sliced;
            sr.size = new Vector2(explosionSize, explosionSize);
            CircleCollider2D radius = explosion.AddComponent<CircleCollider2D> ();
            radius.radius = explosionSize;
            radius.isTrigger = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
            Debug.Log("Bomb is about to explode");
            activated = true;
        }
        
    }
}
