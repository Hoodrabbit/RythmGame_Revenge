using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System;





//0번 타입 : 보스 타입인데 보스도 종류가 있다보니 어떤 식으로 하면 좋을 지 
//아니면 보스만 별개로 100번대 타입으로 하는 것도 방법일듯함

//ㅁㄴㅇㄹ





public class NoteInfoPos
{
    public float xpos; // 노트의 X 값(에디터 상 정보이기 때문에 나중에 실제 게임 씬에서는 해당 데이터에 추가로 오프셋이나 속도에 따라 위치를 조절할 수 있음
    public int HeightValue; //노트 높이 번호로 저장받아서 나중에 불러올 때는 그 번호에 따라 Y값을 특정 값으로 할당시킴
    //신규추가
    public int NoteType; //노트의 종류에 대한 정보를 저장받을 변수   (0 : 화면 전환 노트(칠 수 없음 아니면 칠 수 있게 만들수도?)1 : 기본 , 2 : 롱 노트, 3 : 유령노트 특수 노트가 될 예정 여기에서 더 추가 될 수 있음)
    public int LongNoteStartEndCheck; //롱노트의 길이를 체크해줄 변수(정확히는 시작과 끝) (롱노트 종류가 아닌 경우 전부 0이고 롱노트일 때 1이 시작 2가 끝을 체크해줌)

    public double SongTime; //찍은 노트에 해당하는 노래의 시간

    public int EnemyType = 0; // 적의 음률 상태(특이 케이스)를 통해서 기존의 무기 상태로는 공격이 불가능함 무시되기 때문에 무기를 변경하여 노트를 쳐야 함



    public NoteInfoPos(float x, int h, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        xpos = x;
        HeightValue = h;
        NoteType = noteType;
        this.LongNoteStartEndCheck = LongNoteStartEndCheck;
        SongTime = songtime;
        EnemyType = enemyType;
    
    }



}



/// <summary>
/// 1 : Note 종류 2 : 위치  3 : 높이   4 : 롱노트 전용 변수(시작 1, 끝 2)   5 : 노래 시간   6 : 음률 전용 변수(0이면 기본 1, 2까지 존재)
/// </summary>
public class NoteInfoAll //찍은 노트에 대한 정보를 담아 줄 클래스
{
    public GameObject Note;
    public NoteInfoPos notePos;
    

    public NoteInfoAll(GameObject Note, float x, int h, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        this.Note = Note;
        notePos = new NoteInfoPos(x, h, noteType, LongNoteStartEndCheck, songtime, enemyType);
    }



}




public class DataManager : Singleton<DataManager>
{
    //파일 관리를 해줄 스크립트 게임 매니저의 스크립터블에서 파일 주소를 뽑아옴
#if UNITY_EDITOR
    static string NoteDataFolder = Application.streamingAssetsPath + "\\NOTEDATA_Folder";
#else
        static string NoteDataFolder = Application.streamingAssetsPath + "\\NOTEDATA_Folder";
#endif
    string NoteDataPath;

    public List<NoteInfoAll> EditNotes = new List<NoteInfoAll>();



    protected override void Awake()
    {
        base.Awake();
        if (!Directory.Exists(NoteDataFolder))
        {
            Directory.CreateDirectory(NoteDataFolder);
        }
        NoteDataPath = Path.Combine(NoteDataFolder, GameManager.Instance.musicInfo.Music_Name + "_NoteData.txt");
    }


    public void ListNullCheck() //해당 리스트 중에서 null이 존재하는지 확인하고 만약 존재한다면 해당 데이터는 제거함
    {
        int i = EditNotes.Count - 1;
        while (i >= 0)
        {
            if (EditNotes[i].Note == null)
            {
                Debug.Log(i + "이거 지워요");
                EditNotes.RemoveAt(i);
            }
            i--;
        }
    }


