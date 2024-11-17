using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SongDetailPanelScript : MonoBehaviour
{
    public static SongDetailPanelScript instance;


    public TMP_Text MusicName;
    public TMP_Text ArtistName;

    public TMP_Text MaxScore;
    public TMP_Text MaxComboInfo;
    public TMP_Text Accuracy;
    public TMP_Text Clear_Count;

    public Image ScoreAlphaBet;






    private void Awake()
    {
        instance = this;
    }





    public void Init_MusicInfo(MusicInfo music, SongStat stat)
    {
        MusicName.text = music.Music_Name;
        ArtistName.text = music.Artist_Name;

        if(stat.IsEmpty())
        {
            MaxScore.text = "없음";
            MaxComboInfo.text = "없음";
            Accuracy.text = "없음";
            Clear_Count.text = "0";
        }
        else
        {
            MaxScore.text = stat.Score.ToString();
            MaxComboInfo.text = stat.Combo.ToString();
            Accuracy.text = stat.Perfect.ToString();
            Clear_Count.text = stat.ClearCount.ToString();
        }


        //점수 콤보 정확도 클리어횟수를 저장받을 리스트를 추가로 제작해야함
        //난이도마다...





    }
}
