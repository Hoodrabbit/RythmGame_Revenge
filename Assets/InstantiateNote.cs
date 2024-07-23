using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class InstantiateNote : MonoBehaviour
{
    public RaycastHit2D[] hit;

    public GameObject Head;
    public GameObject Tail;

    Vector2 HeadPos; //�Ӹ���ġ ��������

    public bool TailOn = false;





    public void Update()
    {
        Vector2 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Pos; //���콺�� ���� ��ġ�� ��Ʈ�� ���������ִ� ������Ʈ�� ��ġ�� �� �ֵ��� ��

        if (Input.GetMouseButtonDown(0))
        {
            AreaCheck(transform.position, false);
        }

        if (Input.GetMouseButton(1))
        {
            AreaCheck(transform.position, true);
        }
    }

    public void LongNoteMaker()
    {

    }

    public void AreaCheck(Vector2 Pos, bool DeleteMode)
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

                    if(TailOn == false)
                    {

                        GameObject LongNote; //������ ��Ʈ�� ���� ������ ��� ����ؾ� �ؼ� ������� ������Ʈ�Դϴ�. ���߿��� �Ƹ��� list���ٰ� ������ ���� ������ �� ���׿�.

                        LongNote = Instantiate(Head, new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + 2), Quaternion.identity, hit[i].transform.parent); //�Ӹ� ����

                        HeadPos = new Vector2(LongNote.transform.position.x, LongNote.transform.position.y); //�Ӹ� ��ġ �Ҵ�

                        Tail = LongNote.transform.GetChild(1).gameObject; //������ �Ӹ� ������Ʈ�� 2��° ���� ������Ʈ�� ��ġ�� �ֱ� ������ �̷��� �ۼ���

                        TailOn = true; //�Ӹ� ��Ʈ ���������� ���� ���� ��ġ �����ؾ� �ϴϱ� true�� �� ����

                        float RealXpos = LongNote.transform.position.x - EditManager.Instance.GetNPXpos();
                        //�����̴��� ���� �ű�鼭 �ش� ��ġ�� ����ؼ� ���ϱ� ������ ���ϴ��� ���������� ������ �� �ֵ��� �ڵ� �߰�

                        DataManager.Instance.EditNotes.Add(new NoteInfoAll(LongNote, RealXpos, 1, 2, 1));
                    }
                    else
                    {
                        if(Tail != null)
                        {
                            if (hit[i].transform.position.x > HeadPos.x) //�⺻������ ������ �Ӹ����� �տ� �ְų� ���� ��ġ�� ������ �ȵǱ� ������ �ش� ������ ��������
                            {
                                Tail.transform.position = new Vector2(hit[i].transform.position.x, HeadPos.y); //���� ��ġ ����

                                TailOn = false; //�ٽ� �Ӹ� �������ֱ� ���� false�� �� ����

                                float RealXpos = Tail.transform.position.x - EditManager.Instance.GetNPXpos();

                                DataManager.Instance.EditNotes.Add(new NoteInfoAll(Tail, RealXpos, 1, 2, 2));

                            }

                        }
                    }
                }
                else
                {
                    if (TailOn == false)
                    {
                        GameObject LongNote; //������ ��Ʈ�� ���� ������ ��� ����ؾ� �ؼ� ������� ������Ʈ�Դϴ�. ���߿��� �Ƹ��� list���ٰ� ������ ���� ������ �� ���׿�.

                        LongNote = Instantiate(Head, new Vector3(hit[i].transform.position.x, hit[i].transform.position.y - 2), Quaternion.identity, hit[i].transform.parent); //�Ӹ� ����

                        HeadPos = new Vector2(LongNote.transform.position.x, LongNote.transform.position.y); //�Ӹ� ��ġ �Ҵ�

                        Tail = LongNote.transform.GetChild(1).gameObject; //������ �Ӹ� ������Ʈ�� 2��° ���� ������Ʈ�� ��ġ�� �ֱ� ������ �̷��� �ۼ���

                        TailOn = true; //�Ӹ� ��Ʈ ���������� ���� ���� ��ġ �����ؾ� �ϴϱ� true�� �� ����

                        float RealXpos = LongNote.transform.position.x - EditManager.Instance.GetNPXpos();

                        DataManager.Instance.EditNotes.Add(new NoteInfoAll(LongNote, RealXpos, 2, 2, 1));


                    }
                    else
                    {
                        if (Tail != null)
                        {
                            //Debug.Log("������ �ȵ�");
                            if (hit[i].transform.position.x > HeadPos.x) //�⺻������ ������ �Ӹ����� �տ� �ְų� ���� ��ġ�� ������ �ȵǱ� ������ �ش� ������ ��������
                            {
                                Tail.transform.position = new Vector2(hit[i].transform.position.x, HeadPos.y); //���� ��ġ ����

                                TailOn = false; //�ٽ� �Ӹ� �������ֱ� ���� false�� �� ����

                                float RealXpos = Tail.transform.position.x - EditManager.Instance.GetNPXpos();

                                DataManager.Instance.EditNotes.Add(new NoteInfoAll(Tail, RealXpos, 2, 2, 2));
                            }

                        }
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

                    //�ճ�Ʈ�� ��� ���� �ٸ��� ������������
                    //Destroy(hit[i].transform.gameObject);


                }
                i++;
            }
        }



    }







}
