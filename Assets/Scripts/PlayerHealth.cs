using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    public GameOverScreen gameOverScreen;
    public Vector2 lastCheckpoint;
    public float isInvincible = 0f;
    private SpriteRenderer sr;

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

        if (currentHealth == 0)
        {
            Debug.Log("You died");
            gameOverScreen.gameObject.SetActive(true);
            ResetPlayer();

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
}
