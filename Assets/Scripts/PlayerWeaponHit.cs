using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWeaponHit : MonoBehaviour
{
    // how long does the attack exist, how much damage does it do, create collider
    public float lifeTime;
    public float damage;
    public BoxCollider2D attackHit;
    public bool disappear;
    public float invincibilityFrames;
    private bool hitting;
    private Enemy target;
    private bool continualDamage = false;



    // Start is called before the first frame update
    void Start()
    {
        //immediately define hitbox
        attackHit = gameObject.AddComponent<BoxCollider2D>();
        attackHit.size = new Vector2(1, 1);
        attackHit.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        //if the attack goes a certain amount of time without hitting anything, it despawns
        if (lifeTime <= 0f)
        {
            hitting = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Enemy e = collision.GetComponent<Enemy>();
        if (e != null)
        {
            //if the attack hits an enemy, it despawns
            if (disappear == true)
            {
                e.health -= damage;
                Debug.Log(e.health);
                Debug.Log("destroy attack");
                Destroy(gameObject);
            }
            else
            {
                //if the attack has the other type, it only despawns when lifeTime runs out
                e.health -= damage;
                target = e;
                Debug.Log(e.health);
                Debug.Log("attack continues");
                hitting = true;

                if (continualDamage == false)
                {
                    continualDamage = true;
                    StartCoroutine(repeatedDamage());
                }

            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hitting = false;
        continualDamage = false;
    }

    IEnumerator repeatedDamage()
    {

        if (hitting == true)
        {
            target.health -= damage;
            Debug.Log(target.health);
            yield return new WaitForSecondsRealtime(invincibilityFrames);
            StartCoroutine(repeatedDamage());
            yield break;
        }
        else
        {
            continualDamage = false;
            yield break;
        }

    }
}