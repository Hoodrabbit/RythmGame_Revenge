using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioscripttest : MonoBehaviour
{
    public AudioSource mainaudio;


    // Start is called before the first frame update
    void Start()
    {
        double curDspTime = AudioSettings.dspTime;
        //Debug.Log(curDspTime);

        //Debug.Log(mainaudio.clip.frequency);
        Debug.Log(mainaudio.timeSamples);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log((double)(mainaudio.time));
        Debug.Log("¿€µø :  "  + mainaudio.timeSamples);
    }
}
