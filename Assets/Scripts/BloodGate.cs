using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodGate : MonoBehaviour
{
    // Long note you can use multiple of these but make sure there is always
    // Enemies inside to not destroy the field i still need to figure out
    // How to disable the door script while i wait for enemies to be destroyed


    // Objects Using the blood gate tag need to be set as IsTrigger with a Box2DCollider

    // These are the tags of your player and enemy objects
    public string playerTag = "Player";
    public string enemyTag = "Enemy";
    private bool isPlayerInside = false; // Is the player inside the area
    private bool isEnemyInside = false; // Is the enemy inside the area

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If both the player and the enemy have exited the area
        // Destroy BloodGate Game Object
        // This can cause some issues if neither player or enemy object spawn inside

        if (!isPlayerInside && !isEnemyInside)
        {
            Destroy(gameObject);
        }
    }

    // This might look cleaner but its not good practise im told
    // This function is called when another object enters the trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // If the object that entered the trigger has Player or Enemy Tag
        if (other.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Player has entered the area");
            isPlayerInside = true;
        }
        // If the object that entered the trigger has the enemy tag...
        else if (other.gameObject.CompareTag(enemyTag))
        {
            // Then the enemy has entered or spawned inside the area
            Debug.Log("Enemy Spawned");
            isEnemyInside = true;
        }
    }

    // This function is called when another object exits the trigger collider
    void OnTriggerExit2D(Collider2D other)
    {
        // If the object that exited the trigger has enemy or player tag
        if (other.gameObject.CompareTag(playerTag))
        {
            // The player has left the area
            Debug.Log("Player has left the area");
            isPlayerInside = false;
        }
        else if (other.gameObject.CompareTag(enemyTag))
        {
            // The enemy has left the area or been destroyed
            // How to use eyes to check? url("https://youtu.be/TY1giZgddAs?si=qYgqqjMv2mblTqiYhttps://youtu.be/TY1giZgddAs?si=qYgqqjMv2mblTqiY")
            Debug.Log("Enemy Possibly Destroyed??");
            isEnemyInside = false;
        }
    }
}