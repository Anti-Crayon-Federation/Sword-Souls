using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoorTriggerButton : MonoBehaviour
{
    [SerializeField] DoorBehaviour _doorBehaviour;
    public string PlayerTag = "Player";
    private bool isPlayerInside = false;

    void OnTriggerEnter2D (Collider2D DoorTriggerButton)
    {
        if (DoorTriggerButton.gameObject.CompareTag(PlayerTag))
        {
            Debug.Log("Door open");
            isPlayerInside = true;

            if (isPlayerInside = true)
            {
                _doorBehaviour._isDoorOpen = true;
            }
        }
    }

    void OnTriggerExit2D (Collider2D DoorTriggerButton)
    {
        if (DoorTriggerButton.gameObject.CompareTag(PlayerTag))
        {
            
            isPlayerInside = false;

         
                _doorBehaviour._isDoorOpen = false;
                Debug.Log("Door close");
            
        }
    }


    // Checks if Player is inside collider
    public bool isPLayerinside()
    {
        return isPlayerInside;
    }

}
