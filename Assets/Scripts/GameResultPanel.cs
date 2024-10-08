using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameResultPanel : MonoBehaviour
{
    public Text SongName;
    public Text ArtistName;

    public Text Score;
    public Text Combo;
    public Text etc; //Perfect, Great, Good, Miss 이런 것들 텍스트로 나타내줄 예정 한번에 여러개 하면 좋을 듯 함


    // Start is called before the first frame update
    void Start()
    {
        SongName.text = GameManager.Instance.musicInfo.Music_Name.ToString();
        ArtistName.text = GameManager.Instance.musicInfo.Artist_Name.ToString();
        Score.text = GameManager.Instance.Score.ToString();
        Combo.text = GameManager.Instance.Combo.ToString();
        //콤보랑 점수는 게임 씬이 종료되기 전에 게임매니저에 저장할 변수하나 만들어줘서 거기다가 저장시켜서 들고 오도록


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
