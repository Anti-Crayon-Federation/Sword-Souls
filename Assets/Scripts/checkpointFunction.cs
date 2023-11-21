using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointFunction : MonoBehaviour
{
    public GameObject healthBar;
    public Sprite doorOpen;
    public Sprite doorClosed;

    private void Start()
    {
        healthBar = GameObject.Find("Bullets&HealthTemplate").gameObject;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        PlayerHealth e = collision.GetComponent<PlayerHealth>();
        if (e != null)
        {
            Debug.Log("Reached checkpoint");
            gameObject.transform.Find("CheckpointDoor_closed").gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
            e.lastCheckpoint = transform.position;
            e.currentHealth = e.maxHealth;
            healthBar.transform.Find("1hp").gameObject.SetActive(true);
            healthBar.transform.Find("2hp").gameObject.SetActive(true);
            healthBar.transform.Find("3hp").gameObject.SetActive(true);
            healthBar.transform.Find("4hp").gameObject.SetActive(true);
            e.GetComponent<PlayerWeapon>().bulletsLeft = e.GetComponent<PlayerWeapon>().maxBullets;
            healthBar.transform.Find("1shots").gameObject.SetActive(true);
            healthBar.transform.Find("2shots").gameObject.SetActive(true);
            healthBar.transform.Find("3shots").gameObject.SetActive(true);
            healthBar.transform.Find("4shots").gameObject.SetActive(true);
            FindObjectOfType<SoundManager>().PlaySound(FindObjectOfType<SoundManager>().soundPlayerHeal);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.transform.Find("CheckpointDoor_closed").gameObject.GetComponent<SpriteRenderer>().sprite = doorClosed;
    }
}
