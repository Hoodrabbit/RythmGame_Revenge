using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metronomemake : MonoBehaviour
{
    public float MusicBPM;
    public float stdBPM;

    public AudioSource metro;
    public float Timecheck;
    public Transform mysr;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(stdBPM / MusicBPM);
        mysr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Timecheck += Time.deltaTime;

        if (Timecheck >= 3 + stdBPM / MusicBPM) 
        {
            metro.PlayOneShot(metro.clip);
            transform.Rotate(Vector3.back * 45f);
            Timecheck -= stdBPM / MusicBPM;
            
        }
    }
}
