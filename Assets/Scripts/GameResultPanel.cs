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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
