using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public GameDataState DataState = GameDataState.Data_UnLoad;


    public GameState state = GameState.None;

    public AudioSource MainAudio;

    public MusicInfo musicInfo;


    //���� �� ���� �� ������� ����
    public int Score;
    public int Combo;





    public int speed = 10; //���߿� �̰� �ٸ� �Ŵ������ٰ� �ű� ���� ���� �� �ű�

    public bool BossAppear = false;

    public float OffsetValue;



    double startDSPtimeValue;
    
    double CurDspTimeValue; //�뷡�� ���۵� ������ dsptime üũ�� ����
    Coroutine checkCoroutine;



    int SongValue = 0;
    int SceneModeValue = 0;

    public int NowSelectValue = 0; //�ӽ÷� ���⼭ ������ �� ���� ���߿� �ٸ� ��ũ��Ʈ���� �ű� ����

    public int SongDelayTime = 4;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

    }

    public void Start()
    {
        Debug.Log("���ӸŴ��� ���� ����");
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
        Debug.Log("�۵�Ȯ�� ��");
        yield return new WaitForSeconds(Value);

        Debug.Log("�۵�Ȯ��");

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
        //�ؽ�Ʈ ���ֱ�
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

        Debug.Log("SetSongValue �۵�");

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
        //dfjk Ű ����

        Debug.Log("DFJK ���");
    }

    public void UseArrowKey()
    {
        //arrowkey Ű ����
        Debug.Log("����Ű ���");
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




