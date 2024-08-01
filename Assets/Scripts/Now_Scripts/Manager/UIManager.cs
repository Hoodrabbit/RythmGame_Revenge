using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(GameManager.Instance.MainAudio.isPlaying)
            {
                StopMusic();
            }
            else
            {
                PlayMusic();
            }
        }
    }


    public void PlayMusic()
    {
        GameManager.Instance.PlayMusicOnly();
    }

    public void StopMusic()
    {
        GameManager.Instance.MainAudio.Pause();
    }
}
