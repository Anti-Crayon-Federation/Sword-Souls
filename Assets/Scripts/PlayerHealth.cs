using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 4;
    public float currentHealth;
    public GameOverScreen gameOverScreen;
    public Vector2 lastCheckpoint;
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Awake()
    {
        lastCheckpoint = transform.position;
    }

    private void Update()
    {
        if (Input.GetKey("b"))
        {
            TakeDamange(4);
        }

        if (currentHealth == 0)
        {
            Debug.Log("You died");
            gameOverScreen.gameObject.SetActive(true);

            //Possible death animation
            //Fail state

            GameObject newPlayer = GameObject.Instantiate(this.gameObject);

            newPlayer.transform.position = lastCheckpoint;
            Destroy(gameObject);
        }
    }

    public void TakeDamange(int amount)
    {
            currentHealth -= amount;
    }
}
