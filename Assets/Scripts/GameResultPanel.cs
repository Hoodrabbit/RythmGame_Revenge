using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;


public class GameResultPanel : MonoBehaviour
{
    public Text SongName;
    public Text ArtistName;

    public Text Score;
    public Text Combo;
    //public Text etc; //Perfect, Great, Good, Miss 이런 것들 텍스트로 나타내줄 예정 한번에 여러개 하면 좋을 듯 함

    public Text Perfect;
    public Text Great;
    public Text Miss;
    public Text HighScore;
    public Text Accuracy;

    public VisualizeScoreAlphabet alphabet;


    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.musicInfo != null)
        {
            SongName.text = GameManager.Instance.musicInfo.Music_Name.ToString();
            ArtistName.text = GameManager.Instance.musicInfo.Artist_Name.ToString();
            Score.text = GameManager.Instance.GetScore().ToString();
            Combo.text = GameManager.Instance.GetCombo().ToString();

            Perfect.text = GameManager.Instance.songStatus.Perfect.ToString();
            Great.text = GameManager.Instance.songStatus.Great.ToString();
            Miss.text = GameManager.Instance.songStatus.Miss.ToString();
            //HighScore.text = GameManager.Instance.songStatus.Score.ToString();

            Accuracy.text = GameManager.Instance.GetAccuracy().ToString("P2");
            alphabet.VisualizeImage(GameManager.Instance.GetAccuracy());
            SongStatusManager.instance.SaveGameResult();
        }

        

        //콤보랑 점수는 게임 씬이 종료되기 전에 게임매니저에 저장할 변수하나 만들어줘서 거기다가 저장시켜서 들고 오도록


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
