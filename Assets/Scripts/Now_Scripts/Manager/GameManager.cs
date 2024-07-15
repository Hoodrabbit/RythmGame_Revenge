using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state = GameState.None;
    public AudioSource MainAudio;
    public MusicInfo musicInfo;
    public float speed = 3f; //나중에 이거 다른 매니저에다가 옮길 변수 아직 안 옮김

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
    }

    public void Start()
    {
        double CurDspTime = AudioSettings.dspTime;
        MusicManager.Instance.SetMusic(0);
        //MainAudio.PlayScheduled(CurDspTime); 


    }

    public void SetAudio(AudioSource audio)
    {
        MainAudio = audio;

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