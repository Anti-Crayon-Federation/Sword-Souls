using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public Color highlightColor = Color.yellow;
    public bool enableHighlight = true;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private bool isEnemyInside = false;

    // Start is called before the first frame update
    void Start()
    {
        // This Code is reused from player detection and needs to be updated
        // This Code is the start of a death spiral / "Do something elese"
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // This function is called when another object enters the trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            Debug.Log("Enemies Detected");
            isEnemyInside = true;
            if (enableHighlight)
            {
                spriteRenderer.color = highlightColor;
            }
        }
    }

    // This function is called when another object exits the trigger collider
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            Debug.Log("No Enemies Detected");
            isEnemyInside = false;
            if (enableHighlight)
            {
                spriteRenderer.color = originalColor;
            }
        }
    }

    // This method allows other scripts to check if the player is inside the area
    public bool IsEnemyInside()
    {
        return isEnemyInside;
    }
}