using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    // Lifetime of the EnemyBullet
    private float shelflife;
    private float totalLifeTime = 5f;

    [HideInInspector] public float force;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Gets The player position relative to the CannonBall or Bullet
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        // Rotates the CannonBall or Bullet if needed
        // (PS Please Just Make Sprites In One Direction)
        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 180f);
    }

    // Update is called once per frame
    void Update()
    {

        // Destroys The Game Object if enough time has passed
        shelflife += Time.deltaTime;

        if (shelflife > totalLifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamange(1);
            Destroy(gameObject);
        }
    }
}
