using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;












public class GameManager : Singleton<GameManager>
{
    public GameDataState DataState = GameDataState.Data_UnLoad;

    public GameState state = GameState.None;

    public DifficultState difficultState;

    public KeyPressType keyPressType;


    public AudioSource MainAudio;

    public MusicInfo musicInfo;


    public SongStat BeforeSongRecord;

    //게임 씬 끝날 때 저장받을 변수
    public SongStat songStatus;




    public int speed = 10; //나중에 이거 다른 매니저에다가 옮길 변수 아직 안 옮김

    public bool BossAppear = false;

    public float OffsetValue;



    double startDSPtimeValue;
    
    double CurDspTimeValue; //노래가 시작된 순간의 dsptime 체크용 변수
    Coroutine checkCoroutine;



    int SongValue = 0;
    int SceneModeValue = 0;

    public int NowSelectValue = 0; //임시로 여기서 선택한 곡 보관 나중에 다른 스크립트에서 옮길 예정

    public int SongDelayTime = 4;

    public bool GodMode; //무적모드



    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

    }

    /// <summary>
    /// 노래의 점수 및 기타 콤보와 같은 정보들을 초기화 시켜줌
    /// </summary>
    public void Init_SongStat()
    {
        BeforeSongRecord = songStatus;
        songStatus.Init();
    }





    public void PlayMusicOnly()
    {
        startDSPtimeValue = AudioSettings.dspTime;
        MainAudio.PlayScheduled(AudioSettings.dspTime + MainAudio.time);
       
    }

    public void PlayMusic()
    {
        DataManager.Instance.LoadNote();

        Init_SongStat(); 

        startDSPtimeValue = AudioSettings.dspTime;
        MainAudio.PlayScheduled(AudioSettings.dspTime + SongDelayTime);
        if(state == GameState.Play_Mode)
        {
            checkCoroutine = StartCoroutine(GoToGameResult(MainAudio.clip.length + SongDelayTime));
            StartCoroutine(StartCountdown(SongDelayTime - 2));
        }
        
        
    }
    IEnumerator GoToGameResult(float Value)
    {
        Debug.Log("작동확인 전");
        yield return new WaitForSeconds(Value);

        Debug.Log("작동확인");

        GetScoreAndCombo(ScoreSystem.Instance.Score, ComboSystem.Instance.MaxCombo);

        //모든 노트들의 구독을 취소함

        SceneManager.LoadScene("GameResult");
    }

    public IEnumerator GoToGameOver()
    {
        Debug.Log("작동되는지 확인");


        yield return new WaitForSeconds(1f);


        //모든 노트들의 구독을 취소함

        SceneManager.LoadScene("GameOver");


    }





    IEnumerator StartCountdown(int countdownNum)
    {
        PlayMainUIScript.Instance.CountdownText.gameObject.SetActive(true);
        int count = countdownNum;

        while (count > 0)
        {
            PlayMainUIScript.Instance.CountdownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        PlayMainUIScript.Instance.CountdownText.text = "Start!";
        yield return new WaitForSeconds(0.5f);
        //텍스트 꺼주기
        PlayMainUIScript.Instance.CountdownText.gameObject.SetActive(false);
    }




    public void PauseAudio()
    {
        if(AudioListener.pause == false)
        {
            AudioListener.pause = true;
            StopCoroutine(checkCoroutine);
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
        StartCoroutine(StartCountdown(SongDelayTime - 1));
        checkCoroutine = StartCoroutine(GoToGameResult(MainAudio.clip.length - MainAudio.time + SongDelayTime));
        yield return new WaitForSeconds(SongDelayTime);
        
        AudioListener.pause = false;
        //Time.timeScale = 1;
    }


    public void SetSongStat(SongStat songStat)
    {
        print("fffff");
        if (songStat.IsEmpty())
        {
            print("asdf");
            print(songStat.Score);
        }

        songStatus = songStat;

    }



    public int GetSongValue()
    {
        return SongValue;
    }
    public void SetSongValue(int num)
    {


        SongValue = num;
        DataState = GameDataState.FinishData_Load;
        AudioManager.Instance.GetAudio().PlaySong();
        
    }

    public void SetSongValue(MusicInfo musicInfo)
    {

        this.musicInfo = musicInfo;
        Init_SongStat();
        SongStatusManager.instance.LoadSongStat();
        SongDetailPanelScript.instance.Init_MusicInfo(this.musicInfo, songStatus);
    }


    public int GetSceneModeValue()
    {
        return SceneModeValue;
    }
    public void SetSceneModeValue(int num)
    {
        SceneModeValue = num;
    }

    public void SetDifficultValue(int num)
    {
        difficultState = (DifficultState)num;
        Init_SongStat();
        SongStatusManager.instance.LoadSongStat();


        SongDetailPanelScript.instance.Init_MusicInfo(this.musicInfo, songStatus);
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

    public int GetScore()
    {
        return songStatus.Score;
    }
    public int GetCombo()
    {
        return songStatus.Combo;
    }

    public void GetScoreAndCombo(int score_Get, int combo_Get)
    {
        songStatus.Get_Score(score_Get);
        songStatus.Get_Combo(combo_Get);
    }

    public void Increase_Perfect()
    {
        songStatus.Increase_Perfect();
    }
    public void Increase_Great()
    {
        songStatus.Increase_Great();
    }
    public void Increase_Miss()
    {
        songStatus.Increase_Miss();
    }

    public float GetAccuracy()
    {
        float value = (float)songStatus.Perfect / (songStatus.Perfect + songStatus.Miss + songStatus.Great);

        Debug.Log("value: "  + value);

        return value;
    }



    public void StopGameManagerCoroutine()
    {
        StopAllCoroutines();
    }







}




