using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이씬을 관리하는 싱글톤
public class PlayManager : Singleton<PlayManager>
{
    public List<GameObject> NoteTypes; //일단 순서대로 일반 롱, 특수 노트 ... 이런식으로 갈듯
    public List<GameObject> Notes;
    public NoteInfoPos NotePos;

    public ComboSystem combosystem;


    public GameObject Note_Parent;

    public GameObject[] HidingJudgementObj;

    Queue<LongNoteScript> UnCompleteLongNoteQueue = new Queue<LongNoteScript>();


    public void PlayScene_NoteMaker(float xpos, int heightnum, int noteType, int LongNoteStartEndCheck)
    {

        switch (noteType)
        {
            case 0:
                ExpandLine(xpos, heightnum, noteType, LongNoteStartEndCheck);
                break;

            case 1:

                NormalNote(xpos, heightnum, noteType, LongNoteStartEndCheck);
                break;

            case 2:

               LongNote(xpos, heightnum, noteType, LongNoteStartEndCheck);
                //Debug.Log("작동은 하나");
                break;

            case 3:


                break;



            default:
                break;
        }


        //일단 다시 만들어야 하기 때문에 주석처리함

        ////Notes.Add(Instantiate(xpos));//내일 할거임

        //NotePos = new NoteInfoPos(xpos + 1*3 * 6-5, heightnum); 

        //if(NotePos.HeightValue ==1)
        //{
        //    Notes.Add(Instantiate(Note, new Vector3(NotePos.xpos, 2), Quaternion.identity));
        //}
        //else
        //{
        //    Notes.Add(Instantiate(Note, new Vector3(NotePos.xpos, -2), Quaternion.identity));
        //}



    }

    public void HitNote()
    {
        combosystem.HitNote();
    }

    public void MissNote()
    {
        combosystem.MissNote();
    }


    void NormalNote(float xpos, int height, int noteType, int LongNoteStartEndCheck)
    {
        if (height == 1)
        {
            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType , LongNoteStartEndCheck);
            Notes.Add(Instantiate(NoteTypes[0], new Vector3(NotePos.xpos, 2), Quaternion.identity, Note_Parent.transform));

            //float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


            //height 부분 나중에 바꿀거임
            //Notes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck));
        }
        else if (height == 2)
        {
            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck);
            Notes.Add(Instantiate(NoteTypes[0], new Vector3(NotePos.xpos, -2), Quaternion.identity, Note_Parent.transform));

            //float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

            ////height 부분 나중에 바꿀거임
            //DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck));


        }
        else if (height == 3)
        {
            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck);
            Notes.Add(Instantiate(NoteTypes[0], new Vector3(NotePos.xpos, 6), Quaternion.identity, Note_Parent.transform));
        }
        else
        {
            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck);
            Notes.Add(Instantiate(NoteTypes[0], new Vector3(NotePos.xpos, -6), Quaternion.identity, Note_Parent.transform));
        }



    }

    void LongNote(float xpos, int height, int noteType, int LongNoteStartEndCheck)
    {
        LongNoteInternalMethod(xpos, height, noteType, LongNoteStartEndCheck);
    }

    //롱노트 내부 메서드 따로 분리함
    void LongNoteInternalMethod(float xpos, int height, int noteType, int LongNoteStartEndCheck)
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
            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck);
            LongNote = Instantiate(NoteTypes[1], new Vector3(NotePos.xpos, ypos), Quaternion.identity, Note_Parent.transform);

            Notes.Add(LongNote);
            
            UnCompleteLongNoteQueue.Enqueue(LongNote.GetComponent<LongNoteScript>());

           // float RealXpos = LongNote.transform.position.x - EditManager.Instance.GetNPXpos();

          //  DataManager.Instance.EditNotes.Add(new NoteInfoAll(LongNote, RealXpos, height, noteType, LongNoteStartEndCheck));
        }
        else if (LongNoteStartEndCheck == 2)
        {
            Queue<LongNoteScript> newLongNoteQueue = new Queue<LongNoteScript>();
            foreach (var head in UnCompleteLongNoteQueue)
            {
                if (head.transform.position.y == ypos) //줄 번호가 1일 경우 2의 위치임
                {
                    NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck);
                    head.Tail.transform.position = new Vector3(NotePos.xpos, head.transform.position.y);
                    Debug.Log(xpos);
                    //float RealXpos = head.Tail.transform.position.x - EditManager.Instance.GetNPXpos();

                    Notes.Add(head.Tail);
                    //DataManager.Instance.EditNotes.Add(new NoteInfoAll(head.Tail, RealXpos, height, noteType, LongNoteStartEndCheck));
                }
                else
                {
                    newLongNoteQueue.Enqueue(head); //해당 조건을 만족하지 않는 큐만 따로 추가함
                }

            }
            UnCompleteLongNoteQueue = newLongNoteQueue;

        }
    }

    public void ExpandLine(float xpos, int height, int noteType, int LongNoteStartEndCheck)
    {
            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck);

            Notes.Add(Instantiate(NoteTypes[2], new Vector3(NotePos.xpos, height), Quaternion.identity, Note_Parent.transform));

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


}





/*
 public void MakeNote(float xpos, int height)
    {
        //나중에 번호에 따른 수치 값 조정을 원활하게 할 수 있도록 따로 값으로 만들어놔야 할 것 같음
        if (height == 1)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, 2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


            //height 부분 나중에 바꿀거임
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height));
        }
        else if (height == 2)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, -2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

            //height 부분 나중에 바꿀거임
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height));


        }

    }
 
 */