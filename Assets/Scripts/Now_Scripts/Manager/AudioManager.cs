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
       // Debug.Log("    " + ss.gameObject.name);
        songSelect = ss;
        //Debug.Log("    " + songSelect.gameObject.name);
    }

    public void SetValue(MusicInfo music)
    {
        if (songSelect == null)
        {
            Debug.Log("�����ϴ�.");
        }

        songSelect.InitializeSongSelect();
        songSelect.SongAudio.clip = music.Music;
        songSelect.SongAudio.PlayScheduled(0);
    }


}
