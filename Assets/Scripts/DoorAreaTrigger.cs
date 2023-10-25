using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAreaTrigger : MonoBehaviour
{
    [SerializeField] Door doorToOpen;
    public string PlayerTag = "Player";
    public bool isPlayerInside = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.gameObject.CompareTag(PlayerTag))
        {
            Debug.Log("Door open");
            isPlayerInside = true;

            if (isPlayerInside)
            {
                doorToOpen._isDoorOpen = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D _collider)
    {
        if (_collider.gameObject.CompareTag(PlayerTag))
        {
            isPlayerInside = false;
            doorToOpen._isDoorOpen = false;
        }
    }
}