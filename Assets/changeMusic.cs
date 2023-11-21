using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMusic : MonoBehaviour
{
    public bool bossfight=false;
    public bool bossfightOver;
    public AudioSource bossMusic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bossfight)
        {
            GetComponent<AudioSource>().clip = bossMusic.clip;
            bossfight = false;
        }
        if(bossfightOver)
        {
            GetComponent<AudioSource>().enabled=false;
        }
    }
}
