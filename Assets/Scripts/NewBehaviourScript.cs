using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public double StartDspTime;
    public double CurDspTime;

    public AudioSource mainaudio;


    private void Start()
    {
        StartDspTime = AudioSettings.dspTime;
        Debug.Log(StartDspTime);

        Debug.Log(mainaudio.timeSamples);

    }

    void Update()
    {
       CurDspTime = AudioSettings.dspTime;

        Debug.Log((float)mainaudio.timeSamples / mainaudio.clip.frequency + "      ,       " + mainaudio.time);

        //Debug.Log(CurDspTime);



    }
}
