using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditManager : Singleton<EditManager>
{
    //���� ���������� �ʿ��� ��ɵ��� ��Ƴ��� 
    public BarNote NoteParent;

    public GameObject NormalNote_Obj;
    public GameObject LongNote_Obj;

    Queue<LongNoteScript> UnCompleteLongNoteQueue = new Queue<LongNoteScript>(); //���� ������ġ�� ����� �Ҵ���� ���� �ճ�Ʈ�� ���� �����ϱ� ���� �������


    public float GetNPXpos()
    {
        return NoteParent.transform.position.x;
    }

    public void MakeNote(float xpos, int height, int noteType, int LongNoteStartEndCheck)
    {
        //���߿� ��ȣ�� ���� ��ġ �� ������ ��Ȱ�ϰ� �� �� �ֵ��� ���� ������ �������� �� �� ����

        switch (noteType)
        {
            case 1:

                NormalNote(xpos, height, noteType, LongNoteStartEndCheck);
                break;

            case 2:

                LongNote(xpos, height, noteType, LongNoteStartEndCheck);
                //Debug.Log("�۵��� �ϳ�");
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


            //height �κ� ���߿� �ٲܰ���
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck));
        }
        else if (height == 2)
        {
            GameObject AddNote = Instantiate(NormalNote_Obj, new Vector3(xpos, -2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

            //height �κ� ���߿� �ٲܰ���
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck));


        }
    }

    public void LongNote(float xpos, int height, int noteType, int LongNoteStartEndCheck)
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
                if (head.transform.position.y == ypos) //�� ��ȣ�� 1�� ��� 2�� ��ġ��
                {
                    head.Tail.transform.position = new Vector3(xpos, head.transform.position.y);

                    float RealXpos = head.Tail.transform.position.x - EditManager.Instance.GetNPXpos();

                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(head.Tail, RealXpos, height, noteType, LongNoteStartEndCheck));
                }
                else
                {
                    newLongNoteQueue.Enqueue(head); //�ش� ������ �������� �ʴ� ť�� ���� �߰���
                }

            }
            UnCompleteLongNoteQueue = newLongNoteQueue;

        }
    }




}
