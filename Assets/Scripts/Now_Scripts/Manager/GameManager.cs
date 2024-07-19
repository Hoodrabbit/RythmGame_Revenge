using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state = GameState.None;
    public AudioSource MainAudio;
    public MusicInfo musicInfo;
    public float speed = 3f; //���߿� �̰� �ٸ� �Ŵ������ٰ� �ű� ���� ���� �� �ű�

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
        //if(state == GameState.Play_Mode)
        //{
        //    DataManager.Instance.LoadNote();
        //    MainAudio.PlayScheduled(CurDspTime);
        //}



    }
    public void PlayMusicOnly()
    {
        MainAudio.PlayScheduled(AudioSettings.dspTime + 3f);
    }

    public void PlayMusic()
    {
        DataManager.Instance.LoadNote();
        MainAudio.PlayScheduled(AudioSettings.dspTime+3f);
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