    public void SaveNote()
    {
        EditNotes = EditNotes.OrderBy(N => N.notePos.xpos).ToList();

        if (File.Exists(NoteDataPath))
        {
            Debug.Log("파일 존재");
            StreamWriter writer = File.CreateText(NoteDataPath);


            ListNullCheck();
            writer.WriteLine(EditNotes.Count);

            foreach (NoteInfoAll np in EditNotes)
            {

                writer.WriteLine($"{np.notePos.xpos / GameManager.Instance.speed} , {np.notePos.HeightValue}, {np.notePos.NoteType}, {np.notePos.LongNoteStartEndCheck}, {np.notePos.SongTime}, {np.notePos.EnemyType}");
            }
            writer.Close();
        }
        else
        {
            Debug.Log("파일 생성");
            FileStream fileStream = File.Create(NoteDataPath);
            StreamWriter fileWriter = new StreamWriter(fileStream);

            fileWriter.WriteLine(EditNotes.Count);

            foreach (NoteInfoAll np in EditNotes)
            {
                fileWriter.WriteLine($"{np.notePos.xpos / GameManager.Instance.speed} , {np.notePos.HeightValue}, {np.notePos.NoteType}, {np.notePos.LongNoteStartEndCheck}, {np.notePos.SongTime}, {np.notePos.EnemyType}");
            }
            fileWriter.Close();

        }

        //EditNotes.Add(songtimes.Count.ToString());


        //노트 리스트 정보에 대해서 파싱을 함

    }

    public void LoadNote()
    {
        //이미 생성되어 있으면 해당 데이터를 지우고 다시 깔아줘야 함


        //string NoteDataPath = Path.Combine(NoteDataFolder, GameManager.Instance.musicInfo.NoteFileDirection);
        //로드 할 때는 메인 노트 관리해주는 오브젝트 위치를 받아서 그만큼 더해줘야함(0 부터 -값이기 때문에 빼줘도 더해지기만 함)

        StreamReader NoteParsing = new StreamReader(NoteDataPath);
        int NoteCount = Int32.Parse(NoteParsing.ReadLine());


        //여기에서 로드할 때 싹다 초기화시켜줘야함
        while (EditNotes.Count > 0)
        {
            Destroy(EditNotes[EditNotes.Count - 1].Note);
            EditNotes.RemoveAt(EditNotes.Count - 1);
            Debug.Log("노트 초기화 중입니다.");
        }




        for (int i = 0; i < NoteCount; i++)
        {
            float xpos; int height; int NoteType; int LongNoteStartEndCheck; double SongTime; int enemyType;
            
            string LineText; //파싱한 노트 정보를 저장받을 문자열
            
            LineText = NoteParsing.ReadLine();

            string[] split_Text = LineText.Split(',');
            xpos = float.Parse(split_Text[0]);
            height = Int32.Parse(split_Text[1]);
            NoteType = Int32.Parse(split_Text[2]);
            LongNoteStartEndCheck = Int32.Parse(split_Text[3]);

            if (split_Text[4] != null)
            {
                SongTime = double.Parse(split_Text[4]);
            }
            else
            SongTime = 0;

            if (split_Text[5] != null) 
            {
                enemyType = Int32.Parse(split_Text[5]);
            }
            else
            enemyType = 0;


            if (GameManager.Instance.state != GameState.Play_Mode)
                EditManager.Instance.MakeNote(xpos * GameManager.Instance.speed + EditManager.Instance.GetNPXpos() , height, NoteType, LongNoteStartEndCheck, SongTime, enemyType);
            else
            {
                PlayManager.Instance.PlayScene_NoteMaker( xpos * GameManager.Instance.speed, height, NoteType, LongNoteStartEndCheck, SongTime, enemyType);
                
                if(LongNoteStartEndCheck != 2 || NoteType < 100)
                {
                    PlayManager.Instance.AddNoteCount();
                }
                PlayManager.Instance.SetComboCount();
            }
        
        }

       
        NoteParsing.Close();
    }

    void SaveGameResult()
    {
        //Debug.Log(PlayManager.Instance);
    }







}
