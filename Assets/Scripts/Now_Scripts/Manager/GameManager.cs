using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameDataState DataState = GameDataState.Data_UnLoad;


    public GameState state = GameState.None;

    public AudioSource MainAudio;

    public MusicInfo musicInfo;


    //게임 씬 끝날 때 저장받을 변수
    public int Score;
    public int Combo;





    public int speed = 10; //나중에 이거 다른 매니저에다가 옮길 변수 아직 안 옮김

    public bool BossAppear = false;

    public float OffsetValue;



    double startDSPtimeValue;
    double CurDspTimeValue; //노래가 시작된 순간의 dsptime 체크용 변수

    int SongValue = 0;
    int SceneModeValue = 0;

    public int NowSelectValue = 0; //임시로 여기서 선택한 곡 보관 나중에 다른 스크립트에서 옮길 예정


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

    }

    public void Start()
    {
        Debug.Log("게임매니저 먼저 실행");
        //InitializeSongSelect();
        //MusicManager.Instance.SetMusic(0);

    }

    public void Update()
    {
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

    public void PauseAudio()
    {
        if(AudioListener.pause == false)
        {
            AudioListener.pause = true;
            //Time.timeScale = 0;
        }
        else
        {
            StartCoroutine(UnPauseGameAudio());
        }
        
    }

    public void PlayAudio()
    {
        AudioListener.pause = false;
        //Time.timeScale = 1;
    }

    IEnumerator UnPauseGameAudio()
    {
        yield return new WaitForSeconds(2f);

        AudioListener.pause = false;
        //Time.timeScale = 1;
    }





    public int GetSongValue()
    {
        return SongValue;
    }
    public void SetSongValue(int num)
    {

        Debug.Log("SetSongValue 작동");

        SongValue = num;
        DataState = GameDataState.FinishData_Load;
        AudioManager.Instance.GetAudio().PlaySong();
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
        //dfjk 키 선택

        Debug.Log("DFJK 사용");
    }

    public void UseArrowKey()
    {
        //arrowkey 키 선택
        Debug.Log("방향키 사용");
    }

    public void SetOffset(float Offset)
    {
        OffsetValue = Offset;
    }



    public void GetScoreAndCombo(int score_Get, int combo_Get)
    {
        Score = score_Get;
        Combo = combo_Get;
    }






}




