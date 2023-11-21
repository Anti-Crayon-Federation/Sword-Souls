using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    public GameObject gameOverScreen;
    public Vector2 lastCheckpoint;
    public float isInvincible = 0f;
    private SpriteRenderer sr;
    private float gameOverTime=0.5f;
    GameObject healthCounter;
    bool noise1;
    SoundManager sm;
    float timeBeforeMute;

    // Start is called before the first frame update
    void Start()
    {
        lastCheckpoint = transform.position;
        sr = GetComponent<SpriteRenderer>();
        healthCounter = GameObject.Find("Bullets&HealthTemplate").gameObject;
        sm = FindObjectOfType<SoundManager>();
        timeBeforeMute = sm.soundPlayerGameOver.clip.length-3.25f;
    }

    public void ResetPlayer()
    {
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Priority = 10;
        GameObject.Find("CM vcam2").GetComponent<CinemachineVirtualCamera>().Priority = 10;
        GameObject.Find("Main Camera").GetComponent<UILock>().bossfight = false;

        GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
        currentHealth = maxHealth;
        transform.position = lastCheckpoint;
        healthCounter.transform.Find("4hp").gameObject.SetActive(true);
        healthCounter.transform.Find("3hp").gameObject.SetActive(true);
        healthCounter.transform.Find("2hp").gameObject.SetActive(true);
        healthCounter.transform.Find("1hp").gameObject.SetActive(true);
        GetComponent<PlayerStateManager>().playerRigidbody.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (isInvincible > 0f)
        {
            isInvincible -= Time.deltaTime;
        }
        else
        {
            sr.color = new Color(1, 1, 1, 1); 
        }

        if (currentHealth <= 0)
        {
            Debug.Log("You died");
            gameOverScreen.SetActive(true);
            sm.PlaySound(sm.soundPlayerGameOver);
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
            GetComponent<PlayerStateManager>().playerRigidbody.velocity = Vector2.zero;
            gameOverTime -= Time.deltaTime;
            timeBeforeMute -= Time.deltaTime;
            if(timeBeforeMute<=0)
            {
                GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = false;
            }
            if((Input.anyKey||Input.GetAxisRaw("Horizontal")!=0||Input.GetAxisRaw("Vertical")!=0||Input.GetButton("Jump")||Input.GetButton("Dash")) && gameOverTime<=0)
            {
                gameOverScreen.SetActive(false);
                ResetPlayer();
                gameOverTime = 0.5f;
            }

            //Possible death animation
            //Fail state
        }
    }

    public void TakeDamange(int amount)
    {
        if (isInvincible <= 0f)
        {
            currentHealth -= amount;
            isInvincible = 0.8f;
            sr.color = new Color(1, 1, 1, 0.35f);
            healthCounter.transform.Find("4hp").gameObject.SetActive(false);
            if(currentHealth<3)
            {
                healthCounter.transform.Find("3hp").gameObject.SetActive(false);
                if (currentHealth < 2)
                {
                    healthCounter.transform.Find("2hp").gameObject.SetActive(false);
                    if (currentHealth==0)
                    {
                        healthCounter.transform.Find("1hp").gameObject.SetActive(false);
                    }
                }
            }

            if(currentHealth>0)
            {
                if(noise1)
                {
                    sm.PlaySound(sm.soundPlayerDamage1);
                    noise1 = false;
                }
                else
                {
                    sm.PlaySound(sm.soundPlayerDamage2);
                    noise1 = true;
                }
            }
        }
    }
    
    void ContactDamage(Collision2D collision)
    {
        Enemy e = collision.gameObject.GetComponent<Enemy>();
        if (e != null)
        {
            switch (e.type)
            {
                case EnemyType.Standard:
                    //Debug.Log("Hit by standard enemy touch");
                    TakeDamange(1);
                    break;
                case EnemyType.Bomb:
                    Debug.Log("Hit by bomb touch");
                    break;
                case EnemyType.Boss:
                    Debug.Log("Hit by boss touch");
                    break;
                default:
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactDamage(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ContactDamage(collision);
    }

}
