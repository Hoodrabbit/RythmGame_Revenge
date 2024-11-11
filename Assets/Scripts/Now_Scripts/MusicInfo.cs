using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MusicInfo : ScriptableObject
{
    public string Artist_Name;

    public string Music_Name;

    public AudioClip Music;

    public float BPM;

    //public string NoteFileDirection;

    public Sprite MusicSprite;

}


//음악을 선택했을 때 선택된 음악의 정보와 난이도를 확인하여
//저장되어있는 데이터를 꺼내어 패널에 정보를 출력함


public class Music_Status
{


    public DifficultState difficult;

    public int Score;
    public int MaxCombo;
    public float Accuracy;
    public int Clear_Count;



}
