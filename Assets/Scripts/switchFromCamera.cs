using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class switchFromCamera : MonoBehaviour
{
    public bool finishedBossfight = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if(CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera.Name == "CM vcam1" && finishedBossfight)
            {
                GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Priority = 10;
            }
            GameObject.Find("CM vcam2").GetComponent<CinemachineVirtualCamera>().Priority = 10;
            GameObject.Find("MainCamera").GetComponent<UILock>().bossfight = false;
        }
    }
}
