using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public void PlayMusic()
    {
        GameManager.Instance.MainAudio.PlayScheduled(AudioSettings.dspTime);
    }

    public void StopMusic()
    {
        GameManager.Instance.MainAudio.Pause();
    }
}
