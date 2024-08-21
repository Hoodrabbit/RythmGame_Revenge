using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditManager : Singleton<EditManager>
{
    //각종 에딧씬에서 필요한 기능들을 모아놓음 
    public BarNote NoteParent;


    [Header("일반 노트")]
    public GameObject NormalNote_Obj;
    public GameObject LongNote_Obj;

    [Header("특수 노트들")]
    public GameObject ExpandLine_Obj;
    public GameObject GhostNote_Obj;



    Queue<LongNoteScript> UnCompleteLongNoteQueue = new Queue<LongNoteScript>(); //아직 꼬리위치가 제대로 할당되지 않은 롱노트를 쉽게 관리하기 위해 만들어줌






    public float GetNPXpos()
    {
        return NoteParent.transform.position.x;
    }

    public void MakeNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType=0)
    {
        //나중에 번호에 따른 수치 값 조정을 원활하게 할 수 있도록 따로 값으로 만들어놔야 할 것 같음

        switch (noteType)
        {
            case 0:
                //ExpandLine(xpos, height, noteType, LongNoteStartEndCheck, songtime);
                break;

            case 1:

                NormalNote(xpos, height, noteType, LongNoteStartEndCheck, songtime,enemyType);
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

    public void NormalNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        GameObject AddNote = Instantiate(NormalNote_Obj, EditManager.Instance.NoteParent.RhythmNote.transform);


        if (height == 1)
        {
            AddNote.transform.position = new Vector3(xpos, 2);
            AddNote.GetComponent<Note>().SetNoteType(enemyType);


            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


            //height 부분 나중에 바꿀거임
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / GameManager.Instance.speed, enemyType));
        }
        else if (height == 2)
        {
            AddNote.transform.position = new Vector3(xpos, -2);
            AddNote.GetComponent<Note>().SetNoteType(enemyType);
            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

            //height 부분 나중에 바꿀거임
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / GameManager.Instance.speed, enemyType));


        }
        //else if (height == 3)
        //{
        //    AddNote.transform.position = new Vector3(xpos, 6);

        //    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
        //    AddNote.GetComponent<Note>().SetNoteType(enemyType);
        //    //height 부분 나중에 바꿀거임
        //    DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / 10));
        //}
        //else
        //{
        //    AddNote.transform.position = new Vector3(xpos, -6);
        //    AddNote.GetComponent<Note>().SetNoteType(enemyType);
        //    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

        //    //height 부분 나중에 바꿀거임
        //    DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / 10));
        //}
    }

    public void LongNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime,int enemyType= 0)
    {
        LongNoteInternalMethod(xpos, height, noteType, LongNoteStartEndCheck, (double)xpos / 10);
    }

    //public void ExpandLine(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
    //{
    //    GameObject AddNote = Instantiate(ExpandLine_Obj, new Vector3(xpos, 0), Quaternion.identity, EditManager.Instance.NoteParent.transform);

    //    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

    //    //height 부분 나중에 바꿀거임
    //    DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / 10));
    //}
    public void GhostNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
        {
            if (height == 1)
            {
                GameObject AddNote = Instantiate(GhostNote_Obj, new Vector3(xpos, 2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

                float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


                //height 부분 나중에 바꿀거임
                DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / GameManager.Instance.speed));
            }
            else if (height == 2)
            {
                GameObject AddNote = Instantiate(GhostNote_Obj, new Vector3(xpos, -2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

                float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

                //height 부분 나중에 바꿀거임
                DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / GameManager.Instance.speed));


            }
            else if (height == 3)
            {
                GameObject AddNote = Instantiate(GhostNote_Obj, new Vector3(xpos, 6), Quaternion.identity, EditManager.Instance.NoteParent.transform);

                float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

                //height 부분 나중에 바꿀거임
                DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / GameManager.Instance.speed));
            }
            else
            {
                GameObject AddNote = Instantiate(GhostNote_Obj, new Vector3(xpos, -6), Quaternion.identity, EditManager.Instance.NoteParent.transform);

                float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

                //height 부분 나중에 바꿀거임
                DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / GameManager.Instance.speed));
            }
        }


        //롱노트 내부 메서드 따로 분리함
    void LongNoteInternalMethod(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
        {
            int ypos = 0;
            switch (height)
            {
                case 1:
                    ypos = 2;
                    break;

                case 2:
                    ypos = -2;
                    break;
            }

            GameObject LongNote;
            if (LongNoteStartEndCheck == 1)
            {
                LongNote = Instantiate(LongNote_Obj, new Vector3(xpos, ypos), Quaternion.identity, EditManager.Instance.NoteParent.transform);
                UnCompleteLongNoteQueue.Enqueue(LongNote.GetComponent<LongNoteScript>());

                float RealXpos = LongNote.transform.position.x - EditManager.Instance.GetNPXpos();

                DataManager.Instance.EditNotes.Add(new NoteInfoAll(LongNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)LongNote.transform.localPosition.x / GameManager.Instance.speed));
            }
            else if (LongNoteStartEndCheck == 2)
            {
                Queue<LongNoteScript> newLongNoteQueue = new Queue<LongNoteScript>();
                foreach (var head in UnCompleteLongNoteQueue)
                {
                    if (head.transform.position.y == ypos) //줄 번호가 1일 경우 2의 위치임
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


    public void ChangeScreenSize()
        {
            NoteParent.ExpandScreen();
        }

    public void ActivateBeatLine()
    {
        NoteParent.ActiveNote();
    }


    
}
