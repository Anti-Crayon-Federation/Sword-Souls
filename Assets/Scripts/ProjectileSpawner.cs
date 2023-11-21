using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    SoundManager sm;

    [SerializeField]
    private float maxDetectionDistance = 10f;
    // The CircleCollider2D that represents the detection radius
    private BoxCollider2D detectionArea;
    public float bulletSpeed = 1f;
    public float spawnInterval = 1f;
    private float timer;
    private GameObject player;
    private Enemy e;
    // Start is called before the first frame update
    void Start()
    {
        sm = FindObjectOfType<SoundManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        detectionArea = GetComponentInChildren<BoxCollider2D>();
        e = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //float distance = Vector2.Distance(transform.position, player.transform.position);

        if (e.currentState == EnemyState.Attack1)
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
        sm.PlaySound(sm.soundClamCannonFire);
        GameObject go = Instantiate(bullet, bulletPos.position, Quaternion.identity);
        go.GetComponent<EnemyProjectile>().force = bulletSpeed;
    }
}
