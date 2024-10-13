using System.Collections;
using System.Collections.Generic;
using UnityEngine;










public class EditManager : Singleton<EditManager>
{
    public NoteEditOperatingState OperateEditState = NoteEditOperatingState.Mouse;

    //각종 에딧씬에서 필요한 기능들을 모아놓음 
    public BarNote barNote;


    [Header("일반 노트")]
    public GameObject NormalNote_Obj;
    public GameObject LongNote_Obj;

    [Header("특수 노트들")]
    public GameObject GhostNote_Obj;


    [Header("장애물")]
    public GameObject Obstacle_Obj;

    [Header("보스 노트")]
    public GameObject BossAppearNote_Obj;
    public GameObject BossDisappearNote_Obj;
    public GameObject BossDashNote_Obj;


    [Header("이벤트 생성 노트")]
    public GameObject EndEvent_Obj;
    public GameObject NoteOutSpawnEvent_Obj;
    public GameObject NoteOutSpawnReverseEvent_Obj;




    Queue<LongNoteScript> UnCompleteLongNoteQueue = new Queue<LongNoteScript>(); //아직 꼬리위치가 제대로 할당되지 않은 롱노트를 쉽게 관리하기 위해 만들어줌

    public const int UP = 3;
    public const int DOWN = -1;
    public const int MIDDLE = (UP + DOWN) / 2;
    public const int OBSTACLE_UP = 4;
    public const int OBSTACLE_DOWN = -2;

    public const int UP_OUTSIDE = 6;
    public const int DOWN_OUTSIDE = -4;





    public float GetNPXpos()
    {
        return barNote.transform.position.x;
    }

    public void MakeNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        //나중에 번호에 따른 수치 값 조정을 원활하게 할 수 있도록 따로 값으로 만들어놔야 할 것 같음

