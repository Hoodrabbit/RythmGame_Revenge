using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


//�⺻ ��Ʈ�� �ǿܷ� Ȯ��� �� �ִ� �κ��� ����� ���Ƽ� �Ӽ��� �����ؾ� �ҵ� ��




public class Normal_Note_Maker : NoteMakerBase
{
    public GameObject NormalNote;

    protected override void Awake()
    {
        base.Awake();
        NoteType = 1;
    }


    public override GameObject Note { get => NormalNote; set => NormalNote = value; }

    protected override void AreaCheck(GameObject Note, Vector2 Pos, bool DeleteMode)
    {
        hit = Physics2D.RaycastAll(Pos, transform.forward, 10);


        int i = 0;
        bool Checkduplication = false; // ��Ʈ�� �ߺ��Ǽ� ������� ������ �˻�
        while (i < hit.Length && !DeleteMode)
        {
            //Debug.Log("�۵�" + hit[i].collider.name);
            if (hit[i].collider.CompareTag("Note"))
            {
                Checkduplication = true;
                Debug.Log("�ߺ��Դϴ�.");
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
                    GameObject AddNote = Instantiate(Note, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform);

                    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                    //�����̴��� ���� �ű�鼭 �ش� ��ġ�� ����ؼ� ���ϱ� ������ ���ϴ��� ���������� ������ �� �ֵ��� �ڵ� �߰�
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
        EventType GameEventState = EventManager.Instance.GetEvent();

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


}


