using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public bool _isDoorOpen = false;
    Vector3 _doorClosedPos;
    Vector3 _doorOpenedPos;
    float _doorSpeed = 10f;
    GameObject actualDoor;

    // Start is called before the first frame update
    void Start()
    {
        actualDoor = transform.Find("DoorSlider-1x4").gameObject;
        _doorClosedPos = actualDoor.transform.position;
        _doorOpenedPos = new Vector3(actualDoor.transform.position.x,
            actualDoor.transform.position.y + 3.5f,
            actualDoor.transform.position.z);
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
        if(actualDoor.transform.position != _doorOpenedPos) 
        {  
        actualDoor.transform.position = Vector3.MoveTowards
                (actualDoor.transform.position, _doorOpenedPos, _doorSpeed * Time.deltaTime);
        }
    }


    void CloseDoor()
    {
        if (actualDoor.transform.position != _doorClosedPos)
        {
            actualDoor.transform.position = Vector3.MoveTowards
                    (actualDoor.transform.position, _doorClosedPos, _doorSpeed * Time.deltaTime);
            Debug.Log("Moving down");

        }

    }
}
