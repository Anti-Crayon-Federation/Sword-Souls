using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerButton : MonoBehaviour
{
    private string PlayerTag = "Player";
    public GameObject doorThatWillOpen;


    private void Start()
    {
        doorThatWillOpen = transform.parent.gameObject;
    }

    void OnTriggerStay2D (Collider2D DoorTriggerButton)
    {
        if (DoorTriggerButton.gameObject.CompareTag(PlayerTag))
        {
            Debug.Log("Door open");
            doorThatWillOpen.GetComponent<DoorBehaviour>()._isDoorOpen = true;

        }
    }

    void OnTriggerExit2D (Collider2D DoorTriggerButton)
    {
        if (DoorTriggerButton.gameObject.CompareTag(PlayerTag))
        {
            doorThatWillOpen.GetComponent<DoorBehaviour>()._isDoorOpen = false;
            Debug.Log("Door close");
            
        }
    }
}
