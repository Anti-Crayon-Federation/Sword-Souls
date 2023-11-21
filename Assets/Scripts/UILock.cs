using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UILock : MonoBehaviour
{
    public bool bossfight=false;
    GameObject upperLeftCorner;
    GameObject lowerRightCorner;
    GameObject middle;
    float topY;
    float leftX;
    float bottomY;
    float rightX;
    float middleX;
    float middleY;
    float size1=1;
    float size2= 1.571429f; //kunne sikkert forkortes, men ville v�re sikker p� at skaleringen passede til kamerast�rrelserne (7 -> 11)
    Vector2 size1Vector;
    Vector2 size2Vector;
    float transitionTime=4;
    bool started=false;
    int healthAndBullets;

    // Start is called before the first frame update
    void Start()
    {
        upperLeftCorner = transform.Find("UpperLeftCorner").gameObject;
        lowerRightCorner = transform.Find("LowerRightCorner").gameObject;
        middle = transform.Find("Middle").gameObject;
        size1Vector = new Vector2(size1, size1);
        size2Vector = new Vector2(size2, size2);
        topY = 1.046f;
        leftX = 0.53f;
        bottomY = 0.035f;
        rightX = 0.962f;
        middleX = 0.5f;
        middleY = 0.5f;
        healthAndBullets = upperLeftCorner.transform.GetChild(0).childCount;
    }

    // Update is called once per frame
    void Update()
    {
        size1Vector = new Vector2(size1, size1);
        size2Vector = new Vector2(size2, size2);
        Camera camera = GetComponent<Camera>();

        Vector2 topLeft = camera.ViewportToWorldPoint(new Vector3(leftX, topY));
        Vector2 bottomRight = camera.ViewportToWorldPoint(new Vector3(rightX, bottomY,gameObject.transform.position.z+0.5f));
        Vector2 middlePoint = camera.ViewportToWorldPoint(new Vector3(middleX, middleY));

        if(!started)
        {
            Debug.Log("set things");
            lowerRightCorner.transform.position = new Vector3(bottomRight.x,bottomRight.y,gameObject.transform.position.z+1);
            middle.transform.GetChild(0).GetChild(0).position = new Vector3(middlePoint.x, middlePoint.y, gameObject.transform.position.z + 0.5f);
            middle.transform.GetChild(0).GetChild(1).position = new Vector3(middlePoint.x, middlePoint.y, gameObject.transform.position.z + 0.6f);

            for (int i = 0; i < healthAndBullets; ++i)
            {
                upperLeftCorner.transform.GetChild(0).GetChild(i).position = new Vector3(upperLeftCorner.transform.GetChild(0).GetChild(i).position.x, upperLeftCorner.transform.GetChild(0).GetChild(i).position.y, upperLeftCorner.transform.position.z - 3);
            }
            upperLeftCorner.transform.GetChild(0).GetChild(0).position = new Vector3(upperLeftCorner.transform.GetChild(0).GetChild(0).position.x, upperLeftCorner.transform.GetChild(0).GetChild(0).position.y, +2);

            started = true;
        }

        if (!bossfight)
        {
            upperLeftCorner.transform.localScale = size1Vector;
            lowerRightCorner.transform.localScale = size1Vector;
            middle.transform.localScale = size1Vector;
            topY = 1.046f;
            leftX = 0.53f;
            bottomY = 0.035f;
            rightX = 0.962f;
            middleX = 0.5f;
            middleY = 0.5f;

            topLeft = camera.ViewportToWorldPoint(new Vector2(leftX, topY));
            bottomRight = camera.ViewportToWorldPoint(new Vector2(rightX, bottomY));
            middle.transform.GetChild(0).GetChild(0).position = new Vector3(middlePoint.x,middlePoint.y, +0.5f);
            middle.transform.GetChild(0).GetChild(1).position = new Vector3(middlePoint.x, middlePoint.y, +0.6f);

            upperLeftCorner.transform.position = new Vector3(topLeft.x, topLeft.y, upperLeftCorner.transform.position.z);
            lowerRightCorner.transform.GetChild(0).position = bottomRight;
            lowerRightCorner.transform.GetChild(1).position = bottomRight;
            lowerRightCorner.transform.GetChild(2).position = bottomRight;
            for (int i = 0; i < healthAndBullets; ++i)
            {
                upperLeftCorner.transform.GetChild(0).GetChild(i).position = new Vector3(upperLeftCorner.transform.GetChild(0).GetChild(i).position.x, upperLeftCorner.transform.GetChild(0).GetChild(i).position.y, upperLeftCorner.transform.position.z - 3);
            }
            upperLeftCorner.transform.GetChild(0).GetChild(0).position = new Vector3(upperLeftCorner.transform.GetChild(0).GetChild(0).position.x, upperLeftCorner.transform.GetChild(0).GetChild(0).position.y, +2);
        }
   
        else if(bossfight)
        {
            upperLeftCorner.transform.localScale = size2Vector;
            lowerRightCorner.transform.localScale = size2Vector;
            middle.transform.localScale = size2Vector;
            topY = 1.048f;
            leftX = 0.529f;
            bottomY = 0.034f;
            rightX = 0.962f;
            middleX = 0.5f;
            middleY = 0.5f;

            topLeft = camera.ViewportToWorldPoint(new Vector2(leftX, topY));
            bottomRight = camera.ViewportToWorldPoint(new Vector2(rightX, bottomY));
            middlePoint = camera.ViewportToWorldPoint(new Vector3(middleX, middleY,-5));

            upperLeftCorner.transform.position = new Vector3(topLeft.x, topLeft.y, upperLeftCorner.transform.position.z);
            lowerRightCorner.transform.GetChild(0).GetChild(0).position = Vector2.MoveTowards(lowerRightCorner.transform.position, bottomRight, transitionTime * Time.deltaTime);
            lowerRightCorner.transform.GetChild(0).GetChild(1).position = Vector2.MoveTowards(lowerRightCorner.transform.position, bottomRight, transitionTime * Time.deltaTime);
            lowerRightCorner.transform.GetChild(0).GetChild(2).position = Vector2.MoveTowards(lowerRightCorner.transform.position, bottomRight, transitionTime * Time.deltaTime);
            middle.transform.GetChild(0).GetChild(0).position = new Vector3(middlePoint.x, middlePoint.y,+0.5f);
            middle.transform.GetChild(0).GetChild(1).position = new Vector3(middlePoint.x, middlePoint.y, +0.6f);

            for (int i = 0; i < healthAndBullets; ++i)
            {
                upperLeftCorner.transform.GetChild(0).GetChild(i).position = new Vector3(upperLeftCorner.transform.GetChild(0).GetChild(i).position.x, upperLeftCorner.transform.GetChild(0).GetChild(i).position.y, upperLeftCorner.transform.position.z - 3);
            }
            upperLeftCorner.transform.GetChild(0).GetChild(0).position = new Vector3(upperLeftCorner.transform.GetChild(0).GetChild(0).position.x, upperLeftCorner.transform.GetChild(0).GetChild(0).position.y, +2);
        }


        
        //upperLeftCorner.transform.position =topLeft;
        //lowerRightCorner.transform.position = bottomRight;
        //middle.transform.position = middlePoint;
    }
}
