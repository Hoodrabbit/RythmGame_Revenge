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
        return Score == 0 && Combo == 0 && Perfect == 0 && Great == 0;
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

    public void Get_Score(int score) => Score = score;


};







public class SongStatusManager : MonoBehaviour
{
    public static SongStatusManager instance;


    string SongStatsFolder = Application.streamingAssetsPath + "/MusicStatusInfo_Folder";

    string SongStatsDataPath;
    string SongStatsPath;
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
        //SongStatsDataPath = Path.Combine(SongStatsFolder, GameManager.Instance.musicInfo.Music_Name + "_SongStatus");
    }


    public void SaveGameResult()
    {
        SongStatsDataPath = Path.Combine(SongStatsFolder, $"{GameManager.Instance.musicInfo.Music_Name}_SongStatus");
        SongStatsDataPath = SongStatsDataPath.Replace("\\", "/");
        SongStatsPath = SongStatsDataPath + "_" + GameManager.Instance.difficultState.ToString() + ".txt";

        if (File.Exists(SongStatsPath))
        {
            if (GameManager.Instance.songStatus.Score > GameManager.Instance.BeforeSongRecord.Score)
            {
                // 새 데이터를 작성 (파일 초기화 후 작성)
                using (StreamWriter writer = new StreamWriter(SongStatsPath, false)) // 덮어쓰기 모드
                {
                    writer.WriteLine(GameManager.Instance.songStatus.Score);
                    writer.WriteLine(GameManager.Instance.songStatus.Combo);
                    writer.WriteLine(GameManager.Instance.songStatus.Perfect);
                    writer.WriteLine(GameManager.Instance.songStatus.Great);
                    writer.WriteLine(GameManager.Instance.songStatus.Miss);
                    writer.WriteLine(GameManager.Instance.GetAccuracy());
                }
            }
           


        }
        else
        {
            FileStream fileStream = File.Create(SongStatsPath);
            StreamWriter fileWriter = new StreamWriter(fileStream);

            //Debug.Log("작동확인");

            fileWriter.WriteLine(GameManager.Instance.songStatus.Score);
            fileWriter.WriteLine(GameManager.Instance.songStatus.Combo);
            fileWriter.WriteLine(GameManager.Instance.songStatus.Perfect);
            fileWriter.WriteLine(GameManager.Instance.songStatus.Great);
            fileWriter.WriteLine(GameManager.Instance.songStatus.Miss);
            fileWriter.WriteLine(GameManager.Instance.GetAccuracy());

            fileWriter.Close();
        }
    }

        public void LoadSongStat()
        {
            SongStatsDataPath = Path.Combine(SongStatsFolder, $"{GameManager.Instance.musicInfo.Music_Name}_SongStatus");
            SongStatsDataPath = SongStatsDataPath.Replace("\\", "/");
            SongStatsPath = SongStatsDataPath + "_" + GameManager.Instance.difficultState.ToString() + ".txt";

            SongStat songStat = new SongStat();

            try
            {
                using (StreamReader reader = new StreamReader(SongStatsPath))
                {
                    // 한 줄씩 읽어서 각 변수에 할당
                    songStat.Score = int.Parse(reader.ReadLine());
                    songStat.Combo = int.Parse(reader.ReadLine());
                    songStat.Perfect = int.Parse(reader.ReadLine());
                    songStat.Great = int.Parse(reader.ReadLine());
                    songStat.Miss = int.Parse(reader.ReadLine());
                    songStat.Accuracy = float.Parse(reader.ReadLine()); //파싱할때 데이터 잘보고 파싱해야됨..





                }
                //Debug.Log(songStat.Score + " , " + songStat.Combo + " , " + songStat.Perfect + " , " + songStat.Great);
                GameManager.Instance.SetSongStat(songStat);


            }
            catch (FileNotFoundException ex)
            {
                //Debug.LogError($"File not found: {SongStatsPath}\n{ex.Message}");
            }
            catch (FormatException ex)
            {
                //Debug.LogError($"Data parsing error in file: {SongStatsPath}\n{ex.Message}");
            }
            catch (Exception ex)
            {
                //Debug.LogError($"Unexpected error while reading file: {SongStatsPath}\n{ex.Message}");
            }
        }





    }
