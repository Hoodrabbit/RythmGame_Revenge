using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SongSelect : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public AudioSource SongAudio;
    public float Volume;

    public void Awake()
    {
       
    }

    public void Start()
    {//audioClips = MusicManager.Instance.musicClipList();
        SongAudio = GetComponent<AudioSource>();
        GameManager.Instance.MainAudio = SongAudio;
        SongAudio.clip = GameManager.Instance.musicInfo.Music;
        AudioManager.Instance.SetAudio(this);
        StartCoroutine(PreviewSong());
    }

    IEnumerator PreviewSong()
    {
        while(true)
        {
            if(!SongAudio.isPlaying)
            {
                SongAudio.volume = 0.5f;
                SongAudio.PlayScheduled(0);
            }

            if (SongAudio.time <= 20 && SongAudio.time > 15)
            {
                if (SongAudio.volume > 0)
                {
                    SongAudio.volume -= 0.15f * Time.deltaTime;
                }
            }

            if (SongAudio.time > 20)
            {
                SongAudio.Stop();
            }
            yield return null;

        }

        
    }
}