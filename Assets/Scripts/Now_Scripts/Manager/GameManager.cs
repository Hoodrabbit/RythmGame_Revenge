using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    Coroutine checkCoroutine;



    int SongValue = 0;
    int SceneModeValue = 0;

    public int NowSelectValue = 0; //임시로 여기서 선택한 곡 보관 나중에 다른 스크립트에서 옮길 예정

    public int SongDelayTime = 4;


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
        startDSPtimeValue = AudioSettings.dspTime;
        MainAudio.PlayScheduled(AudioSettings.dspTime + SongDelayTime);
        checkCoroutine = StartCoroutine(GoToGameResult(MainAudio.clip.length + SongDelayTime));
        StartCoroutine(StartCountdown(SongDelayTime-1));
        
    }
    IEnumerator GoToGameResult(float Value)
    {
        Debug.Log("작동확인 전");
        yield return new WaitForSeconds(Value);

        Debug.Log("작동확인");

        GetScoreAndCombo(ScoreSystem.Instance.Score, ComboSystem.Instance.MaxCombo);

        SceneManager.LoadScene("GameResult");
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




