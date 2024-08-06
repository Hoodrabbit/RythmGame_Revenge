using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾��� �����ϴ� �̱���
public class PlayManager : Singleton<PlayManager>
{
    public List<GameObject> NoteTypes; //�ϴ� ������� �Ϲ� ��, Ư�� ��Ʈ ... �̷������� ����
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
                //Debug.Log("�۵��� �ϳ�");
                break;

            case 3:


                break;



            default:
                break;
        }


        //�ϴ� �ٽ� ������ �ϱ� ������ �ּ�ó����

        ////Notes.Add(Instantiate(xpos));//���� �Ұ���

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


            //height �κ� ���߿� �ٲܰ���
            //Notes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck));
        }
        else if (height == 2)
        {
            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck);
            Notes.Add(Instantiate(NoteTypes[0], new Vector3(NotePos.xpos, -2), Quaternion.identity, Note_Parent.transform));

            //float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

            ////height �κ� ���߿� �ٲܰ���
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

    //�ճ�Ʈ ���� �޼��� ���� �и���
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
                if (head.transform.position.y == ypos) //�� ��ȣ�� 1�� ��� 2�� ��ġ��
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
                    newLongNoteQueue.Enqueue(head); //�ش� ������ �������� �ʴ� ť�� ���� �߰���
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
        //���߿� ��ȣ�� ���� ��ġ �� ������ ��Ȱ�ϰ� �� �� �ֵ��� ���� ������ �������� �� �� ����
        if (height == 1)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, 2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


            //height �κ� ���߿� �ٲܰ���
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height));
        }
        else if (height == 2)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, -2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

            //height �κ� ���߿� �ٲܰ���
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height));


        }

    }
 
 */