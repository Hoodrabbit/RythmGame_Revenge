using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public SongSelect songSelect;


    public SongSelect GetAudio()
    {
        return songSelect;
    }

    public void SetAudio(SongSelect ss)
    {
        songSelect = ss;
    }

    public void SetValue(MusicInfo music)
    {
        if (songSelect == null)
        {
            Debug.Log("없습니다.");
        }

        songSelect.InitializeSongSelect();
        songSelect.SongAudio.clip = music.Music;
        songSelect.SongAudio.PlayScheduled(0);
    }


}
