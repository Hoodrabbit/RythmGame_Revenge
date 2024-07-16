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
        InitalAudio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitalAudio()
    {
        audioSource.clip = GameManager.Instance.musicInfo.Music;
        GameManager.Instance.SetAudio(audioSource);

        if(GameManager.Instance.state == GameState.Play_Mode)
        {
            GameManager.Instance.PlayMusic();
        }
        
    }

}
