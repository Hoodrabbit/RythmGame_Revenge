using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;



public class NoteInfoPos
{
    public float xpos; // 노트의 X 값(에디터 상 정보이기 때문에 나중에 실제 게임 씬에서는 해당 데이터에 추가로 오프셋이나 속도에 따라 위치를 조절할 수 있음
    public int HeightValue; //노트 높이 번호로 저장받아서 나중에 불러올 때는 그 번호에 따라 Y값을 특정 값으로 할당시킴
    //신규추가
    public int NoteType; //노트의 종류에 대한 정보를 저장받을 변수   (-1 : 장애물 노트, 1 : 기본 , 2 : 롱 노트, 3 : 유령노트 특수 노트가 될 예정 여기에서 더 추가 될 수 있음)
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



public class NoteEventInfoPos
{
    public float xpos; // 노트의 X 값(에디터 상 정보이기 때문에 나중에 실제 게임 씬에서는 해당 데이터에 추가로 오프셋이나 속도에 따라 위치를 조절할 수 있음
    public int HeightValue; //노트 높이 번호로 저장받아서 나중에 불러올 때는 그 번호에 따라 Y값을 특정 값으로 할당시킴

    public int EventType; //이벤트 종류 체크

    public double SongTime;


    public NoteEventInfoPos(float x, int h, int eventType, double songtime)
    {
        xpos = x;
        HeightValue = h;
        EventType = eventType;
        SongTime = songtime;
    }



}

public class EventInfoAll
{
    public GameObject EventNote;
    public NoteEventInfoPos eventPos;

    public EventInfoAll(GameObject eventNote, float x, int h, int eventType, double songtime)
    {
        EventNote = eventNote;
        eventPos = new NoteEventInfoPos(x, h, eventType, songtime);
    }


}







public class DataManager : Singleton<DataManager>
{
    //파일 관리를 해줄 스크립트 게임 매니저의 스크립터블에서 파일 주소를 뽑아옴
    string NoteDataFolder = Application.streamingAssetsPath + "\\NOTEDATA_Folder";
    string NoteEventDataFolder = Application.streamingAssetsPath + "\\NOTEEVENTDATA_Folder";

    string NoteDataPath;
    string NoteEventDataPath;


    public EventManager eventManager;
    public Action EventCheck= delegate { }; //해당 이벤트의 경우 특수하게 이벤트 노트 생성기 스크립트 내의 액션 변수에 구독되어 있음
    public List<NoteInfoAll> EditNotes = new List<NoteInfoAll>();

    public List<EventInfoAll> EventNotes = new List<EventInfoAll>();
    //public List<NoteEventInfoPos> NoteEventList = new List<NoteEventInfoPos>();




    protected override void Awake()
    {
        //NoteEventList.RemoveAll(info => info.Event)
        base.Awake();
        if (!Directory.Exists(NoteEventDataFolder))
        {
            Directory.CreateDirectory(NoteEventDataFolder);
        }
        if (!Directory.Exists(NoteDataFolder))
        {
            Directory.CreateDirectory(NoteDataFolder);
        }


        NoteEventDataPath = Path.Combine(NoteEventDataFolder, GameManager.Instance.musicInfo.Music_Name + "_EventData.txt");
        NoteDataPath = Path.Combine(NoteDataFolder, GameManager.Instance.musicInfo.Music_Name + "_NoteData.txt");
        if (eventManager != null)
        {
            EventCheck += eventManager.RefreshNoteEventMethod;
        }
        else
        {
            Debug.Log("없어요");
        }
    }


    public void ListNullCheck(GameObject Note = null) //해당 리스트 중에서 null이 존재하는지 확인하고 만약 존재한다면 해당 데이터는 제거함
    {
        //int i = EditNotes.Count - 1;

        EditNotes.RemoveAll(info => info.Note == null);
        EditNotes = EditNotes.OrderBy(N => N.notePos.xpos).ToList();
        //while (i >= 0)
        //{
        //    if (EditNotes[i].Note == null)
        //    {
        //        //Debug.Log(i + "이거 지워요");
        //        EditNotes.RemoveAt(i);
        //    }
        //    i--;
        //}

        EventNotes.RemoveAll(info => info.EventNote== Note);
        EventNotes = EventNotes.OrderBy(N => N.eventPos.SongTime).ToList();

        //int j = 0;
        //while(j < EventNotes.Count)
        //{
        //    //Debug.Log(EventNotes.Count);
        //    //Debug.Log(EventNotes[j].EventNote.gameObject.name);
        //    //if (EventNotes[j].EventNote == null)
        //    //{
        //    //    Debug.Log("이벤트 제거");
        //    //    EventNotes.RemoveAt(j);
        //    //}

        //    if(songtime == (EventNotes[j].SongTime))
        //    {
        //        //DataManager.Instance.ListNullCheck(hit[i].collider.GetComponent<Note>().SongTime);
        //        NoteEventList.RemoveAt(j);
        //    }


        //    j++;
        //}





    }

    public NoteInfoAll FindNoteData(GameObject Note)
    {

        foreach (var note in EditNotes)
        {
            if(note.Note == Note)
            {
                return note;
            }
        }

        return null;
    }

