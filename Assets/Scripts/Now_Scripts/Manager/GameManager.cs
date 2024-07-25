using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state = GameState.None;
    
    public AudioSource MainAudio;
    
    public MusicInfo musicInfo;

    public int speed = 10; //나중에 이거 다른 매니저에다가 옮길 변수 아직 안 옮김

    double startDSPtimeValue;
    double CurDspTimeValue; //노래가 시작된 순간의 dsptime 체크용 변수


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
    }

    public void Start()
    {
        
        MusicManager.Instance.SetMusic(0);
        //MainAudio.PlayScheduled(CurDspTime); 
        //if(state == GameState.Play_Mode)
        //{
        //    DataManager.Instance.LoadNote();
        //    MainAudio.PlayScheduled(CurDspTime);
        //}



    }
    public void PlayMusicOnly()
    {
        startDSPtimeValue = AudioSettings.dspTime;
        MainAudio.PlayScheduled(AudioSettings.dspTime);
    }

    public void PlayMusic()
    {
        DataManager.Instance.LoadNote();
        MainAudio.PlayScheduled(AudioSettings.dspTime + 3f);
        
        
    }


    public void SetAudio(AudioSource audio)
    {
        MainAudio = audio;

    }
    public float ClipLength()
    {
        return MainAudio.clip.length;
    }

    public double StartDspTime()
    {
        return startDSPtimeValue;
    }

    public float GetBPS()
    {
        return 60 /musicInfo.BPM;
    }


}




public enum GameState
{
    None,
    Offset_Mode,
    Debug_Mode,
    Play_Mode
};