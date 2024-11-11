using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;






[System.Serializable]
public struct SongStat
{
    public int Score;
    public int Combo;

    public int Perfect;
    public int Great;
    public int Miss;

    public float Accuracy;
    public int ClearCount;


    public bool IsEmpty()
    {
        return Score == 0 && Combo == 0 && Perfect == 0 && Great == 0 && Miss == 0;
    }



    public void Init()
    {
        Score = 0;
        Combo = 0;
        Perfect = 0;
        Great = 0;
        Miss = 0;
    }



    public void Increase_Perfect() => Perfect++;

    public void Increase_Miss() => Miss++;

    public void Increase_Great() => Great++;


    public void Get_Combo(int combo)
    {
        //맥스 콤보를 넘지 못했을 경우 콤보수가 증가되지 않음
        Combo = combo;
    }

    public void Get_Score(int score) => Score=score;


};







public class SongStatusManager : MonoBehaviour
{
    public static SongStatusManager instance;


    string SongStatsFolder = Application.streamingAssetsPath + "\\MusicStatusInfo_Folder";

    string SongStatsDataPath;
    string songStatsDataPath;
    protected void Awake()
    {
        instance = this;
       

    }

    public void Start()
    {
        if (!Directory.Exists(SongStatsFolder))
        {
            Directory.CreateDirectory(SongStatsFolder);
        }
        SongStatsDataPath = Path.Combine(SongStatsFolder, GameManager.Instance.musicInfo.Music_Name + "_SongStatus");
    }


    public void SaveGameResult()
    {
        SongStatsDataPath = Path.Combine(SongStatsFolder, GameManager.Instance.musicInfo.Music_Name + "_SongStatus");
        songStatsDataPath = SongStatsDataPath + "_" + GameManager.Instance.difficultState.ToString() + ".txt";

        if(File.Exists(songStatsDataPath)) 
        {


            StreamWriter writer = File.CreateText(songStatsDataPath);

            writer.WriteLine(GameManager.Instance.songStatus);

            writer.Close();
        }
        else
        {
            

            FileStream fileStream = File.Create(songStatsDataPath);
            StreamWriter fileWriter = new StreamWriter(fileStream);

            Debug.Log("작동확인");

            fileWriter.WriteLine(GameManager.Instance.songStatus.Score);
            fileWriter.WriteLine(GameManager.Instance.songStatus.Combo);
            fileWriter.WriteLine(GameManager.Instance.songStatus.Perfect);
            fileWriter.WriteLine(GameManager.Instance.songStatus.Great);
            fileWriter.WriteLine(GameManager.Instance.songStatus.Miss);

            fileWriter.Close();
        }


    }

    public void LoadSongStat()
    {
        SongStatsDataPath = Path.Combine(SongStatsFolder, GameManager.Instance.musicInfo.Music_Name + "_SongStatus");
        songStatsDataPath = SongStatsDataPath + "_" + GameManager.Instance.difficultState.ToString() + ".txt";

        SongStat songStat = new SongStat();


        try
        {
            using (StreamReader reader = new StreamReader(songStatsDataPath))
            {
                // 한 줄씩 읽어서 각 변수에 할당
                songStat.Score = int.Parse(reader.ReadLine());
                songStat.Combo = int.Parse(reader.ReadLine());
                songStat.Perfect = int.Parse(reader.ReadLine());
                songStat.Great = int.Parse(reader.ReadLine());
                songStat.Miss = int.Parse(reader.ReadLine());
            }

            GameManager.Instance.SetSongStat(songStat);


        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading data: " + ex.Message);
        }
    }





}
