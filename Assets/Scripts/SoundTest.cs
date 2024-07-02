using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    float TTime = 0;
    AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        TTime += Time.deltaTime;
        if(TTime >= 2.260317)
        {
            m_AudioSource.Play();
            Debug.Log("효과음 작동");
            TTime -= 2.260317f; 
        }
    }
}
