using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public List<MusicInfo> musicInfos;

    public void SetMusic(int value)
    {
        //Debug.Log(value);
        GameManager.Instance.musicInfo = musicInfos[value];
        GameManager.Instance.SetSongValue(value);
    }

    public List<AudioClip> musicClipList()
    {
        List<AudioClip> audioClips = new List<AudioClip>();

        foreach (var music in musicInfos)
        {
            audioClips.Add(music.Music);
        }

        return audioClips;


    }

    public List<string>MusicNameList()
    {
        List<string> MusicNames = new List<string>();

        foreach(var music in musicInfos)
        {
            MusicNames.Add(music.Music_Name);
        }



        return MusicNames;
    }


}
