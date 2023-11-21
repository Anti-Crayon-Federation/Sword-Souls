using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSystem : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    [SerializeField]
    private float maxDetectionDistance = 10f;
    // The CircleCollider2D that represents the detection radius
    private CircleCollider2D detectionRadius;
    public float bulletSpeed = 1f;
    public float spawnInterval = 1f;
    private float timer;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        detectionRadius = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        // Set the radius of the CircleCollider2D to the distance value
        detectionRadius.radius = Mathf.Min(distance, maxDetectionDistance);

        if (distance < maxDetectionDistance)
        {
            timer += Time.deltaTime;
            if (timer > spawnInterval)
            {
                timer = 0;
                shoot();
            }
        }
    }

    void shoot()
    {
        GameObject go = Instantiate(bullet, bulletPos.position, Quaternion.identity);
        go.GetComponent<EnemyBullet>().force = bulletSpeed;
    }
}
