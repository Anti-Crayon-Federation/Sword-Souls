using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public bool _isDoorOpen = false;
    Vector3 _doorClosedPos;
    Vector3 _doorOpenedPos;
    float _doorSpeed = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        _doorClosedPos = transform.position;
        _doorOpenedPos = new Vector3(transform.position.x,
            transform.position.y + 3.5f,
            transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDoorOpen)
        {
            OpenDoor();
        }

        else if (!_isDoorOpen)
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        if(transform.position != _doorOpenedPos) 
        {  
        transform.position = Vector3.MoveTowards
                (transform.position, _doorOpenedPos, _doorSpeed * Time.deltaTime);
        }
    }


    void CloseDoor()
    {
        if (transform.position != _doorClosedPos)
        {
            transform.position = Vector3.MoveTowards
                    (transform.position, _doorClosedPos, _doorSpeed * Time.deltaTime);
            Debug.Log("Moving down");

        }

    }
}
