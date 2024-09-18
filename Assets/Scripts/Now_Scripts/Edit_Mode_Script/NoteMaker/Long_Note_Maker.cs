using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Long_Note_Maker : NoteMakerBase
{
    public GameObject Head;
    public GameObject Tail;
    public override GameObject Note { get => Head; set => Head = value; }

    Transform HeadPos; //�Ӹ���ġ ��������

    public bool TailOn = false;


    private void OnEnable()
    {
        TailOn = false;
        Tail = null;
    }

    private void OnDisable()
    {
        if(Tail != null) 
        {
            if(TailOn == true)
            {
                Destroy(Tail.transform.parent.gameObject);
            }
        }
    }


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
                if (Pos.y > 0)
                {
                    Vector3 InstantiatePos = new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + EditManager.UP);

                    if (NoteCheck(InstantiatePos))
                    {


                        if (TailOn == false)
                        {

                            GameObject LongNote; //������ ��Ʈ�� ���� ������ ��� ����ؾ� �ؼ� ������� ������Ʈ�Դϴ�. ���߿��� �Ƹ��� list���ٰ� ������ ���� ������ �� ���׿�.

                            LongNote = Instantiate(Head, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform); //�Ӹ� ����

                            HeadPos = LongNote.transform; //�Ӹ� ��ġ �Ҵ�

                            Tail = LongNote.transform.GetChild(1).gameObject; //������ �Ӹ� ������Ʈ�� 2��° ���� ������Ʈ�� ��ġ�� �ֱ� ������ �̷��� �ۼ���

                            TailOn = true; //�Ӹ� ��Ʈ ���������� ���� ���� ��ġ �����ؾ� �ϴϱ� true�� �� ����

                            float RealXpos = LongNote.transform.position.x - EditManager.Instance.GetNPXpos();
                            //�����̴��� ���� �ű�鼭 �ش� ��ġ�� ����ؼ� ���ϱ� ������ ���ϴ��� ���������� ������ �� �ֵ��� �ڵ� �߰�

                            DataManager.Instance.EditNotes.Add(new NoteInfoAll(LongNote, RealXpos, 1, 2, 1, (double)RealXpos / GameManager.Instance.speed));
                        }
                        else
                        {
                            if (Tail != null)
                            {
                                if (hit[i].transform.position.x > HeadPos.position.x) //�⺻������ ������ �Ӹ����� �տ� �ְų� ���� ��ġ�� ������ �ȵǱ� ������ �ش� ������ ��������
                                {
                                    Tail.transform.position = new Vector2(hit[i].transform.position.x, HeadPos.position.y); //���� ��ġ ����

                                    TailOn = false; //�ٽ� �Ӹ� �������ֱ� ���� false�� �� ����

                                    float RealXpos = Tail.transform.position.x - EditManager.Instance.GetNPXpos();

                                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(Tail, RealXpos, 1, 2, 2, (double)RealXpos / GameManager.Instance.speed));

                                }

                            }
                        }
                    }
                }
                else
                {

                    Vector3 InstantiatePos = new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + EditManager.DOWN);

                    if (NoteCheck(InstantiatePos))
                    {
                        if (TailOn == false)
                        {
                            GameObject LongNote; //������ ��Ʈ�� ���� ������ ��� ����ؾ� �ؼ� ������� ������Ʈ�Դϴ�. ���߿��� �Ƹ��� list���ٰ� ������ ���� ������ �� ���׿�.

                            LongNote = Instantiate(Head, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform); //�Ӹ� ����

                            HeadPos = LongNote.transform; //�Ӹ� ��ġ �Ҵ�

                            Tail = LongNote.transform.GetChild(1).gameObject; //������ �Ӹ� ������Ʈ�� 2��° ���� ������Ʈ�� ��ġ�� �ֱ� ������ �̷��� �ۼ���

                            TailOn = true; //�Ӹ� ��Ʈ ���������� ���� ���� ��ġ �����ؾ� �ϴϱ� true�� �� ����

                            float RealXpos = LongNote.transform.position.x - EditManager.Instance.GetNPXpos();

                            DataManager.Instance.EditNotes.Add(new NoteInfoAll(LongNote, RealXpos, 2, 2, 1, (double)RealXpos / GameManager.Instance.speed));

                        }
                        else
                        {
                            if (Tail != null)
                            {

                                if (hit[i].transform.position.x > HeadPos.position.x) //�⺻������ ������ �Ӹ����� �տ� �ְų� ���� ��ġ�� ������ �ȵǱ� ������ �ش� ������ ��������
                                {
                                    Tail.transform.position = new Vector2(hit[i].transform.position.x, HeadPos.position.y); //���� ��ġ ����

                                    TailOn = false; //�ٽ� �Ӹ� �������ֱ� ���� false�� �� ����

                                    float RealXpos = Tail.transform.position.x - EditManager.Instance.GetNPXpos();

                                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(Tail, RealXpos, 2, 2, 2, (double)RealXpos / GameManager.Instance.speed));
                                }

                            }
                        }
                    }
                }
                i++;
            }
        }

        if (DeleteMode)
        {
            i = 0;
            while (i < hit.Length && DeleteMode)
            {
                //Debug.Log("�۵�" + hit[i].collider.name);


                if (hit[i].collider.CompareTag("Note"))
                {
                    Transform LongNote = hit[i].collider.transform;
                    LongNoteScript longNoteScript = LongNote.parent.GetComponent<LongNoteScript>();
                    if (longNoteScript != null)
                    {
                        Debug.Log(longNoteScript.gameObject.name);
                        Destroy(longNoteScript.gameObject);
                    }


                }
                i++;
            }
        }



    }

    


}
