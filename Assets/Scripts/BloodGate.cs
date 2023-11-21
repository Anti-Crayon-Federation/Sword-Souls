using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodGate : MonoBehaviour
{
    // Long note you can use multiple of these but make sure there is always
    // Enemies inside to not destroy the field i still need to figure out
    // How to disable the door script while i wait for enemies to be destroyed


    // Objects Using the blood gate tag need to be set as IsTrigger with a Box2DCollider

    // These are the tags of your enemy objects
    public string enemyTag = "Enemy";
    private bool isEnemyInside = true; // Is the enemy inside the area
    public GameObject doorThatWillOpen;

    // Start is called before the first frame update
    void Start()
    {
        doorThatWillOpen = gameObject.transform.GetChild(0).gameObject;
        doorThatWillOpen.GetComponent<DoorBehaviour>().enabled = false;
        doorThatWillOpen.transform.Find("LightGreen").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LightRed");
    }

    // Update is called once per frame
    void Update()
    {
        // If all enemies have exited the area
        // Destroy BloodGate Game Object
        // This can cause some issues if no enemy objects spawn inside

        if (!isEnemyInside)
        {
            doorThatWillOpen.GetComponent<DoorBehaviour>().enabled = true;
            doorThatWillOpen.transform.Find("LightGreen").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LightGreen");
            gameObject.GetComponent<BloodGate>().enabled = false;
        }
    }

    // This might look cleaner but its not good practise im told
    // This function is called when another object enters the trigger collider
    void OnTriggerStay2D(Collider2D other)
    {
        // If the object that entered the trigger has the enemy tag...
        if (other.gameObject.CompareTag(enemyTag))
        {
            // Then the enemy has entered or spawned inside the area
            isEnemyInside = true;
        }
    }

    // This function is called when another object exits the trigger collider
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            // The enemy has left the area or been destroyed
            // How to use eyes to check? url("https://youtu.be/TY1giZgddAs?si=qYgqqjMv2mblTqiYhttps://youtu.be/TY1giZgddAs?si=qYgqqjMv2mblTqiY")
            isEnemyInside = false;
        }
    }
}