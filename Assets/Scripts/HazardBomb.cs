using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBomb : MonoBehaviour
{
    public bool bombActivated = false;
    public Sprite explosionSprite;
    public float bombCountdown=1.5f;
    public float explosionSize=5;
    public float explosionLifetime;
    public int explosionDamage=2;
    SpriteRenderer sr;
    CircleCollider2D radius;
    bool obstacleDestroyed = false;
    float spawncountdown = 2;
    Sprite bombSprite;
    CircleCollider2D bombCollider;
    bool kaboom = false;
    Animator thingThatCreatesKaboom;
    string nameOfClip;
    AnimatorClipInfo[] currentClipInfo;
    float currentClipLength;
    float totalCountdown;
    Vector2 bombPosition;
    float normalGravity;

    // Start is called before the first frame update
    void Start()
    {
        thingThatCreatesKaboom = gameObject.GetComponent<Animator>();
        currentClipInfo = this.thingThatCreatesKaboom.GetCurrentAnimatorClipInfo(0);
        currentClipLength = currentClipInfo[0].clip.length;
        nameOfClip = currentClipInfo[0].clip.name;
        explosionLifetime = currentClipLength-0.2f;
        gameObject.GetComponent<Animator>().enabled = false;
        sr = GetComponent<SpriteRenderer>();
        bombSprite = sr.sprite;
        bombCollider = gameObject.GetComponent<CircleCollider2D>();
        totalCountdown = bombCountdown + explosionLifetime + spawncountdown;
        radius = gameObject.AddComponent<CircleCollider2D>();
        radius.isTrigger = true;
        bombPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        normalGravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        radius.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bombActivated)
        {
            bombCountdown -= Time.deltaTime;
            totalCountdown -= Time.deltaTime;
        }

        if (bombCountdown <= 0 && !kaboom)
        {
            bombCollider.enabled = false;
            //radius.radius = explosionSize;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            //sr.sprite = explosionSprite;
            //sr.drawMode = SpriteDrawMode.Sliced;
            //sr.size = new Vector2(explosionSize * 2, explosionSize * 2);
            radius.enabled = true;
            kaboom = true;
            gameObject.GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<AudioSource>().Play();
        }

        if (bombCountdown <= -explosionLifetime)
        {
            if (gameObject.name == "Puzzle bomb")
            {
                if (obstacleDestroyed)
                {
                    Destroy(gameObject);
                }

                else
                {
                    Debug.Log("obstacle not destroyed!");
                    explosionDamage = 0;
                    gameObject.GetComponent<Animator>().enabled = false;
                    radius.enabled = false;
                    sr.enabled = false;
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                }
            }
            else
            {
                Destroy(gameObject);
            }

        }

        if (totalCountdown <= 0)
        {
            sr.enabled = true;
            bombCollider.enabled = true;
            Debug.Log("spawning new bomb");
            bombActivated = false;
            transform.position = bombPosition; ;
            //sr.drawMode = SpriteDrawMode.Simple;
            //sr.sprite = bombSprite;
            sr.color = Color.white;
            spawncountdown = 2;
            kaboom = false;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            bombCountdown = 1.5f;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-1);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = normalGravity;
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
        if(bombActivated)
        {
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
                obstacleDestroyed = true;
                Destroy(obstacle);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.name == "Player")
        {
            ActivateBomb();
        }
    }
}
