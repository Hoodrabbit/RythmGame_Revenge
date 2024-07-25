using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditManager : Singleton<EditManager>
{
    //각종 에딧씬에서 필요한 기능들을 모아놓음 
    public BarNote NoteParent;

    public GameObject NormalNote_Obj;
    public GameObject LongNote_Obj;

    Queue<LongNoteScript> UnCompleteLongNoteQueue = new Queue<LongNoteScript>(); //아직 꼬리위치가 제대로 할당되지 않은 롱노트를 쉽게 관리하기 위해 만들어줌


    public float GetNPXpos()
    {
        return NoteParent.transform.position.x;
    }

    public void MakeNote(float xpos, int height, int noteType, int LongNoteStartEndCheck)
    {
        //나중에 번호에 따른 수치 값 조정을 원활하게 할 수 있도록 따로 값으로 만들어놔야 할 것 같음

        switch (noteType)
        {
            case 1:

                NormalNote(xpos, height, noteType, LongNoteStartEndCheck);
                break;

            case 2:

                LongNote(xpos, height, noteType, LongNoteStartEndCheck);
                //Debug.Log("작동은 하나");
                break;

            case 3:
                

                break;



            default:
                break;
        }

        

    }

    public void NormalNote(float xpos, int height, int noteType, int LongNoteStartEndCheck)
    {
        if (height == 1)
        {
            GameObject AddNote = Instantiate(NormalNote_Obj, new Vector3(xpos, 2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


            //height 부분 나중에 바꿀거임
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck));
        }
        else if (height == 2)
        {
            GameObject AddNote = Instantiate(NormalNote_Obj, new Vector3(xpos, -2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

            //height 부분 나중에 바꿀거임
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck));


        }
    }

    public void LongNote(float xpos, int height, int noteType, int LongNoteStartEndCheck)
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
            LongNote = Instantiate(LongNote_Obj, new Vector3(xpos, ypos), Quaternion.identity, EditManager.Instance.NoteParent.transform);
            UnCompleteLongNoteQueue.Enqueue(LongNote.GetComponent<LongNoteScript>());

            float RealXpos = LongNote.transform.position.x - EditManager.Instance.GetNPXpos();

            DataManager.Instance.EditNotes.Add(new NoteInfoAll(LongNote, RealXpos, height, noteType, LongNoteStartEndCheck));
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

                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(head.Tail, RealXpos, height, noteType, LongNoteStartEndCheck));
                }
                else
                {
                    newLongNoteQueue.Enqueue(head); //해당 조건을 만족하지 않는 큐만 따로 추가함
                }

            }
            UnCompleteLongNoteQueue = newLongNoteQueue;

        }
    }




}
