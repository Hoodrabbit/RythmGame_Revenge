using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�÷��̾��� �����ϴ� �̱���
public class PlayManager : Singleton<PlayManager>
{
    public List<GameObject> NoteTypes; //�ϴ� ������� �Ϲ� ��, Ư�� ��Ʈ ... �̷������� ����
    public List<GameObject> Notes;
    public NoteInfoPos NotePos;

    public ComboSystem combosystem;
    public ScoreSystem scoresystem;


    int NoteCount_Now =0;

    const int UP = 3;
    const int DOWN = -1;
    const int MIDDLE = (UP + DOWN) / 2;




    //public Image MusicTimeLeftLine;

    public GameObject Note_Parent;

    public GameObject[] HidingJudgementObj;

    Queue<LongNoteScript> UnCompleteLongNoteQueue = new Queue<LongNoteScript>();


    public void PlayScene_NoteMaker(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0 )
    {
        xpos += GameManager.Instance.OffsetValue;
        switch (noteType)
        {
            case 0:
                //ExpandLine(xpos, height, noteType, LongNoteStartEndCheck, songtime);
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


            case 100:
                Debug.Log("출현노트 ");
                BossAppearNote(xpos, height, noteType, LongNoteStartEndCheck, songtime);
                break;

            case 101:
                Debug.Log("퇴장노트 ");
                BossDisappearNote(xpos, height, noteType, LongNoteStartEndCheck, songtime);
                break;



            default:
                break;
        }


        //�ϴ� �ٽ� ������ �ϱ� ������ �ּ�ó����

        ////Notes.Add(Instantiate(xpos));//���� �Ұ���

        //NotePos = new NoteInfoPos(xpos + 1*3 * 6-5, height); 

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
        scoresystem.IncreaseScore();
    }

    public void MissNote()
    {
        combosystem.MissNote();
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



    void NormalNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType)
    {
        GameObject Note_Instantiate;

        

            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, SettingHeight(height), noteType , LongNoteStartEndCheck, songtime);
            Note_Instantiate = Instantiate(NoteTypes[0], new Vector3(NotePos.xpos, SettingHeight(height)), Quaternion.identity, Note_Parent.transform);
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
       

        GameObject LongNote;
        if (LongNoteStartEndCheck == 1)
        {
            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, SettingHeight(height), noteType, LongNoteStartEndCheck, songtime);
            LongNote = Instantiate(NoteTypes[1], new Vector3(NotePos.xpos, SettingHeight(height)), Quaternion.identity, Note_Parent.transform);
            LongNote.GetComponent<Note>().SetSongTime(songtime);
            Debug.Log(songtime + " ���ʴ��ΰ���");
            Notes.Add(LongNote);
            
            UnCompleteLongNoteQueue.Enqueue(LongNote.GetComponent<LongNoteScript>());
        }
        else if (LongNoteStartEndCheck == 2)
        {
            Queue<LongNoteScript> newLongNoteQueue = new Queue<LongNoteScript>();
            foreach (var head in UnCompleteLongNoteQueue)
            {
                if (head.transform.position.y == SettingHeight(height)) //�� ��ȣ�� 1�� ��� 2�� ��ġ��
                {
                    NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, SettingHeight(height), noteType, LongNoteStartEndCheck, songtime);
                    head.Tail.transform.position = new Vector3(NotePos.xpos, head.transform.position.y);

                    Notes.Add(head.Tail);
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

    //    GameObject Note_Instantiate;

    //    NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, height, noteType, LongNoteStartEndCheck, songtime);

    //    Note_Instantiate = Instantiate(NoteTypes[2], new Vector3(NotePos.xpos-1, height), Quaternion.identity, Note_Parent.transform);
        
    //    Note_Instantiate.GetComponent<Note>().SetSongTime(songtime);

    //    Notes.Add(Note_Instantiate);
    //}

    public void GhostNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime)
    {
        GameObject Note_Instantiate;
      
            NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, SettingHeight(height), noteType, LongNoteStartEndCheck, songtime);
            Note_Instantiate = Instantiate(NoteTypes[3], new Vector3(NotePos.xpos, SettingHeight(height)), Quaternion.identity, Note_Parent.transform);
            
            Note_Instantiate.GetComponent<Note>().SetSongTime(songtime);
                
            Notes.Add(Note_Instantiate);

            //float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


            //height �κ� ���߿� �ٲܰ���
            //Notes.Add(new NoteInfoAll(AddNote, RealXpos, height, noteType, LongNoteStartEndCheck));
    }


    public void BossAppearNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        GameObject Note_Instantiate;

        Debug.Log("BossAppearNote Xpos : " + xpos);

        NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, MIDDLE, noteType, LongNoteStartEndCheck, songtime);
        Note_Instantiate = Instantiate(NoteTypes[4], new Vector3(NotePos.xpos, MIDDLE), Quaternion.identity, Note_Parent.transform);

        Note_Instantiate.GetComponent<Note>().SetSongTime(songtime);

        Notes.Add(Note_Instantiate);
    }

    public void BossDisappearNote(float xpos, int height, int noteType, int LongNoteStartEndCheck, double songtime, int enemyType = 0)
    {
        GameObject Note_Instantiate;

        Debug.Log("BossDisppearNote Xpos : " + xpos);

        NotePos = new NoteInfoPos(xpos + 1 * 3 * GameManager.Instance.speed, MIDDLE, noteType, LongNoteStartEndCheck, songtime);
        Note_Instantiate = Instantiate(NoteTypes[5], new Vector3(NotePos.xpos, MIDDLE), Quaternion.identity, Note_Parent.transform);

        Note_Instantiate.GetComponent<Note>().SetSongTime(songtime);

        Notes.Add(Note_Instantiate);
        
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





/*
 public void MakeNote(float xpos, int height)
    {
        //���߿� ��ȣ�� ���� ��ġ �� ������ ��Ȱ�ϰ� �� �� �ֵ��� ���� ������ �������� �� �� ����
        if (height == 1)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, 2), Quaternion.identity, EditManager.Instance.barNote.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();


            //height �κ� ���߿� �ٲܰ���
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height));
        }
        else if (height == 2)
        {
            GameObject AddNote = Instantiate(Note, new Vector3(xpos, -2), Quaternion.identity, EditManager.Instance.barNote.transform);

            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

            //height �κ� ���߿� �ٲܰ���
            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, height));


        }

    }
 
 */