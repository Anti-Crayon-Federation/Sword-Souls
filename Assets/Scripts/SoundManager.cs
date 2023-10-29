using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource soundPlayerWinState;
    public AudioSource soundFootstep;

    public AudioSource soundPlayerJump;
    public AudioSource soundPlayerDash;
    public AudioSource soundPlayerWalk;

    // Start is called before the first frame update
    void Start()
    {
        
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
        _sound.time = _trimStart;
        _sound.Play();
    }
}
