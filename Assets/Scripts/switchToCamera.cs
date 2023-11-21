using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class switchToCamera : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="Player")
        {
            if(gameObject.name=="camera trigger bossfight")
            {
                gameObject.transform.Find("CM vcam1").gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 12;
                GameObject.Find("MainCamera").GetComponent<UILock>().bossfight = true;
                GameObject.Find("MainCamera").GetComponent<changeMusic>().bossfight = true;
            }
            else
            {
                gameObject.transform.Find("CM vcam2").gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 12;
            }
        }
    }
}
