using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testtset : MonoBehaviour
{

    public AudioSource myaudio;


    // Start is called before the first frame update
    void Start()
    {
        //int changeSamplepos = 80 * myaudio.clip.frequency;
        //myaudio.timeSamples = changeSamplepos;
        //Debug.Log(myaudio.clip.frequency);
        myaudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
