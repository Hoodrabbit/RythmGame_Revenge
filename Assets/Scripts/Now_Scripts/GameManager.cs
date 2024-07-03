using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : Singleton<GameManager>
{
    public GameState state = GameState.None;
    public AudioSource MainAudio;

    public void Start()
    {
        double CurDspTime = AudioSettings.dspTime;

        MainAudio.PlayScheduled(CurDspTime + 3f); //3�ʵڿ� �뷡 ����
    }


}

public enum GameState
{
    None,
    Offset_Mode,
    Debug_Mode,
    Play_Mode
};