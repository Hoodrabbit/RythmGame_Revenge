using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state = GameState.None;

    public AudioSource MainAudio;

    public MusicInfo musicInfo;

    public int speed = 10; //���߿� �̰� �ٸ� �Ŵ������ٰ� �ű� ���� ���� �� �ű�

    double startDSPtimeValue;
    double CurDspTimeValue; //�뷡�� ���۵� ������ dsptime üũ�� ����

    int SongValue = 0;
    int SceneModeValue = 0;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

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
        MainAudio.PlayScheduled(AudioSettings.dspTime + MainAudio.time);
    }

    public void PlayMusic()
    {
        DataManager.Instance.LoadNote();
        MainAudio.PlayScheduled(AudioSettings.dspTime + 3f);


    }

    public int GetSongValue()
    {
        return SongValue;
    }
    public void SetSongValue(int num)
    {
        SongValue = num;
    }

    public void SetSongValue(MusicInfo musicInfo)
    {
       this.musicInfo = musicInfo;
    }


    public int GetSceneModeValue()
    {
        return SceneModeValue;
    }
    public void SetSceneModeValue(int num)
    {
        SceneModeValue = num;
    }



    public void SetAudio(AudioSource audio)
    {
        MainAudio = audio;

    }
    public float ClipLength()
    {
        return musicInfo.Music.length;
    }

    public double StartDspTime()
    {
        return startDSPtimeValue;
    }

    public float GetBPS()
    {
        return 60 / musicInfo.BPM;
    }



    public void UseDFJK()
    {
        //dfjk Ű ����

        Debug.Log("DFJK ���");
    }

    public void UseArrowKey()
    {
        //arrowkey Ű ����
        Debug.Log("����Ű ���");
    }




}




