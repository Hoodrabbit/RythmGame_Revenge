using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudioScript : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if(GameManager.Instance.state != GameState.Offset_Mode)
        {
            InitalAudio();
        }
        else
        {
            InitalAudio();
            GameManager.Instance.PlayMusicOnly();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitalAudio()
    {
        if(GameManager.Instance.state != GameState.Offset_Mode) 
        {
            audioSource.clip = GameManager.Instance.musicInfo.Music;
        }
        
        
        GameManager.Instance.SetAudio(audioSource);

        if(GameManager.Instance.state == GameState.Play_Mode)
        {
            GameManager.Instance.PlayMusic();
        }
        
    }

}
