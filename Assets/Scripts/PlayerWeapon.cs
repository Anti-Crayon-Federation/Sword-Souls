using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float meleeCooldown = 0f;
    public float rangeCooldown = 0f;
    public Sprite attackSprite;
    bool usingRangedWeapon = false;
    public Sprite attackSprite2;
    public Sprite attackSprite3;
    public float weapon2invincibilityFrames;
    private PlayerStateManager stateManager;
    private string attackName = "projectileHitbox";
    GameObject bulletCounter;
    public int maxBullets = 4;
    public int bulletsLeft=4;
    SpriteRenderer uiWeaponGun;
    SpriteRenderer uiWeaponSword;
    SoundManager sm;

    private void Start()
    {
        stateManager = GetComponent<PlayerStateManager>();
         bulletCounter = GameObject.Find("Bullets&HealthTemplate");
        uiWeaponGun = GameObject.Find("pistolui").GetComponent<SpriteRenderer>();
        uiWeaponSword = GameObject.Find("swordui").GetComponent<SpriteRenderer>();
        sm = FindObjectOfType<SoundManager>();
        usingRangedWeapon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rangeCooldown > 0f)
        {
            rangeCooldown -= Time.deltaTime;
        }

        if (meleeCooldown > 0f)
        {
            meleeCooldown -= Time.deltaTime;
        }

        if (Input.GetButtonDown("ToggleWeapon"))
        {
            usingRangedWeapon = !usingRangedWeapon;
            if (usingRangedWeapon)
            {
                attackName = "projectileHitbox";
                uiWeaponSword.enabled = false;
                uiWeaponGun.enabled=true;
                sm.PlaySound(sm.soundPlayerSwitchToRanged);
            }
            else
            {
                attackName = "meleeHitbox";
                uiWeaponGun.enabled=false;
                uiWeaponSword.enabled=true;
                sm.PlaySound(sm.soundPlayerSwitchToMelee);
            }
        }

        if (Input.GetButtonDown("Attack"))// || Input.GetButtonDown("Fire1"))
        {
            GameObject attack = new GameObject(attackName);
            PlayerAttack hitbox = attack.AddComponent<PlayerAttack>();
            hitbox.player = stateManager;
            SpriteRenderer sr = attack.AddComponent<SpriteRenderer>();
            attack.transform.SetParent(transform);
            hitbox.gameObject.layer = 2;
            float directionMultiplier = GetComponent<CharacterMovement>().lastXDirection;
            float directionY = 0f;

            if (stateManager.playerInputDirection.y != 0)
            {
                //directionMultiplier = stateManager.playerInputDirection.x;
                directionY = stateManager.playerInputDirection.y;
            }

            attack.transform.localPosition = new Vector3(directionMultiplier, directionY);

            if (directionMultiplier > 0f)
            {
                sr.flipX = true;
            }

            if (directionY != 0f)
            {
                //sr.flipY = true;
            }

            if (usingRangedWeapon)
            {
                if (rangeCooldown <= 0f && bulletsLeft > 0)
                {
                    Animator a = attack.AddComponent<Animator>();
                    rangeCooldown = 2f;
                    attack.transform.localPosition = new Vector3(attack.transform.localPosition.x, attack.transform.localPosition.y + 0.75f);
                    attack.transform.parent = null;
                    hitbox.projectileSpeed = 15f * directionMultiplier;
                    hitbox.direction = stateManager.playerRBDirection.normalized;
                    hitbox.damage = 2;
                    hitbox.lifeTime = 2;
                    hitbox.disappear = true;
                    sr.sprite = attackSprite;
                    //sr.color = Color.red;
                    //sr.size = new Vector2(0.5f, 0.2f);
                    bulletsLeft -= 1;
                    bulletCounter.transform.Find("4shots").gameObject.SetActive(false);
                    if(bulletsLeft<3)
                    {
                        bulletCounter.transform.Find("3shots").gameObject.SetActive(false);
                        if (bulletsLeft<2)
                        {
                            bulletCounter.transform.Find("2shots").gameObject.SetActive(false);
                            if (bulletsLeft==0)
                            {
                                bulletCounter.transform.Find("1shots").gameObject.SetActive(false);
                            }
                            
                        }
                    }
                }
                else
                {
                    //Evt. feedback til at cooldown ikke er done
                }
                
            }
            else
            {
                if (meleeCooldown <= 0f)
                {
                    stateManager.sm.PlaySound(stateManager.sm.soundPlayerDash, 0.25f);

                    meleeCooldown = 0.3f;

                    hitbox.damage = 4;
                    hitbox.lifeTime = 0.15f;
                    hitbox.projectileSpeed = 0f;
                    hitbox.disappear = true;
                    hitbox.invincibilityFrames = weapon2invincibilityFrames;
                    //sr.color = Color.red;
                    sr.sprite = attackSprite2;
                    sr.size = new Vector2(0.5f, 1);
                    sr.transform.position = new Vector3(sr.transform.position.x, sr.transform.position.y, -3f);
                    if (directionY != 0f)
                    {
                        //sr.flipY = true;
                        sr.sprite = attackSprite3;
                    }
                }
                else
                {
                    //Evt. feedback til at cooldown ikke er done
                }
            }
        }
    }
}