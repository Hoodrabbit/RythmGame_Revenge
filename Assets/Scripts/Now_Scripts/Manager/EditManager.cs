using System.Collections;
using System.Collections.Generic;
using UnityEngine;










public class EditManager : Singleton<EditManager>
{
    public NoteEditOperatingState OperateEditState = NoteEditOperatingState.Mouse;

    //���� ���������� �ʿ��� ��ɵ��� ��Ƴ��� 
    public BarNote barNote;


    [Header("�Ϲ� ��Ʈ")]
    public GameObject NormalNote_Obj;
    public GameObject LongNote_Obj;

    [Header("Ư�� ��Ʈ��")]
    public GameObject GhostNote_Obj;


    [Header("��ֹ�")]
    public GameObject Obstacle_Obj;

    [Header("���� ��Ʈ")]
    public GameObject BossAppearNote_Obj;
    public GameObject BossDisappearNote_Obj;
    public GameObject BossDashNote_Obj;


    [Header("�̺�Ʈ ���� ��Ʈ")]
    public GameObject EndEvent_Obj;
    public GameObject NoteOutSpawnEvent_Obj;
    public GameObject NoteOutSpawnReverseEvent_Obj;




    Queue<LongNoteScript> UnCompleteLongNoteQueue = new Queue<LongNoteScript>(); //���� ������ġ�� ����� �Ҵ���� ���� �ճ�Ʈ�� ���� �����ϱ� ���� �������

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
        //���߿� ��ȣ�� ���� ��ġ �� ������ ��Ȱ�ϰ� �� �� �ֵ��� ���� ������ �������� �� �� ����

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
                //Debug.Log("�۵��� �ϳ�");
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
                Debug.Log("��Ʈ ���� �ٱ���");
                NoteSpawnOutsideEvent(xpos, height, eventType, songtime);
                break;

            case 2:
                Debug.Log("��Ʈ ���� �ٱ��� ����");
                NoteSpawnOutsideReverseEvent(xpos, height, eventType, songtime);
                break;

            case 3:
                Debug.Log("�̺�Ʈ ����");
                EndEventNote(xpos, height, eventType, songtime);
                break;

            case 100:
                Debug.Log("������Ʈ ");
                BossAppearNote(xpos, height, eventType, songtime);
                break;

            case 101:
                Debug.Log("�����Ʈ ");
                BossDisappearNote(xpos, height, eventType, songtime);
                break;
            case 102:
                Debug.Log("������Ʈ ");
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

        //height �κ� ���߿� �ٲܰ���
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
                if (head.transform.position.y == height) //�� ��ȣ�� 1�� ��� 2�� ��ġ��
                {
                    head.Tail.transform.position = new Vector3(xpos, head.transform.position.y);

                    float RealXpos = head.Tail.transform.position.x - EditManager.Instance.GetNPXpos();

                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(head.Tail, RealXpos, height, noteType, LongNoteStartEndCheck, (double)(head.transform.position.x + head.Tail.transform.localPosition.x) / GameManager.Instance.speed));
                }
                else
                {
                    newLongNoteQueue.Enqueue(head); //�ش� ������ �������� �ʴ� ť�� ���� �߰���
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


    //�ճ�Ʈ ���� �޼��� ���� �и���


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

            //��ֹ� ����
            case -1:
                return OBSTACLE_UP;

            case -2:
                return OBSTACLE_DOWN;


            //�⺻ ��Ʈ ����
            case 1:
                return UP;


            case 2:
                return DOWN;



            //�ٱ��� ��Ʈ ����
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
