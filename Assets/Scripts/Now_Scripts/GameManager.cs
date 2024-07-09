using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : Singleton<GameManager>
{
    public GameState state = GameState.None;
    public AudioSource MainAudio;
    public AudioClip[] audioClips;
    public void Start()
    {
        double CurDspTime = AudioSettings.dspTime;

        //MainAudio.PlayScheduled(CurDspTime); 


    }
    public float ClipLength()
    {
        return MainAudio.clip.length;
    }

}




public enum GameState
{
    None,
    Offset_Mode,
    Debug_Mode,
    Play_Mode
};