using JetBrains.Annotations;
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


    public void PlayScene_NoteMaker(float xpos, int heightnum, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0 )
    {

        switch (noteType)
        {
            case 0:
                ExpandLine(xpos, heightnum, noteType, LongNoteStartEndCheck, songtime);
                break;

            case 1:

                NormalNote(xpos, heightnum, noteType, LongNoteStartEndCheck, songtime, enemyType);
                break;

            case 2:

               LongNote(xpos, heightnum, noteType, LongNoteStartEndCheck, songtime);
                //Debug.Log("�۵��� �ϳ�");
                break;

            case 3:
                GhostNote(xpos, heightnum, noteType, LongNoteStartEndCheck, songtime);

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


    void NormalNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType)
    {
        GameObject Note_Instantiate;

        float ypos = 0;
        switch (height)
        {
            case 1:
                ypos = 2.8f;
                break;

            case 2:
                ypos = -2.2f;
                break;
        }

            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType , LongNoteStartEndCheck, songtime);
            Note_Instantiate = Instantiate(NoteTypes[0], new Vector3(NotePos.xpos, ypos), Quaternion.identity, Note_Parent.transform);
            Note_Instantiate.GetComponent<Note>().SetSongTime(songtime);
            Note_Instantiate.GetComponent<Note>().SetNoteType(enemyType);
            Notes.Add(Note_Instantiate);

    }

    void LongNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
    {
        LongNoteInternalMethod(xpos, height, noteType, LongNoteStartEndCheck, songtime);
    }

    //�ճ�Ʈ ���� �޼��� ���� �и���
    void LongNoteInternalMethod(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
    {
        float ypos = 0;
        switch (height)
        {
            case 1:
                ypos = 2.8f;
                break;

            case 2:
                ypos = -2.2f;
                break;
        }

        GameObject LongNote;
        if (LongNoteStartEndCheck == 1)
        {
            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck, songtime);
            LongNote = Instantiate(NoteTypes[1], new Vector3(NotePos.xpos, ypos), Quaternion.identity, Note_Parent.transform);
            LongNote.GetComponent<Note>().SetSongTime(songtime);
            Debug.Log(songtime + " ���ʴ��ΰ���");
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
                    NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck, songtime);
                    head.Tail.transform.position = new Vector3(NotePos.xpos, head.transform.position.y);
                    //Debug.Log(xpos);
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

    public void ExpandLine(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
    {

        GameObject Note_Instantiate;

        NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck, songtime);

        Note_Instantiate = Instantiate(NoteTypes[2], new Vector3(NotePos.xpos-1, height), Quaternion.identity, Note_Parent.transform);
        
        Note_Instantiate.GetComponent<Note>().SetSongTime(songtime);

        Notes.Add(Note_Instantiate);
    }

    public void GhostNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
    {
        GameObject Note_Instantiate;

        float ypos = 0;
        switch (height)
        {
            case 1:
                ypos = 2.8f;
                break;

            case 2:
                ypos = -2.2f;
                break;
        }

      
            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck, songtime);
            Note_Instantiate = Instantiate(NoteTypes[3], new Vector3(NotePos.xpos, ypos), Quaternion.identity, Note_Parent.transform);
            
            Note_Instantiate.GetComponent<Note>().SetSongTime(songtime);
                
            Notes.Add(Note_Instantiate);

            //float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


            //height �κ� ���߿� �ٲܰ���
            //Notes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck));
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