using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public double StartDspTime;
    public double CurDspTime;

    private void Start()
    {
        StartDspTime = AudioSettings.dspTime;
        Debug.Log(StartDspTime);
    }

    void Update()
    {
       CurDspTime = AudioSettings.dspTime;

        Debug.Log(CurDspTime);



    }
}
