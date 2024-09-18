using System.Collections;
using System.Collections.Generic;
using UnityEngine;











public class EditManager : Singleton<EditManager>
{
    //���� ���������� �ʿ��� ��ɵ��� ��Ƴ��� 
    public BarNote barNote;


    [Header("�Ϲ� ��Ʈ")]
    public GameObject NormalNote_Obj;
    public GameObject LongNote_Obj;

    [Header("Ư�� ��Ʈ��")]
    public GameObject ExpandLine_Obj;
    public GameObject GhostNote_Obj;

    [Header("���� ��Ʈ")]
    public GameObject BossAppearNote_Obj;
    public GameObject BossDisappearNote_Obj;

    Queue<LongNoteScript> UnCompleteLongNoteQueue = new Queue<LongNoteScript>(); //���� ������ġ�� ����� �Ҵ���� ���� �ճ�Ʈ�� ���� �����ϱ� ���� �������

    public const int UP = 3;
    public const int DOWN = -1;
    public const int MIDDLE = (UP + DOWN) / 2;



    public float GetNPXpos()
    {
        return barNote.transform.position.x;
    }

    public void MakeNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType=0)
    {
        //���߿� ��ȣ�� ���� ��ġ �� ������ ��Ȱ�ϰ� �� �� �ֵ��� ���� ������ �������� �� �� ����

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
                //Debug.Log("�۵��� �ϳ�");
                break;

            case 3:
                GhostNote(xpos, height, noteType, LongNoteStartEndCheck, songtime);

                break;



            case 100:
                Debug.Log("������Ʈ ");
                BossAppearNote(xpos, height, noteType, LongNoteStartEndCheck, songtime);
                break;

            case 101:
                Debug.Log("�����Ʈ ");
                BossDisappearNote(xpos, height, noteType, LongNoteStartEndCheck, songtime);
                break;


            default:
                break;
        }



    }

    public void NormalNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        GameObject AddNote = Instantiate(NormalNote_Obj, EditManager.Instance.barNote.RhythmNote.transform);

            AddNote.transform.position = new Vector3(xpos, SettingHeight(height));
            AddNote.GetComponent<Note>().SetNoteType(enemyType);


            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


            //height �κ� ���߿� �ٲܰ���
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / GameManager.Instance.speed, enemyType));


    }

    public void LongNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime,int enemyType= 0)
    {
        LongNoteInternalMethod(xpos, height, noteType, LongNoteStartEndCheck, (double)xpos / 10);
    }

             void LongNoteInternalMethod(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
    {

        GameObject LongNote;
        if (LongNoteStartEndCheck == 1)
        {
            LongNote = Instantiate(LongNote_Obj, new Vector3(xpos, SettingHeight(height)), Quaternion.identity, EditManager.Instance.barNote.transform);
            UnCompleteLongNoteQueue.Enqueue(LongNote.GetComponent<LongNoteScript>());

            float RealXpos = LongNote.transform.position.x - EditManager.Instance.GetNPXpos();

            DataManager.Instance.EditNotes.Add(new NoteInfoAll(LongNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)LongNote.transform.localPosition.x / GameManager.Instance.speed));
        }
        else if (LongNoteStartEndCheck == 2)
        {
            Queue<LongNoteScript> newLongNoteQueue = new Queue<LongNoteScript>();
            foreach (var head in UnCompleteLongNoteQueue)
            {
                if (head.transform.position.y == SettingHeight(height)) //�� ��ȣ�� 1�� ��� 2�� ��ġ��
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



    //public void ExpandLine(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
    //{
    //    GameObject AddNote = Instantiate(ExpandLine_Obj, new Vector3(xpos, 0), Quaternion.identity, EditManager.Instance.barNote.transform);

    //    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

    //    //height �κ� ���߿� �ٲܰ���
    //    DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / 10));
    //}
    public void GhostNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
        {
                GameObject AddNote = Instantiate(GhostNote_Obj, new Vector3(xpos, SettingHeight(height)), Quaternion.identity, EditManager.Instance.barNote.transform);

                float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

                DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / GameManager.Instance.speed));
         
        }


        //�ճ�Ʈ ���� �޼��� ���� �и���
  

    public void BossAppearNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        GameObject AddNote = Instantiate(BossAppearNote_Obj, new Vector3(xpos, MIDDLE), Quaternion.identity, EditManager.Instance.barNote.transform);

        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

        DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / 10));
    }

    public void BossDisappearNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType= 0)
    {
        GameObject AddNote = Instantiate(BossDisappearNote_Obj, new Vector3(xpos, MIDDLE), Quaternion.identity, EditManager.Instance.barNote.transform);

        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

        DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck, (double)AddNote.transform.localPosition.x / 10));
    }



    private int SettingHeight(int height)
    {
        switch (height)
        {
            case 1:
                return UP;
                

            case 2:
                return DOWN;
            default:
                return 0;
        }
    }



    public void ActivateBeatLine()
    {
        barNote.ActiveNote();
    }


    
}
