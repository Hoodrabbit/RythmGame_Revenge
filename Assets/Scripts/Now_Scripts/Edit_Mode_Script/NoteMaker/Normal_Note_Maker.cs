using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


//�⺻ ��Ʈ�� �ǿܷ� Ȯ��� �� �ִ� �κ��� ����� ���Ƽ� �Ӽ��� �����ؾ� �ҵ� ��




public class Normal_Note_Maker : NoteMakerBase
{
    public GameObject NormalNote;
    public GameObject NormalNote_UP;


    public override GameObject Note { get => NormalNote; set => NormalNote = value; }
    GameObject Note_UP { get => NormalNote_UP; set => NormalNote_UP = value; }

    public int SpriteNum;



    protected override void Awake()
    {
        base.Awake();
        //NoteType = 1;
    }


    

    protected override void AreaCheck(GameObject Note, Vector2 Pos, bool DeleteMode)
    {

        //hit = Physics2D.BoxCastAll(Pos, new Vector2(2, 50), 0, transform.forward,10);

        hit = Physics2D.RaycastAll(Pos, transform.forward, 10);


        int i = 0;
        bool Checkduplication = false; // ��Ʈ�� �ߺ��Ǽ� ������� ������ �˻�
        while (i < hit.Length && !DeleteMode)
        {
            //Debug.Log("�۵�" + hit[i].collider.name);
            if (hit[i].collider.CompareTag("Note"))
            {
                Checkduplication = true;
                NormalNote thisNote = hit[i].collider.gameObject.GetComponent<NormalNote>();
                ChangeMelodyType(thisNote);
                NoteInfoAll infoAll = DataManager.Instance.FindNoteData(hit[i].collider.gameObject);
                DataManager.Instance.ListNullCheck(hit[i].collider.gameObject);

                //find NotePos and change Data
                infoAll.ChangeEnemyType((int)thisNote.melodyType);
                //DataManager.Instance.EditNotes.Add(new NoteInfoAll(hit[i].collider.gameObject, infoAll.notePos.xpos, infoAll.notePos.HeightValue, infoAll.notePos.NoteType, 0, infoAll.notePos.SongTime, (int)thisNote.melodyType));

                //����� ���� ���� ����
                //�����ؾ� ��


            }

            if (hit[i].collider.CompareTag("NotePlace") && Checkduplication == false)
            {

                Vector2 InstantiatePos;

                if (Pos.y > 0)
                {
                    InstantiatePos = new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + EditManager.UP);
                }
                else
                {
                    InstantiatePos = new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + EditManager.DOWN);
                }



                if (NoteCheck(InstantiatePos))
                {
                    GameObject AddNote;
                    if (Pos.y > 0)
                    {
                        AddNote = Instantiate(Note_UP, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform);
                    }
                    else
                    {
                        AddNote = Instantiate(Note, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform);
                    }


                    

                    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                    //�����̴��� ���� �ű�鼭 �ش� ��ġ�� ����ؼ� ���ϱ� ������ ���ϴ��� ���������� ������ �� �ֵ��� �ڵ� �߰�

                    Debug.Log("��Ʈ ����");

                    AddNote.GetComponent<Note>().SongTime = (double)RealXpos / GameManager.Instance.speed;
                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, SetHeight_Event(Pos.y), NoteType, 0, (double)RealXpos / GameManager.Instance.speed));
                    //�����ؾ� �� ���� ���� ������


                }
            }
            i++;
        }

        if (DeleteMode)
        {
            i = 0;
            while (i < hit.Length && DeleteMode)
            {
                if (hit[i].collider.CompareTag("Note"))
                {
                    Destroy(hit[i].transform.gameObject);
                }
                i++;
            }
        }
    }


    int SetHeight_Event(float Ypos)
    {
        EventType GameEventState = DataManager.Instance.eventManager.GetEvent();

        if (GameEventState == EventType.SpawnOutside_Reverse)
        {

            if (Ypos > 0)
            {
                return EditManager.DOWN_OUTSIDE;
            }
            else
            {
                return EditManager.UP_OUTSIDE;
            }


        }
        else if (GameEventState == EventType.SpawnOutside)
        {

            if (Ypos > 0)
            {
                return EditManager.UP_OUTSIDE;
            }
            else
            {
                return EditManager.DOWN_OUTSIDE;
            }

        }
        else
        {

            if (Ypos > 0)
            {
                return EditManager.UP;
            }
            else
            {
                return EditManager.DOWN;
            }

        }

    }

    void ChangeMelodyType(NormalNote note)
    {
        switch (note.melodyType)
        {
            case MelodyType.Normal:
                //note.melodyType = MelodyType.Yellow;
                note.SetNoteType(1);
                break;

            case MelodyType.Yellow:
                //note.melodyType = MelodyType.Purple;
                note.SetNoteType(2);
                break;
            case MelodyType.Purple:
                //note.melodyType = MelodyType.Normal;
                note.SetNoteType(0);
                break;

            case MelodyType.Obstacle: break;
        }

    }






}




