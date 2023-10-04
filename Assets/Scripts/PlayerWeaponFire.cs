using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWeaponFire : MonoBehaviour
{

    public float cooldown = 2;
    public Sprite attackSprite;
    bool usingFirstWeapon;
    public Sprite attackSprite2;
    public float weapon2invincibilityFrames;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1") == true)
        {
            Debug.Log("using weapon gun");
            usingFirstWeapon = true;
        }
        else if (Input.GetKey("2") == true)
        {
            Debug.Log("using weapon melee");
            usingFirstWeapon = false;
        }

        if (Input.GetKeyDown("z") == true)
        {
            Debug.Log("weapon was fired");
            GameObject attack = new GameObject("attack");

            if (usingFirstWeapon)
            {
                attack.transform.position = new Vector2(transform.position.x - 5, transform.position.y);
                playerWeaponHit hitbox = attack.AddComponent<playerWeaponHit>();
                hitbox.damage = 2;
                hitbox.lifeTime = 2;
                hitbox.disappear = true;
                attack.AddComponent<SpriteRenderer>();
                attack.GetComponent<SpriteRenderer>().color = Color.red;
                attack.GetComponent<SpriteRenderer>().sprite = attackSprite;

                attack.GetComponent<SpriteRenderer>().size = new Vector2(0.5f, 0.2f);
            }
            else
            {
                attack.transform.position = new Vector2(transform.position.x - 3.5f, transform.position.y);
                playerWeaponHit hitbox = attack.AddComponent<playerWeaponHit>();
                hitbox.damage = 4;
                hitbox.lifeTime = 2;
                hitbox.disappear = false;
                hitbox.invincibilityFrames = weapon2invincibilityFrames;
                attack.AddComponent<SpriteRenderer>();
                attack.GetComponent<SpriteRenderer>().color = Color.red;
                attack.GetComponent<SpriteRenderer>().sprite = attackSprite2;

                attack.GetComponent<SpriteRenderer>().size = new Vector2(0.5f, 1);
            }


        }

    }
}