using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float cooldown = 2;
    public Sprite attackSprite;
    bool usingFirstWeapon = true;
    public Sprite attackSprite2;
    public float weapon2invincibilityFrames;
    private PlayerStateManager stateManager;
    private string attackName = "projectileHitbox";

    private void Start()
    {
        stateManager = GetComponent<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1") == true)
        {
            Debug.Log("using weapon gun");
            attackName = "projectileHitbox";
            usingFirstWeapon = true;
        }
        else if (Input.GetKey("2") == true)
        {
            Debug.Log("using weapon melee");
            attackName = "meleeHitbox";
            usingFirstWeapon = false;
        }

        if (Input.GetKeyDown("z") == true)
        {
            GameObject attack = new GameObject(attackName);
            PlayerAttack hitbox = attack.AddComponent<PlayerAttack>();
            SpriteRenderer sr = attack.AddComponent<SpriteRenderer>();
            attack.transform.SetParent(transform);

            attack.transform.localPosition = stateManager.playerInputDirection;
            if (usingFirstWeapon)
            {
                attack.transform.parent = null;
                hitbox.projectileSpeed = 3f;
                hitbox.direction = stateManager.playerRBDirection.normalized;
                hitbox.damage = 2;
                hitbox.lifeTime = 2;
                hitbox.disappear = true;
                sr.color = Color.red;
                sr.sprite = attackSprite;
                sr.size = new Vector2(0.5f, 0.2f);
            }
            else
            {
                hitbox.damage = 4;
                hitbox.lifeTime = 2;
                hitbox.projectileSpeed = 0f;
                hitbox.disappear = false;
                hitbox.invincibilityFrames = weapon2invincibilityFrames;
                sr.color = Color.red;
                sr.sprite = attackSprite2;
                sr.size = new Vector2(0.5f, 1);
            }
        }
    }
}