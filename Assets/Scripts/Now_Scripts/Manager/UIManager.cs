using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public void PlayMusic()
    {
        GameManager.Instance.PlayMusicOnly();
    }

    public void StopMusic()
    {
        GameManager.Instance.MainAudio.Pause();
    }
}
