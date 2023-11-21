using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource soundPlayerWinState;
    public AudioSource soundFootstep;
    public AudioSource soundPlayerDamage1;
    public AudioSource soundPlayerDamage2;
    public AudioSource soundPlayerJump;
    public AudioSource soundPlayerDash;
    public AudioSource soundPlayerWalk;
    public AudioSource soundPlayerGameOver;
    public AudioSource soundPlayerSwitchToMelee;
    public AudioSource soundPlayerSwitchToRanged;
    public AudioSource soundPlayerWin;
    public AudioSource soundPlayerHeal;
    public AudioSource soundChargeKnightDrill;
    public AudioSource soundChargeKnightMoving;
    public AudioSource soundChargeKnightNotAttacking;
    public AudioSource soundClamCannonFire;


    // Start is called before the first frame update
    void Start()
    { 
        if(gameObject.name=="Player")
        {
            soundPlayerJump = transform.Find("SoundJump").gameObject.GetComponent<AudioSource>();
            soundPlayerDash = transform.Find("SoundDash").gameObject.GetComponent<AudioSource>();
            soundPlayerWalk = transform.Find("SoundWalk").gameObject.GetComponent<AudioSource>();
            soundPlayerDamage1 = transform.Find("SoundPlayerDamage1").gameObject.GetComponent<AudioSource>();
            soundPlayerDamage2 = transform.Find("SoundPlayerDamage2").gameObject.GetComponent<AudioSource>();
            soundPlayerWinState = transform.Find("SoundWinState").gameObject.GetComponent<AudioSource>();
            soundPlayerSwitchToMelee = transform.Find("SoundSwitchWeaponChainsword1").gameObject.GetComponent<AudioSource>();
            soundPlayerSwitchToRanged = transform.Find("SoundSwitchWeaponGun").gameObject.GetComponent<AudioSource>();
            soundPlayerHeal = transform.Find("SoundHeal").gameObject.GetComponent<AudioSource>();
            soundPlayerGameOver = transform.Find("SoundGameOver").gameObject.GetComponent<AudioSource>();
            Debug.Log("player sounds assigned");
        }
        if(gameObject.name.Contains("Enemy ground"))
        {
            soundChargeKnightDrill = transform.Find("Drill_Knight_Drill_Sound").gameObject.GetComponent<AudioSource>();
            soundChargeKnightMoving = transform.Find("Drill_Knight_Treads_Sound").gameObject.GetComponent<AudioSource>();
            soundChargeKnightNotAttacking = transform.Find("Drill_Knight_Motor_Sound").gameObject.GetComponent<AudioSource>();
        }

        if(gameObject.name.Contains("Enemy flying"))
        {
            soundClamCannonFire = transform.Find("SoundClammonCannon").gameObject.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioSource _sound)
    {
        
        _sound.Play();
    }

    public void PlaySound(AudioSource _sound, float _trimStart)
    {
        Debug.Log("playing sound");
        _sound.time = _trimStart;
        _sound.Play();
    }
}
