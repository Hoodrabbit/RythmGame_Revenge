using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    SongSelect songSelect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetAudio(SongSelect ss)
    {
        songSelect = ss;
    }

    public void SetValue(MusicInfo music)
    {
        songSelect.SongAudio.clip = music.Music;
        songSelect.SongAudio.PlayScheduled(0);
    }


}
