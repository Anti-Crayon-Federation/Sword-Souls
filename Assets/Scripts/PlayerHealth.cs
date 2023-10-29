using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    public GameObject gameOverScreen;
    public Vector2 lastCheckpoint;
    public float isInvincible = 0f;
    private SpriteRenderer sr;
    private float gameOverTime=0.5f;

    // Start is called before the first frame update
    void Start()
    {
        lastCheckpoint = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    public void ResetPlayer()
    {
        currentHealth = maxHealth;
        transform.position = lastCheckpoint;
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
            /*gameOverScreen.SetActive(true);
            GetComponent<PlayerStateManager>().playerRigidbody.velocity = Vector2.zero;
            gameOverTime -= Time.deltaTime;
            if((Input.anyKey||Input.GetAxisRaw("Horizontal")!=0||Input.GetAxisRaw("Vertical")!=0||Input.GetButton("Jump")||Input.GetButton("Dash")) && gameOverTime<=0)
            {
                gameOverScreen.SetActive(false);*/
            ResetPlayer();
                //gameOverTime = 0.5f;
            //}

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
