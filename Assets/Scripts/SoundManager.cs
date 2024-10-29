using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public AudioMixerGroup UIsfxMixerGroup;
    List<AudioSource> sfxSources = new List<AudioSource>();


    // Start is called before the first frame update
    void Awake()
    {
       foreach (AudioSource source in FindObjectsOfType<AudioSource>()) 
        {
            if(source.outputAudioMixerGroup == UIsfxMixerGroup)
            {
                sfxSources.Add(source);
                Debug.Log(source.gameObject.name);
                source.ignoreListenerPause = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
