using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class NoteMakerBase : MonoBehaviour
{
    public abstract GameObject Note { get; set; }

    protected RaycastHit2D[] hit;

    protected BarNote barNote;


    /// <summary>
    ///  (0 : Obstacle 1 : NormalNote , 2 : LongNote, 3 : GhostNote   100 : BossAppearNote      Ư�� ��Ʈ�� �� ���� ���⿡�� �� �߰� �� �� ����)
    /// </summary>
    protected int NoteType;


    protected virtual void Awake()
    {
        barNote = FindObjectOfType<BarNote>();
        transform.position = new Vector3(0, 2);
        NoteType = Note.GetComponent<Note>().TypeNum;
    }


    protected virtual void Update()
    {

       
        if(EditManager.Instance.OperateEditState == NoteEditOperatingState.Mouse)
        {
            Vector2 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Pos; //���콺�� ���� ��ġ�� ��Ʈ�� ���������ִ� ������Ʈ�� ��ġ�� �� �ֵ��� ��

            if (Input.GetMouseButtonDown(0) && Note != null)
            {
                AreaCheck(Note, transform.position, false);
            }

            if (Input.GetMouseButton(1))
            {
                //Debug.Log("���Ÿ��");
                AreaCheck(Note, transform.position, true);
            }
        }
        else
        {
            //���� üũ ���°� Ű������ ��� �Ʒ� �ڵ带 ��� ���ϵ��� ������� ��

            //transform.position = Pos;

            if(Input.GetKeyDown(KeyCode.W))
            {
                transform.position = new Vector3(0, EditManager.UP);
            }


            if(Input.GetKeyDown(KeyCode.S))
            {
                transform.position = new Vector3(0, EditManager.DOWN);
            }


            

            if(Input.GetKeyDown(KeyCode.E))
            {
                AreaCheck(Note, transform.position, false);
            }

            if(Input.GetKeyDown(KeyCode.Delete))
            {
                AreaCheck(Note, transform.position, true);
            }




        }



    }

    protected virtual void AreaCheck(GameObject Note, Vector2 Pos, bool DeleteMode) 
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
                //������ �κ�
                //���̸� ���� ��Ʈ�� �����ϴ��� ������ �ϴ� �� �Ӹ��ƴ϶� ������Ű�� ���� ��Ʈ�� �����ϴ����� Ȯ���ؾ� �� �׷��� ������ ���� �ڸ��� ��Ʈ�� �ߺ����� ������


                //1
                if (Pos.y > 0)
                {
                    Vector2 InstantiatePos = new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + EditManager.UP);


                    if (NoteCheck(InstantiatePos))
                    {
                        GameObject AddNote = Instantiate(Note, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform);

                        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                        //�����̴��� ���� �ű�鼭 �ش� ��ġ�� ����ؼ� ���ϱ� ������ ���ϴ��� ���������� ������ �� �ֵ��� �ڵ� �߰�


                        DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, 1, NoteType, 0, (double)RealXpos / 10));
                    }

                }

                //2
                else if (Pos.y <= 0)
                {
                    Vector2 InstantiatePos = new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + EditManager.DOWN);


                    if (NoteCheck(InstantiatePos))
                    {
                        GameObject AddNote = Instantiate(Note, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform);

                        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                        //���� ���� 

                        DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, 2, NoteType, 0, (double)RealXpos / 10));
                    }
                }
            }
            i++;
        }

        if (DeleteMode)
        {
            i = 0;
            while (i < hit.Length && DeleteMode)
            {
                //Debug.Log("�۵�" + hit[i].collider.name);


                if (hit[i].collider.CompareTag("Note"))
                {
                    Destroy(hit[i].transform.gameObject);
                }
                i++;
            }
        }


    }

    protected virtual bool NoteCheck(Vector2 Pos)
    {

        Vector2 rayDirection = Pos;
        //Debug.Log("Posup : " + rayDirection);
        RaycastHit2D[] hit_Detail;
        int count = 0;
        hit_Detail = Physics2D.RaycastAll(rayDirection, transform.forward, 10);

        while (count < hit_Detail.Length)
        {
            if (hit_Detail[count].collider.CompareTag("Note"))
            {
                Debug.Log("�ߺ��Դϴ�.");
                return false;
            }
            count++;
        }

        return true;

    }




}
