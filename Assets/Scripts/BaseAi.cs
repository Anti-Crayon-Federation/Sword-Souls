using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAi : MonoBehaviour
{
    public float speed = 2.0f;
    //public PlayerDetection playerDetection; 
    private Transform player; 

    // Start is called before the first frame update
    void Start()
    {
        // Find the player object by its tag and get its Transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is inside the detection area...
        //if (playerDetection.IsPlayerInside())
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
}