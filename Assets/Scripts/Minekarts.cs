using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minekarts : MonoBehaviour
{
    public float movementSpeed=20f;
    public float spawnRate=2f;
    float spawnCountdown;
    Transform startPosition;
    Transform endPosition;
    GameObject minekartList;
    GameObject karts;
    GameObject karts2;
    GameObject karts3;
    GameObject karts4;
    GameObject karts5;
    Vector3 begin;
    Vector3 end;
    bool despawning = false;

    // Start is called before the first frame update
    void Start()
    {
        karts = gameObject.transform.Find("Karts").gameObject;
        karts2= gameObject.transform.Find("Karts (1)").gameObject;
        karts3 = gameObject.transform.Find("Karts (2)").gameObject;
        karts4= gameObject.transform.Find("Karts (3)").gameObject;
        karts5= gameObject.transform.Find("Karts (4)").gameObject;
        startPosition = gameObject.transform.Find("Start");
        endPosition = gameObject.transform.Find("End");
        begin = startPosition.position;
        end = endPosition.position;
        karts.transform.position = begin;
        karts.SetActive(false);
        karts2.SetActive(false);
        karts3.SetActive(false);
        karts4.SetActive(false);
        karts5.SetActive(false);
        spawnCountdown = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        //basically, this scrit edits colliders & child objects inside the Kart parent (to avoid the player hitting multiple colliders at the same time)
        spawnCountdown -= Time.deltaTime;
        
        if (spawnCountdown <= 0)
        {
            if (!karts.activeInHierarchy)
            {
                karts.SetActive(true);
                karts.GetComponent<BoxCollider2D>().offset = new Vector2(-4,0);
                karts.GetComponent<BoxCollider2D>().size = new Vector2(2, 1);
                karts.transform.GetChild(0).gameObject.SetActive(true);
                karts.transform.GetChild(1).gameObject.SetActive(false);
                karts.transform.GetChild(2).gameObject.SetActive(false);
                karts.transform.GetChild(3).gameObject.SetActive(false);
                karts.transform.GetChild(4).gameObject.SetActive(false);
                karts.transform.position = new Vector2(begin.x+4,begin.y);
                spawnCountdown = spawnRate;
            }
            else if (!karts2.activeInHierarchy)
            {
                karts2.SetActive(true);
                karts2.GetComponent<BoxCollider2D>().offset = new Vector2(-4, 0);
                karts2.GetComponent<BoxCollider2D>().size = new Vector2(2, 1);
                karts2.transform.GetChild(0).gameObject.SetActive(true);
                karts2.transform.GetChild(1).gameObject.SetActive(false);
                karts2.transform.GetChild(2).gameObject.SetActive(false);
                karts2.transform.GetChild(3).gameObject.SetActive(false);
                karts2.transform.GetChild(4).gameObject.SetActive(false);
                karts2.transform.position = new Vector2(begin.x + 4, begin.y);
                spawnCountdown = spawnRate;
            }
            else if (!karts3.activeInHierarchy)
            {
                karts3.SetActive(true);
                karts3.GetComponent<BoxCollider2D>().offset = new Vector2(-4, 0);
                karts3.GetComponent<BoxCollider2D>().size = new Vector2(2, 1);
                karts3.transform.GetChild(0).gameObject.SetActive(true);
                karts3.transform.GetChild(1).gameObject.SetActive(false);
                karts3.transform.GetChild(2).gameObject.SetActive(false);
                karts3.transform.GetChild(3).gameObject.SetActive(false);
                karts3.transform.GetChild(4).gameObject.SetActive(false);
                karts3.transform.position = new Vector2(begin.x + 4, begin.y);
                spawnCountdown = spawnRate;
            }
            else if (!karts4.activeInHierarchy)
            {
                karts4.SetActive(true);
                karts4.GetComponent<BoxCollider2D>().offset = new Vector2(-4, 0);
                karts4.GetComponent<BoxCollider2D>().size = new Vector2(2, 1);
                karts4.transform.GetChild(0).gameObject.SetActive(true);
                karts4.transform.GetChild(1).gameObject.SetActive(false);
                karts4.transform.GetChild(2).gameObject.SetActive(false);
                karts4.transform.GetChild(3).gameObject.SetActive(false);
                karts4.transform.GetChild(4).gameObject.SetActive(false);
                karts4.transform.position = new Vector2(begin.x + 4, begin.y);
                spawnCountdown = spawnRate;
            }
            else if (!karts5.activeInHierarchy)
            {
                karts5.SetActive(true);
                karts5.GetComponent<BoxCollider2D>().offset = new Vector2(-4, 0);
                karts5.GetComponent<BoxCollider2D>().size = new Vector2(2, 1);
                karts5.transform.GetChild(0).gameObject.SetActive(true);
                karts5.transform.GetChild(1).gameObject.SetActive(false);
                karts5.transform.GetChild(2).gameObject.SetActive(false);
                karts5.transform.GetChild(3).gameObject.SetActive(false);
                karts5.transform.GetChild(4).gameObject.SetActive(false);
                karts5.transform.position = new Vector2(begin.x + 4, begin.y);
                spawnCountdown = spawnRate;
            }
            else
            {
                Debug.Log("too few minekarts!");
            }
        }   
        karts.transform.position = Vector3.MoveTowards(karts.transform.position, new Vector3(end.x-5,end.y,end.z), movementSpeed * Time.deltaTime);
        karts2.transform.position = Vector3.MoveTowards(karts2.transform.position, new Vector3(end.x - 5, end.y,end.z), movementSpeed * Time.deltaTime);
        karts3.transform.position = Vector3.MoveTowards(karts3.transform.position, new Vector3(end.x - 5, end.y,end.z), movementSpeed * Time.deltaTime);
        karts4.transform.position = Vector3.MoveTowards(karts4.transform.position, new Vector3(end.x - 5, end.y,end.z), movementSpeed * Time.deltaTime);
        karts5.transform.position = Vector3.MoveTowards(karts5.transform.position, new Vector3(end.x - 5, end.y, end.z), movementSpeed * Time.deltaTime);

        //spawning & despawning the first set of karts after initialization
        if (karts.activeSelf)
        {
            if (karts.transform.position.x <= begin.x + 2 && !karts.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                karts.GetComponent<BoxCollider2D>().offset = new Vector2(-3, 0);
                karts.GetComponent<BoxCollider2D>().size = new Vector2(4, 2);
                karts.transform.GetChild(1).gameObject.SetActive(true);
            }

            if (karts.transform.position.x <= begin.x && !karts.transform.GetChild(2).gameObject.activeInHierarchy)
            {
                karts.GetComponent<BoxCollider2D>().offset = new Vector2(-2, 0);
                karts.GetComponent<BoxCollider2D>().size = new Vector2(6, 2);
                karts.transform.GetChild(2).gameObject.SetActive(true);
            }

            if (karts.transform.position.x <= begin.x - 2 && !karts.transform.GetChild(3).gameObject.activeInHierarchy)
            {
                karts.GetComponent<BoxCollider2D>().offset = new Vector2(-1, 0);
                karts.GetComponent<BoxCollider2D>().size = new Vector2(8, 2);
                karts.transform.GetChild(3).gameObject.SetActive(true);
            }

            if (karts.transform.position.x <= begin.x - 4 && !karts.transform.GetChild(4).gameObject.activeInHierarchy)
            {
                karts.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                karts.GetComponent<BoxCollider2D>().size = new Vector2(10, 2);
                karts.transform.GetChild(4).gameObject.SetActive(true);
            }

            if (karts.transform.position.x <= end.x + 4 && karts.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                karts.GetComponent<BoxCollider2D>().offset = new Vector2(1, 0);
                karts.GetComponent<BoxCollider2D>().size = new Vector2(8, 2);
                karts.transform.GetChild(0).gameObject.SetActive(false);
                despawning = true;
            }

            if (karts.transform.position.x <= end.x + 2 && karts.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                karts.GetComponent<BoxCollider2D>().offset = new Vector2(2, 0);
                karts.GetComponent<BoxCollider2D>().size = new Vector2(6, 2);
                karts.transform.GetChild(1).gameObject.SetActive(false);
            }

            if (karts.transform.position.x <= end.x && karts.transform.GetChild(2).gameObject.activeInHierarchy)
            {
                karts.GetComponent<BoxCollider2D>().offset = new Vector2(3, 0);
                karts.GetComponent<BoxCollider2D>().size = new Vector2(4, 2);
                karts.transform.GetChild(2).gameObject.SetActive(false);
            }

            if (karts.transform.position.x <= end.x - 2 && karts.transform.GetChild(3).gameObject.activeInHierarchy)
            {
                karts.GetComponent<BoxCollider2D>().offset = new Vector2(4, 0);
                karts.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
                karts.transform.GetChild(3).gameObject.SetActive(false);
            }

            if (karts.transform.position.x <= end.x - 4 && karts.transform.GetChild(4).gameObject.activeInHierarchy)
            {
                karts.SetActive(false);
                despawning = false;
            }

        }

        //same with the second set
        if (karts2.activeSelf)
        {
            if (karts2.transform.position.x <= begin.x + 2 && !karts2.transform.GetChild(1).gameObject.activeSelf)
            {
                karts2.GetComponent<BoxCollider2D>().offset = new Vector2(-3, 0);
                karts2.GetComponent<BoxCollider2D>().size = new Vector2(4, 2);
                karts2.transform.GetChild(1).gameObject.SetActive(true);
            }

            if (karts2.transform.position.x <= begin.x && !karts2.transform.GetChild(2).gameObject.activeSelf)
            {
                karts2.GetComponent<BoxCollider2D>().offset = new Vector2(-2, 0);
                karts2.GetComponent<BoxCollider2D>().size = new Vector2(6, 2);
                karts2.transform.GetChild(2).gameObject.SetActive(true);
            }

            if (karts2.transform.position.x <= begin.x - 2 && !karts2.transform.GetChild(3).gameObject.activeSelf)
            {
                karts2.GetComponent<BoxCollider2D>().offset = new Vector2(-1, 0);
                karts2.GetComponent<BoxCollider2D>().size = new Vector2(8, 2);
                karts2.transform.GetChild(3).gameObject.SetActive(true);
            }

            if (karts2.transform.position.x <= begin.x - 4 && !karts2.transform.GetChild(4).gameObject.activeSelf)
            {
                karts2.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                karts2.GetComponent<BoxCollider2D>().size = new Vector2(10, 2);
                karts2.transform.GetChild(4).gameObject.SetActive(true);
            }

            if (karts2.transform.position.x <= end.x + 4 && karts2.transform.GetChild(0).gameObject.activeSelf)
            {
                karts2.GetComponent<BoxCollider2D>().offset = new Vector2(1, 0);
                karts2.GetComponent<BoxCollider2D>().size = new Vector2(8, 2);
                karts2.transform.GetChild(0).gameObject.SetActive(false);
                despawning = true;
            }

            if (karts2.transform.position.x <= end.x + 2 && karts2.transform.GetChild(1).gameObject.activeSelf)
            {
                karts2.GetComponent<BoxCollider2D>().offset = new Vector2(2, 0);
                karts2.GetComponent<BoxCollider2D>().size = new Vector2(6, 2);
                karts2.transform.GetChild(1).gameObject.SetActive(false);
            }

            if (karts2.transform.position.x <= end.x && karts2.transform.GetChild(2).gameObject.activeSelf)
            {
                karts2.GetComponent<BoxCollider2D>().offset = new Vector2(3, 0);
                karts2.GetComponent<BoxCollider2D>().size = new Vector2(4, 2);
                karts2.transform.GetChild(2).gameObject.SetActive(false);
            }

            if (karts2.transform.position.x <= end.x - 2 && karts2.transform.GetChild(3).gameObject.activeSelf)
            {
                karts2.GetComponent<BoxCollider2D>().offset = new Vector2(4, 0);
                karts2.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
                karts2.transform.GetChild(3).gameObject.SetActive(false);
            }

            if (karts2.transform.position.x <= end.x - 4 && karts2.transform.GetChild(4).gameObject.activeSelf)
            {
                karts2.SetActive(false);
                despawning = false;
            }
        }

        //and the third
        if (karts3.activeSelf)
        {
            if (karts3.transform.position.x <= begin.x + 2 && !karts3.transform.GetChild(1).gameObject.activeSelf)
            {
                karts3.GetComponent<BoxCollider2D>().offset = new Vector2(-3, 0);
                karts3.GetComponent<BoxCollider2D>().size = new Vector2(4, 2);
                karts3.transform.GetChild(1).gameObject.SetActive(true);
            }

            if (karts3.transform.position.x <= begin.x && !karts3.transform.GetChild(2).gameObject.activeSelf)
            {
                karts3.GetComponent<BoxCollider2D>().offset = new Vector2(-2, 0);
                karts3.GetComponent<BoxCollider2D>().size = new Vector2(6, 2);
                karts3.transform.GetChild(2).gameObject.SetActive(true);
            }

            if (karts3.transform.position.x <= begin.x - 2 && !karts3.transform.GetChild(3).gameObject.activeSelf)
            {
                karts3.GetComponent<BoxCollider2D>().offset = new Vector2(-1, 0);
                karts3.GetComponent<BoxCollider2D>().size = new Vector2(8,2);
                karts3.transform.GetChild(3).gameObject.SetActive(true);
            }

            if (karts3.transform.position.x <= begin.x - 4 && !karts3.transform.GetChild(4).gameObject.activeSelf)
            {
                karts3.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                karts3.GetComponent<BoxCollider2D>().size = new Vector2(10, 2);
                karts3.transform.GetChild(4).gameObject.SetActive(true);
            }

            if (karts3.transform.position.x <= end.x + 4 && karts3.transform.GetChild(0).gameObject.activeSelf)
            {
                karts3.GetComponent<BoxCollider2D>().offset = new Vector2(1, 0);
                karts3.GetComponent<BoxCollider2D>().size = new Vector2(8, 2);
                karts3.transform.GetChild(0).gameObject.SetActive(false);
                despawning = true;
            }

            if (karts3.transform.position.x <= end.x + 2 && karts3.transform.GetChild(1).gameObject.activeSelf)
            {
                karts3.GetComponent<BoxCollider2D>().offset = new Vector2(2, 0);
                karts3.GetComponent<BoxCollider2D>().size = new Vector2(6, 2);
                karts3.transform.GetChild(1).gameObject.SetActive(false);
            }

            if (karts3.transform.position.x <= end.x && karts3.transform.GetChild(2).gameObject.activeSelf)
            {
                karts3.GetComponent<BoxCollider2D>().offset = new Vector2(3, 0);
                karts3.GetComponent<BoxCollider2D>().size = new Vector2(4, 2);
                karts3.transform.GetChild(2).gameObject.SetActive(false);
            }

            if (karts3.transform.position.x <= end.x - 2 && karts3.transform.GetChild(3).gameObject.activeSelf)
            {
                karts3.GetComponent<BoxCollider2D>().offset = new Vector2(4, 0);
                karts3.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
                karts3.transform.GetChild(3).gameObject.SetActive(false);
            }

            if (karts3.transform.position.x <= end.x - 4 && karts3.transform.GetChild(4).gameObject.activeSelf)
            {
                karts3.SetActive(false);
                despawning = false;
            }
        }

        //and fourth
        if (karts4.activeSelf)
        { 
            if (karts4.transform.position.x <= begin.x + 2 && !karts4.transform.GetChild(1).gameObject.activeSelf)
            {
                karts4.GetComponent<BoxCollider2D>().offset = new Vector2(-3, 0);
                karts4.GetComponent<BoxCollider2D>().size = new Vector2(4, 2);
                karts4.transform.GetChild(1).gameObject.SetActive(true);
            }

            if (karts4.transform.position.x <= begin.x && !karts4.transform.GetChild(2).gameObject.activeSelf)
            {
                karts4.GetComponent<BoxCollider2D>().offset = new Vector2(-2, 0);
                karts4.GetComponent<BoxCollider2D>().size = new Vector2(6, 2);
                karts4.transform.GetChild(2).gameObject.SetActive(true);
            }

            if (karts4.transform.position.x <= begin.x - 2 && !karts4.transform.GetChild(3).gameObject.activeSelf)
            {
                karts4.GetComponent<BoxCollider2D>().offset = new Vector2(-1, 0);
                karts4.GetComponent<BoxCollider2D>().size = new Vector2(8, 2);
                karts4.transform.GetChild(3).gameObject.SetActive(true);
            }

            if (karts4.transform.position.x <= begin.x - 4 && !karts4.transform.GetChild(4).gameObject.activeSelf)
            {
                karts4.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                karts4.GetComponent<BoxCollider2D>().size = new Vector2(10, 2);
                karts4.transform.GetChild(4).gameObject.SetActive(true);
            }

            if (karts4.transform.position.x <= end.x + 4 && karts4.transform.GetChild(0).gameObject.activeSelf)
            {
                karts4.GetComponent<BoxCollider2D>().offset = new Vector2(1, 0);
                karts4.GetComponent<BoxCollider2D>().size = new Vector2(8, 2);
                karts4.transform.GetChild(0).gameObject.SetActive(false);
                despawning = true;
            }

            if (karts4.transform.position.x <= end.x + 2 && karts4.transform.GetChild(1).gameObject.activeSelf)
            {
                karts4.GetComponent<BoxCollider2D>().offset = new Vector2(2, 0);
                karts4.GetComponent<BoxCollider2D>().size = new Vector2(6, 2);
                karts4.transform.GetChild(1).gameObject.SetActive(false);
            }

            if (karts4.transform.position.x <= end.x && karts4.transform.GetChild(2).gameObject.activeSelf)
            {
                karts4.GetComponent<BoxCollider2D>().offset = new Vector2(3, 0);
                karts4.GetComponent<BoxCollider2D>().size = new Vector2(4, 2);
                karts4.transform.GetChild(2).gameObject.SetActive(false);
            }

            if (karts4.transform.position.x <= end.x - 2 && karts4.transform.GetChild(3).gameObject.activeSelf)
            {
                karts4.GetComponent<BoxCollider2D>().offset = new Vector2(4, 0);
                karts4.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
                karts4.transform.GetChild(3).gameObject.SetActive(false);
            }

            if (karts4.transform.position.x <= end.x - 4 && karts4.transform.GetChild(4).gameObject.activeSelf)
            {
                karts4.SetActive(false);
                despawning = false;
            }
        }

        //and fifth
        if (karts5.activeSelf)
        {
            if (karts5.transform.position.x <= begin.x + 2 && !karts5.transform.GetChild(1).gameObject.activeSelf)
            {
                karts5.GetComponent<BoxCollider2D>().offset = new Vector2(-3, 0);
                karts5.GetComponent<BoxCollider2D>().size = new Vector2(4, 2);
                karts5.transform.GetChild(1).gameObject.SetActive(true);
            }

            if (karts5.transform.position.x <= begin.x && !karts5.transform.GetChild(2).gameObject.activeSelf)
            {
                karts5.GetComponent<BoxCollider2D>().offset = new Vector2(-2, 0);
                karts5.GetComponent<BoxCollider2D>().size = new Vector2(6, 2);
                karts5.transform.GetChild(2).gameObject.SetActive(true);
            }

            if (karts5.transform.position.x <= begin.x - 2 && !karts5.transform.GetChild(3).gameObject.activeSelf)
            {
                karts5.GetComponent<BoxCollider2D>().offset = new Vector2(-1, 0);
                karts5.GetComponent<BoxCollider2D>().size = new Vector2(8, 2);
                karts5.transform.GetChild(3).gameObject.SetActive(true);
            }

            if (karts5.transform.position.x <= begin.x - 4 && !karts5.transform.GetChild(4).gameObject.activeSelf)
            {
                karts5.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                karts5.GetComponent<BoxCollider2D>().size = new Vector2(10, 2);
                karts5.transform.GetChild(4).gameObject.SetActive(true);
            }

            if (karts5.transform.position.x <= end.x + 4 && karts5.transform.GetChild(0).gameObject.activeSelf)
            {
                karts5.GetComponent<BoxCollider2D>().offset = new Vector2(1, 0);
                karts5.GetComponent<BoxCollider2D>().size = new Vector2(8, 2);
                karts5.transform.GetChild(0).gameObject.SetActive(false);
                despawning = true;
            }

            if (karts5.transform.position.x <= end.x + 2 && karts5.transform.GetChild(1).gameObject.activeSelf)
            {
                karts5.GetComponent<BoxCollider2D>().offset = new Vector2(2, 0);
                karts5.GetComponent<BoxCollider2D>().size = new Vector2(6, 2);
                karts5.transform.GetChild(1).gameObject.SetActive(false);
            }

            if (karts5.transform.position.x <= end.x && karts5.transform.GetChild(2).gameObject.activeSelf)
            {
                karts5.GetComponent<BoxCollider2D>().offset = new Vector2(3, 0);
                karts5.GetComponent<BoxCollider2D>().size = new Vector2(4, 2);
                karts5.transform.GetChild(2).gameObject.SetActive(false);
            }

            if (karts5.transform.position.x <= end.x - 2 && karts5.transform.GetChild(3).gameObject.activeSelf)
            {
                karts5.GetComponent<BoxCollider2D>().offset = new Vector2(4, 0);
                karts5.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
                karts5.transform.GetChild(3).gameObject.SetActive(false);
            }

            if (karts5.transform.position.x <= end.x - 4 && karts5.transform.GetChild(4).gameObject.activeSelf)
            {
                karts5.SetActive(false);
                despawning = false;
            }
        }
    }
}