        switch (noteType)
        {
            case 0:
                Obstacle(xpos, height, noteType, LongNoteStartEndCheck, songtime);
                break;

            case 1:

                NormalNote(xpos, height, noteType, LongNoteStartEndCheck, songtime, enemyType);
                break;

            case 2:

                LongNote(xpos, height, noteType, LongNoteStartEndCheck, songtime);
                //Debug.Log("작동은 하나");
                break;

            case 3:
                GhostNote(xpos, height, noteType, LongNoteStartEndCheck, songtime);

                break;

            default:
                break;
        }



    }


    public void MakeEvent(float xpos, int height, int eventType, double songtime)
    {
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








    public void NormalNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        GameObject AddNote = Instantiate(NormalNote_Obj, EditManager.Instance.barNote.RhythmNote.transform);

        AddNote.transform.position = new Vector3(xpos, height/* SettingHeight(height)*/);
        AddNote.GetComponent<NormalNote>().SetNoteType(enemyType);


        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
        AddNote.GetComponent<Note>().SongTime = (double)RealXpos / GameManager.Instance.speed;

        //height 부분 나중에 바꿀거임
        DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / GameManager.Instance.speed, enemyType));


    }

    public void LongNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        LongNoteInternalMethod(xpos, height, noteType, LongNoteStartEndCheck, (double)xpos / 10);
    }

    void LongNoteInternalMethod(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
    {

        GameObject LongNote;
        if (LongNoteStartEndCheck == 1)
        {
            LongNote = Instantiate(LongNote_Obj, new Vector3(xpos, height/*SettingHeight(height)*/), Quaternion.identity, EditManager.Instance.barNote.RhythmNote.transform);
            UnCompleteLongNoteQueue.Enqueue(LongNote.GetComponent<LongNoteScript>());

            float RealXpos = LongNote.transform.position.x - EditManager.Instance.GetNPXpos();

            DataManager.Instance.EditNotes.Add(new NoteInfoAll(LongNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)LongNote.transform.localPosition.x / GameManager.Instance.speed));
        }
        else if (LongNoteStartEndCheck == 2)
        {
            Queue<LongNoteScript> newLongNoteQueue = new Queue<LongNoteScript>();
            foreach (var head in UnCompleteLongNoteQueue)
            {
                if (head.transform.position.y == height) //줄 번호가 1일 경우 2의 위치임
                {
                    head.Tail.transform.position = new Vector3(xpos, head.transform.position.y);

                    float RealXpos = head.Tail.transform.position.x - EditManager.Instance.GetNPXpos();

                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(head.Tail, RealXpos, height, noteType, LongNoteStartEndCheck, (double)(head.transform.position.x + head.Tail.transform.localPosition.x) / GameManager.Instance.speed));
                }
                else
                {
                    newLongNoteQueue.Enqueue(head); //해당 조건을 만족하지 않는 큐만 따로 추가함
                }

            }
            UnCompleteLongNoteQueue = newLongNoteQueue;

        }
    }
    public void GhostNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        GameObject AddNote = Instantiate(GhostNote_Obj, new Vector3(xpos, height), Quaternion.identity, EditManager.Instance.barNote.RhythmNote.transform);

        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

        DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / GameManager.Instance.speed));


    }


    //롱노트 내부 메서드 따로 분리함


    public void Obstacle(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        GameObject AddNote = Instantiate(Obstacle_Obj, new Vector3(xpos, height), Quaternion.identity, EditManager.Instance.barNote.RhythmNote.transform);

        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

        DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / GameManager.Instance.speed));
    }









    public void BossAppearNote(float xpos, int height, int eventType, double songtime)
    {

        GameObject AddEvent = Instantiate(BossAppearNote_Obj, new Vector3(xpos, MIDDLE), Quaternion.identity, EditManager.Instance.barNote.EventNote.transform);

        float RealXpos = AddEvent.transform.position.x - EditManager.Instance.GetNPXpos();

        DataManager.Instance.EventNotes.Add(new EventInfoAll(AddEvent, RealXpos, height, eventType, (double)AddEvent.transform.localPosition.x / GameManager.Instance.speed));

        AddEvent.GetComponent<Note>().SetSongTime(songtime);
    }

    public void BossDisappearNote(float xpos, int height, int eventType, double songtime)
    {

        GameObject AddEvent = Instantiate(BossDisappearNote_Obj, new Vector3(xpos, MIDDLE), Quaternion.identity, EditManager.Instance.barNote.EventNote.transform);

        float RealXpos = AddEvent.transform.position.x - EditManager.Instance.GetNPXpos();

        DataManager.Instance.EventNotes.Add(new EventInfoAll(AddEvent, RealXpos, height, eventType, (double)AddEvent.transform.localPosition.x / GameManager.Instance.speed));

        AddEvent.GetComponent<Note>().SetSongTime(songtime);
    }

    public void BossDashNote(float xpos, int height, int eventType, double songtime)
    {
        GameObject AddEvent = Instantiate(BossDashNote_Obj, new Vector3(xpos, MIDDLE), Quaternion.identity, EditManager.Instance.barNote.EventNote.transform);

        float RealXpos = AddEvent.transform.position.x - EditManager.Instance.GetNPXpos();

        DataManager.Instance.EventNotes.Add(new EventInfoAll(AddEvent, RealXpos, height, eventType, (double)AddEvent.transform.localPosition.x / GameManager.Instance.speed));

        AddEvent.GetComponent<Note>().SetSongTime(songtime);
    }




    public void EndEventNote(float xpos, int height, int eventType, double songtime)
    {
        GameObject AddEvent = Instantiate(EndEvent_Obj, new Vector3(xpos, MIDDLE), Quaternion.identity, EditManager.Instance.barNote.EventNote.transform);

        float RealXpos = AddEvent.transform.position.x - EditManager.Instance.GetNPXpos();

        DataManager.Instance.EventNotes.Add(new EventInfoAll(AddEvent, RealXpos, height, eventType, (double)AddEvent.transform.localPosition.x / GameManager.Instance.speed));

        AddEvent.GetComponent<Note>().SetSongTime(songtime);
    }

    public void NoteSpawnOutsideEvent(float xpos, int height, int eventType, double songtime)
    {
        GameObject AddEvent = Instantiate(NoteOutSpawnEvent_Obj, new Vector3(xpos, MIDDLE), Quaternion.identity, EditManager.Instance.barNote.EventNote.transform);

        float RealXpos = AddEvent.transform.position.x - EditManager.Instance.GetNPXpos();

        DataManager.Instance.EventNotes.Add(new EventInfoAll(AddEvent, RealXpos, height, eventType, (double)AddEvent.transform.localPosition.x / GameManager.Instance.speed));

        AddEvent.GetComponent<Note>().SetSongTime(songtime);
    }

    public void NoteSpawnOutsideReverseEvent(float xpos, int height, int eventType, double songtime)
    {
        GameObject AddEvent = Instantiate(NoteOutSpawnReverseEvent_Obj, new Vector3(xpos, MIDDLE), Quaternion.identity, EditManager.Instance.barNote.EventNote.transform);

        float RealXpos = AddEvent.transform.position.x - EditManager.Instance.GetNPXpos();

        DataManager.Instance.EventNotes.Add(new EventInfoAll(AddEvent, RealXpos, height, eventType, (double)AddEvent.transform.localPosition.x / GameManager.Instance.speed));

        AddEvent.GetComponent<Note>().SetSongTime(songtime);
    }







    private int SettingHeight(int height)
    {
        switch (height)
        {

            //장애물 높이
            case -1:
                return OBSTACLE_UP;

            case -2:
                return OBSTACLE_DOWN;


            //기본 노트 높이
            case 1:
                return UP;


            case 2:
                return DOWN;



            //바깥쪽 노트 높이
            case 10:
                return UP_OUTSIDE;


            case 20:
                return DOWN_OUTSIDE;



            default:
                return 0;
        }
    }





    public void ActivateBeatLine()
    {
        barNote.ActiveNote();
    }



}