    public void SaveNote()
    {
        EditNotes = EditNotes.OrderBy(N => N.notePos.xpos).ToList();
        EventNotes = EventNotes.OrderBy(N => N.eventPos.SongTime).ToList();


        if (File.Exists(NoteDataPath))
        {
           // Debug.Log("파일 존재");
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
           // Debug.Log("파일 생성");
            FileStream fileStream = File.Create(NoteDataPath);
            StreamWriter fileWriter = new StreamWriter(fileStream);

            fileWriter.WriteLine(EditNotes.Count);

            foreach (NoteInfoAll np in EditNotes)
            {
                fileWriter.WriteLine($"{np.notePos.xpos / GameManager.Instance.speed} , {np.notePos.HeightValue}, {np.notePos.NoteType}, {np.notePos.LongNoteStartEndCheck}, {np.notePos.SongTime}, {np.notePos.EnemyType}");
            }
            fileWriter.Close();

        }

        if (File.Exists(NoteEventDataPath))
        {
            StreamWriter writer = File.CreateText(NoteEventDataPath);


            ListNullCheck();
            writer.WriteLine(EventNotes.Count);  

            foreach (var Ev in EventNotes)
            {

                writer.WriteLine($"{Ev.eventPos.xpos / GameManager.Instance.speed} , {Ev.eventPos.HeightValue}, {Ev.eventPos.EventType}, {Ev.eventPos.SongTime}");
            }
            writer.Close();
        }
        else
        {
            FileStream fileStream = File.Create(NoteEventDataPath);
            StreamWriter fileWriter = new StreamWriter(fileStream);

            fileWriter.WriteLine(EventNotes.Count);

            foreach (var Ev in EventNotes)
            {

                fileWriter.WriteLine($"{Ev.eventPos.xpos / GameManager.Instance.speed} , {Ev.eventPos.HeightValue}, {Ev.eventPos.EventType}, {Ev.eventPos.SongTime}");
            }
            fileWriter.Close();
        }

    }

    public void LoadNote()
    {
        //이미 생성되어 있으면 해당 데이터를 지우고 다시 깔아줘야 함

        StreamReader NoteParsing = new StreamReader(NoteDataPath);
        int NoteCount = Int32.Parse(NoteParsing.ReadLine());

        StreamReader EventNoteParsing = new StreamReader(NoteEventDataPath);
        int EventCount = Int32.Parse(EventNoteParsing.ReadLine());
        while (EventNotes.Count > 0)
        {
            foreach (Transform child in EditManager.Instance.barNote.EventNote.transform)
            {
                Destroy(child.gameObject);
            }
            EventNotes = new List<EventInfoAll>();
        }

        for (int i = 0; i < EventCount; i++)
        {
            float xpos; int height; int EventType; double SongTime;

            string LineText; //파싱한 노트 정보를 저장받을 문자열

            LineText = EventNoteParsing.ReadLine();

            string[] split_Text = LineText.Split(',');
            xpos = float.Parse(split_Text[0]);
            height = Int32.Parse(split_Text[1]);
            EventType = Int32.Parse(split_Text[2]);

            if (split_Text[3] != null)
            {
                SongTime = double.Parse(split_Text[3]);
            }
            else
                SongTime = 0;

            if (GameManager.Instance.state != GameState.Play_Mode)
            {
                EditManager.Instance.MakeEvent(xpos * GameManager.Instance.speed + EditManager.Instance.GetNPXpos(), height, EventType, SongTime);
            }
            else
            {
                EventInfoAll noteEvent = new EventInfoAll(new GameObject(), xpos, height, EventType, SongTime);
                EventNotes.Add(noteEvent);
                PlayManager.Instance.PlayScene_EventMaker(xpos * GameManager.Instance.speed, height, EventType, SongTime);
            }

        }


        //여기에서 로드할 때 싹다 초기화시켜줘야함
        while (EditNotes.Count > 0)
        {
            Destroy(EditNotes[EditNotes.Count - 1].Note);
            EditNotes.RemoveAt(EditNotes.Count - 1);
            //Debug.Log("노트 초기화 중입니다.");
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
                
                if(LongNoteStartEndCheck != 2 || NoteType < 100)//총 노트 체크 횟수 인데 임시로 적용한 버전이라서 수정 필요함
                {
                    PlayManager.Instance.AddNoteCount();
                }
                PlayManager.Instance.SetComboCount();
            }
        
        }

  



        NoteParsing.Close();
        EventNoteParsing.Close();


        Debug.Log("실행");
        EventCheck?.Invoke();

    }

    void SaveGameResult()
    {
        //Debug.Log(PlayManager.Instance);
    }

    public void DestroyEvent()
    {
        while (EventNotes.Count > 0)
        {
            Destroy(EventNotes[EventNotes.Count - 1].EventNote);
            //EventNotes.RemoveAt(EventNotes.Count - 1);
            EventNotes = new List<EventInfoAll>();
        }
    }
    public void DestroyEverything()
    {
        while (EventNotes.Count > 0)
        {
            foreach(Transform child in EditManager.Instance.barNote.EventNote.transform)
            {
                Destroy(child.gameObject);
            }
           
            EventNotes = new List<EventInfoAll>();
        }

        while (EditNotes.Count > 0)
        {
            foreach (Transform child in EditManager.Instance.barNote.RhythmNote.transform)
            {
                Destroy(child.gameObject);
            }
            EditNotes.RemoveAt(EditNotes.Count - 1);
            //Debug.Log("노트 초기화 중입니다.");
        }


    }


}
