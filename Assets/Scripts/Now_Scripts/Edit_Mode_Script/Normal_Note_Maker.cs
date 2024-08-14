using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;




public class Normal_Note_Maker : NoteMakerBase
{
    public GameObject Note;

    //Ŭ������ ���� �ش� ������ üũ�ؼ� �ش� ������ ��Ʈ�� ��ġ�ص� �Ǵ� ȯ������ Ȯ���ϰ� ���� �ƴ϶�� �α׷� �ش� ������ �߸��� ���Դϴ�. ��� �����
    //���������� �ش� ������ üũ���� �� �ش� ������ ��Ʈ�� �̹� �ִٸ� ��Ʈ�� �̹� �����ϴٰ� �α׷� ����ֱ�
    //���� ��츦 ������� ��� ��Ʈ�� �ش� ������ ���������ֱ�+����Ʈ���� �־���(�ƴϸ� ��Ʈ�� �������� �� �ش� ��Ʈ�� ����Ʈ�� ���ʴ�� �ְ� �ϴ� ����� ������)
    protected override void AreaCheck(Vector2 Pos, bool DeleteMode)
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

                Note n = hit[i].collider.GetComponent<Note>();
                int num = (int)n.GetNoteType() + 1;
                
                if(num > 2)
                {
                    num = 0;
                }
                
                Destroy(hit[i].transform.gameObject);

                //������ ������ ����
                ////
                int j =0;
                while (j < hit.Length)
                {
                    //Debug.Log("���ư�����"); Debug.Log(num);

                    if (hit[j].collider.CompareTag("NotePlace"))
                    {
                        if (Pos.y > 0)
                        {
                            GameObject AddNote = Instantiate(Note, new Vector3(hit[j].transform.position.x, hit[j].transform.position.y + 2), Quaternion.identity, barNote.RhythmNote.transform);
                            Note nnote = AddNote.GetComponent<Note>();
                            nnote.SetNoteType(num);
                            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                            //�����̴��� ���� �ű�鼭 �ش� ��ġ�� ����ؼ� ���ϱ� ������ ���ϴ��� ���������� ������ �� �ֵ��� �ڵ� �߰�


                            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, 1, 1, 0, (double)RealXpos / GameManager.Instance.speed, num));
                            break;
                        }


                        else if (Pos.y < 0)
                        {
                            GameObject AddNote = Instantiate(Note, new Vector3(hit[j].transform.position.x, hit[j].transform.position.y - 2), Quaternion.identity, barNote.RhythmNote.transform);
                            Note nnote = AddNote.GetComponent<Note>();

                            nnote.SetNoteType(num);


                            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                            //���� ���� 

                            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, 2, 1, 0, (double)RealXpos / GameManager.Instance.speed, num));
                            break;
                        }
                    }
                    j++;
                }

                ////


                break;
            }

            if (hit[i].collider.CompareTag("NotePlace") && Checkduplication == false)
            {


                if(Pos.y < 3 &&Pos.y > 0)
                {
                    RaycastHit2D[] NextHit;
                    Vector3 NotePos = new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + 2);
                    NextHit = Physics2D.RaycastAll(NotePos, transform.forward, 10);

                    for(int k = 0; k < NextHit.Length; k++)
                    {
                        if (NextHit[i].collider.CompareTag("Note"))
                        {
                            break;
                        }

                    }
                    GameObject AddNote = Instantiate(Note, new Vector3(hit[i].transform.position.x, hit[i].transform.position.y+2), Quaternion.identity, barNote.RhythmNote.transform);

                    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                    //�����̴��� ���� �ű�鼭 �ش� ��ġ�� ����ؼ� ���ϱ� ������ ���ϴ��� ���������� ������ �� �ֵ��� �ڵ� �߰�


                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, 1,1,0, (double)RealXpos / GameManager.Instance.speed));    
                }

                //2
                else if (Pos.y > -3 && Pos.y < 0)
                {
                    RaycastHit2D[] NextHit;
                    Vector3 NotePos = new Vector3(hit[i].transform.position.x, hit[i].transform.position.y - 2);
                    NextHit = Physics2D.RaycastAll(NotePos, transform.forward, 10);

                    for (int k = 0; k < NextHit.Length; k++)
                    {
                        if (NextHit[i].collider.CompareTag("Note"))
                        {
                            break;
                        }

                    }

                    GameObject AddNote = Instantiate(Note, new Vector3(hit[i].transform.position.x, hit[i].transform.position.y - 2), Quaternion.identity, barNote.RhythmNote.transform);

                    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                    //���� ���� 

                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, 2,1,0, (double)RealXpos / GameManager.Instance.speed)) ;
                }

                
            }
            i++;
        }

        if(DeleteMode)
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

    //public void MakeNote()//��Ʈ �������ִ� �޼��� �ٽ� �������� �� ���߿� �ε� ���� ���� �ش� ������ �� ����� �� �ֵ��� 
    //{
    //    GameObject AddNote = Instantiate(Note, new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + 2), Quaternion.identity, hit[i].transform.parent);

    //    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
    //    //�����̴��� ���� �ű�鼭 �ش� ��ġ�� ����ؼ� ���ϱ� ������ ���ϴ��� ���������� ������ �� �ֵ��� �ڵ� �߰�


    //    DataManager.Instance.EditNotes.Add(new NoteInfoAll(RealXpos, 1));
    //}

    //public void MakeNote(float xpos, int height)
    //{
    //    //���߿� ��ȣ�� ���� ��ġ �� ������ ��Ȱ�ϰ� �� �� �ֵ��� ���� ������ �������� �� �� ����
    //    if(height == 1)
    //    {
    //        GameObject AddNote = Instantiate(Note, new Vector3(xpos, 2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

    //        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

    //        DataManager.Instance.EditNotes.Add(new NoteInfoAll(RealXpos, 1));
    //    }
    //    else if(height == 2) 
    //    {
    //        GameObject AddNote = Instantiate(Note, new Vector3(xpos, -2), Quaternion.identity, EditManager.Instance.NoteParent.transform);

    //        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();

    //        DataManager.Instance.EditNotes.Add(new NoteInfoAll(RealXpos, 1));


    //    }
        
    //}



    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    AreaCheck(transform.position);
    //    Debug.Log("Ŭ��");
    //}
}
