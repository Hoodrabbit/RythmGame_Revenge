using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public List<MusicInfo> musicInfos;

    public void SetMusic(int value)
    {
        Debug.Log(value);
        GameManager.Instance.musicInfo = musicInfos[value];
    }


}
