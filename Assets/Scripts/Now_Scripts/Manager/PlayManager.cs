using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayManager : Singleton<PlayManager>
{

    [Header("노트 종류")]
    public List<GameObject> NoteTypes;

    [Header("이벤트 종류")]
    public List<GameObject> EventTypes;





    public List<GameObject> Notes;
    //현재 노트 필요한지 조금 모호함 확인해보고 필요없으면 빼야됨

    public NoteInfoPos NotePos;

    public ComboSystem combosystem;
    public ScoreSystem scoresystem;


    int NoteCount_Now =0;

    public const int UP = 5;
    public const int DOWN = -1;
    const int MIDDLE = (UP + DOWN) / 2;
    public const int OBSTACLE_UP = 6;
    const int OBSTACLE_DOWN = -2;

    public const int UP_OUTSIDE = 12;
    public const int DOWN_OUTSIDE = -8;



    public GameObject Note_Parent;

    public GameObject[] HidingJudgementObj;

    Queue<LongNoteScript> UnCompleteLongNoteQueue = new Queue<LongNoteScript>();


    public void PlayScene_NoteMaker(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0 )
    {
        xpos += GameManager.Instance.OffsetValue;
        switch (noteType)
        {
            case 0:
                MakeObstacle(xpos, height, noteType, LongNoteStartEndCheck, songtime);
                break;


            case 1:

                NormalNote(xpos, height, noteType, LongNoteStartEndCheck, songtime, enemyType);
                break;

            case 2:

               LongNote(xpos, height, noteType, LongNoteStartEndCheck, songtime);

                break;

            case 3:
                GhostNote(xpos, height, noteType, LongNoteStartEndCheck, songtime);

                break;




            default:
                break;
        }



    }

    public void PlayScene_EventMaker(float xpos, int height, int eventType, double songtime)
    {

        Debug.Log("실행됨 : " + eventType); 

        switch (eventType)
        {
            

            case 1:
                Debug.Log("노트 생성 바깥쪽");
                NoteSpawnOutsideEvent(xpos, height, eventType, songtime);
                break;

            case 2:
                Debug.Log("노트 생성 바깥쪽 역순");
                NoteSpawnOutsideReverseEvent(xpos, height, eventType, songtime);

                break;


            case 3:
                Debug.Log("이벤트 종료");
                EndEventNote(xpos, height, eventType, songtime);
                break;


            case 100:
                Debug.Log("출현노트 ");
                BossAppearNote(xpos, height, eventType, songtime);
                break;

            case 101:
                Debug.Log("퇴장노트 ");
                BossDisappearNote(xpos, height, eventType, songtime);
                break;
            case 102:
                Debug.Log("돌진노트 ");
                BossDashNote(xpos, height, eventType, songtime);
                break;




        }

    }



    public void HitNote()
    {
        combosystem.HitNote();
        scoresystem.IncreaseScore();
    }

    public void MissNote()
    {
        combosystem.MissNote();
        PlayerController.Instance.TakeHPMethod(100);
    }

    private int SettingHeight(int height)
    {
        switch (height)
        {
            case -1:
                return OBSTACLE_UP;

            case -2:
                return OBSTACLE_DOWN;

            case 1:
                return UP;


            case 2:
                return DOWN;
            default:
                return 0;
        }
    }


    void NormalNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType)
    {
        GameObject Note_Instantiate;



        NoteInfoPos NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck, songtime);
        Note_Instantiate = Instantiate(NoteTypes[0], new Vector3(NotePos.xpos, height), Quaternion.identity, PlayManager.Instance.Note_Parent.transform);
        Note_Instantiate.GetComponent<NormalNote>().SetSongTime(songtime);
        Note_Instantiate.GetComponent<NormalNote>().SetNoteType(enemyType);
        PlayManager.Instance.Notes.Add(Note_Instantiate);

    }

    void LongNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
    {
        LongNoteInternalMethod(xpos, height, noteType, LongNoteStartEndCheck, songtime);
    }

    void LongNoteInternalMethod(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
    {


        GameObject LongNote;
        if (LongNoteStartEndCheck == 1)
        {
            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck, songtime);
            LongNote = Instantiate(NoteTypes[1], new Vector3(NotePos.xpos, height), Quaternion.identity, PlayManager.Instance.Note_Parent.transform);
            LongNote.GetComponent<Note>().SetSongTime(songtime);
            // Debug.Log(songtime + " ���ʴ��ΰ���");
            Notes.Add(LongNote);

            UnCompleteLongNoteQueue.Enqueue(LongNote.GetComponent<LongNoteScript>());
        }
        else if (LongNoteStartEndCheck == 2)
        {
            Queue<LongNoteScript> newLongNoteQueue = new Queue<LongNoteScript>();
            foreach (var head in UnCompleteLongNoteQueue)
            {
                if (head.transform.position.y == height) //�� ��ȣ�� 1�� ��� 2�� ��ġ��
                {
                    NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck, songtime);
                    head.Tail.transform.position = new Vector3(NotePos.xpos, head.transform.position.y);

                    Notes.Add(head.Tail);
                }
                else
                {
                    newLongNoteQueue.Enqueue(head);
                }

            }
            UnCompleteLongNoteQueue = newLongNoteQueue;

        }
    }

    public void GhostNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
    {
        GameObject Note_Instantiate;

        NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck, songtime);
        Note_Instantiate = Instantiate(NoteTypes[2], new Vector3(NotePos.xpos, height), Quaternion.identity, Note_Parent.transform);

        Note_Instantiate.GetComponent<Note>().SetSongTime(songtime);

        Notes.Add(Note_Instantiate);

    }

    void MakeObstacle(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
    {
        GameObject Note_Instantiate;

        NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck, songtime);
        Note_Instantiate = Instantiate(NoteTypes[3], new Vector3(NotePos.xpos, height), Quaternion.identity, Note_Parent.transform);

        Note_Instantiate.GetComponent<Note>().SetSongTime(songtime);

        Notes.Add(Note_Instantiate);
    }







    public void BossAppearNote(float xpos, int height, int eventType, double songtime, int enemyType = 0)
    {
        GameObject EventNote;

        NoteEventInfoPos EventPos  = new NoteEventInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, MIDDLE, eventType, songtime);

        EventNote = Instantiate(EventTypes[0], new Vector3(EventPos.xpos, MIDDLE), Quaternion.identity, Note_Parent.transform);

        EventNote.GetComponent<Note>().SetSongTime(songtime);
        
    }

    public void BossDisappearNote(float xpos, int height, int eventType, double songtime, int enemyType = 0)
    {
        GameObject EventNote;

        NoteEventInfoPos EventPos = new NoteEventInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, MIDDLE, eventType, songtime);

        EventNote = Instantiate(EventTypes[1], new Vector3(EventPos.xpos, MIDDLE), Quaternion.identity, Note_Parent.transform);

        EventNote.GetComponent<Note>().SetSongTime(songtime);

        //DataManager.Instance.EventNotes.Add(new EventInfoAll(EventNote, EventNote.transform.position.x, height, eventType, songtime));

        //GameObject Note_Instantiate;

        ////Debug.Log("BossDisppearNote Xpos : " + xpos);

        //NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, MIDDLE, noteType, LongNoteStartEndCheck, songtime);
        //Note_Instantiate = Instantiate(NoteTypes[5], new Vector3(NotePos.xpos, MIDDLE), Quaternion.identity, Note_Parent.transform);

        //Note_Instantiate.GetComponent<Note>().SetSongTime(songtime);

        //Notes.Add(Note_Instantiate);

    }


    public void EndEventNote(float xpos, int height, int eventType, double songtime)
    {
        GameObject EventNote;

        NoteEventInfoPos EventPos = new NoteEventInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, MIDDLE, eventType, songtime);

        EventNote = Instantiate(EventTypes[2], new Vector3(EventPos.xpos, MIDDLE), Quaternion.identity, Note_Parent.transform);

        EventNote.GetComponent<Note>().SetSongTime(songtime);

        DataManager.Instance.EventNotes.Add(new EventInfoAll(EventNote, EventNote.transform.position.x, height, eventType, songtime));

    }

    public void NoteSpawnOutsideEvent(float xpos, int height, int eventType, double songtime)
    {
        GameObject EventNote;

        NoteEventInfoPos EventPos = new NoteEventInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, MIDDLE, eventType, songtime);

        EventNote = Instantiate(EventTypes[3], new Vector3(EventPos.xpos, MIDDLE), Quaternion.identity, Note_Parent.transform);

        EventNote.GetComponent<Note>().SetSongTime(songtime);

        DataManager.Instance.EventNotes.Add(new EventInfoAll(EventNote, EventNote.transform.position.x, height, eventType, songtime));
    }

    public void NoteSpawnOutsideReverseEvent(float xpos, int height, int eventType, double songtime)
    {
        GameObject EventNote;

        NoteEventInfoPos EventPos = new NoteEventInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, MIDDLE, eventType, songtime);

        EventNote = Instantiate(EventTypes[4], new Vector3(EventPos.xpos, MIDDLE), Quaternion.identity, Note_Parent.transform);

        EventNote.GetComponent<Note>().SetSongTime(songtime);

       DataManager.Instance.EventNotes.Add(new EventInfoAll(EventNote, EventNote.transform.position.x, height, eventType, songtime));
    }

    public void BossDashNote(float xpos, int height, int eventType, double songtime)
    {
        GameObject EventNote;

        NoteEventInfoPos EventPos = new NoteEventInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, MIDDLE, eventType, songtime);


        EventNote = Instantiate(EventTypes[5], new Vector3(xpos, MIDDLE), Quaternion.identity, Note_Parent.transform);

        EventNote.GetComponent<Note>().SetSongTime(songtime);
    }








    public void ControlHidingJudjement()
    {
        foreach (var obj in HidingJudgementObj)
        {
            if(obj.activeSelf == false)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }

    public float SetLine()
    {
        //Debug.Log(GameManager.Instance.MainAudio.time+ "     ,      "+GameManager.Instance.MainAudio.clip.length);
        return Mathf.Clamp(GameManager.Instance.MainAudio.time / GameManager.Instance.MainAudio.clip.length, 0f, 1f);
    }


    public void AddNoteCount()
    {
        NoteCount_Now += 1;
    }






    public int GetCombo()
    {
        return combosystem.Combo;
    }
    public int GetScore()
    {
        return scoresystem.Score;
    }

    public void SetComboCount()
    {
        combosystem.SetFullNoteCount(NoteCount_Now);
    }


}