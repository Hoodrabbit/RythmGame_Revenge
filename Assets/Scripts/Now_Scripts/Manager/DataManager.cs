using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;



public class NoteInfoPos
{
    public float xpos; // ��Ʈ�� X ��(������ �� �����̱� ������ ���߿� ���� ���� �������� �ش� �����Ϳ� �߰��� �������̳� �ӵ��� ���� ��ġ�� ������ �� ����
    public int HeightValue; //��Ʈ ���� ��ȣ�� ����޾Ƽ� ���߿� �ҷ��� ���� �� ��ȣ�� ���� Y���� Ư�� ������ �Ҵ��Ŵ
    //�ű��߰�
    public int NoteType; //��Ʈ�� ������ ���� ������ ������� ����   (-1 : ��ֹ� ��Ʈ, 1 : �⺻ , 2 : �� ��Ʈ, 3 : ���ɳ�Ʈ Ư�� ��Ʈ�� �� ���� ���⿡�� �� �߰� �� �� ����)
    public int LongNoteStartEndCheck; //�ճ�Ʈ�� ���̸� üũ���� ����(��Ȯ���� ���۰� ��) (�ճ�Ʈ ������ �ƴ� ��� ���� 0�̰� �ճ�Ʈ�� �� 1�� ���� 2�� ���� üũ����)

    public double SongTime; //���� ��Ʈ�� �ش��ϴ� �뷡�� �ð�

    public int EnemyType = 0; // ���� ���� ����(Ư�� ���̽�)�� ���ؼ� ������ ���� ���·δ� ������ �Ұ����� ���õǱ� ������ ���⸦ �����Ͽ� ��Ʈ�� �ľ� ��



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
/// 1 : Note ���� 2 : ��ġ  3 : ����   4 : �ճ�Ʈ ���� ����(���� 1, �� 2)   5 : �뷡 �ð�   6 : ���� ���� ����(0�̸� �⺻ 1, 2���� ����)
/// </summary>
public class NoteInfoAll //���� ��Ʈ�� ���� ������ ��� �� Ŭ����
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
    public float xpos; // ��Ʈ�� X ��(������ �� �����̱� ������ ���߿� ���� ���� �������� �ش� �����Ϳ� �߰��� �������̳� �ӵ��� ���� ��ġ�� ������ �� ����
    public int HeightValue; //��Ʈ ���� ��ȣ�� ����޾Ƽ� ���߿� �ҷ��� ���� �� ��ȣ�� ���� Y���� Ư�� ������ �Ҵ��Ŵ

    public int EventType; //�̺�Ʈ ���� üũ

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
    //���� ������ ���� ��ũ��Ʈ ���� �Ŵ����� ��ũ���ͺ��� ���� �ּҸ� �̾ƿ�
    string NoteDataFolder = Application.streamingAssetsPath + "\\NOTEDATA_Folder";
    string NoteEventDataFolder = Application.streamingAssetsPath + "\\NOTEEVENTDATA_Folder";

    string NoteDataPath;
    string NoteEventDataPath;


    public EventManager eventManager;
    public Action EventCheck= delegate { }; //�ش� �̺�Ʈ�� ��� Ư���ϰ� �̺�Ʈ ��Ʈ ������ ��ũ��Ʈ ���� �׼� ������ �����Ǿ� ����
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
            Debug.Log("�����");
        }
    }


    public void ListNullCheck(GameObject Note = null) //�ش� ����Ʈ �߿��� null�� �����ϴ��� Ȯ���ϰ� ���� �����Ѵٸ� �ش� �����ʹ� ������
    {
        //int i = EditNotes.Count - 1;

        EditNotes.RemoveAll(info => info.Note == null);
        EditNotes = EditNotes.OrderBy(N => N.notePos.xpos).ToList();
        //while (i >= 0)
        //{
        //    if (EditNotes[i].Note == null)
        //    {
        //        //Debug.Log(i + "�̰� ������");
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
        //    //    Debug.Log("�̺�Ʈ ����");
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
           // Debug.Log("���� ����");
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
           // Debug.Log("���� ����");
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
        //�̹� �����Ǿ� ������ �ش� �����͸� ����� �ٽ� ������ ��

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

            string LineText; //�Ľ��� ��Ʈ ������ ������� ���ڿ�

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


        //���⿡�� �ε��� �� �ϴ� �ʱ�ȭ���������
        while (EditNotes.Count > 0)
        {
            Destroy(EditNotes[EditNotes.Count - 1].Note);
            EditNotes.RemoveAt(EditNotes.Count - 1);
            //Debug.Log("��Ʈ �ʱ�ȭ ���Դϴ�.");
        }

        for (int i = 0; i < NoteCount; i++)
        {
            float xpos; int height; int NoteType; int LongNoteStartEndCheck; double SongTime; int enemyType;
            
            string LineText; //�Ľ��� ��Ʈ ������ ������� ���ڿ�
            
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
                
                if(LongNoteStartEndCheck != 2 || NoteType < 100)//�� ��Ʈ üũ Ƚ�� �ε� �ӽ÷� ������ �����̶� ���� �ʿ���
                {
                    PlayManager.Instance.AddNoteCount();
                }
                PlayManager.Instance.SetComboCount();
            }
        
        }

  



        NoteParsing.Close();
        EventNoteParsing.Close();


        Debug.Log("����");
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
            //Debug.Log("��Ʈ �ʱ�ȭ ���Դϴ�.");
        }


    }


}
