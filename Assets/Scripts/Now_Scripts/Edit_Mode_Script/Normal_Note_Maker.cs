using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;




public class Normal_Note_Maker : NoteMakerBase
{
    public GameObject Note;

    //클릭했을 때의 해당 영역을 체크해서 해당 영역이 노트를 설치해도 되는 환경인지 확인하고 만약 아니라면 로그로 해당 영역에 잘못된 곳입니다. 라고 출력함
    //마찬가지로 해당 영역을 체크했을 때 해당 영역에 노트가 이미 있다면 노트가 이미 존재하다고 로그로 띄워주기
    //위의 경우를 통과했을 경우 노트를 해당 영역에 생성시켜주기+리스트에도 넣어줌(아니면 노트를 저장해줄 때 해당 노트를 리스트에 차례대로 넣고 하는 방법도 괜찮음)
    protected override void AreaCheck(Vector2 Pos, bool DeleteMode)
    {
        hit = Physics2D.RaycastAll(Pos, transform.forward, 10);


        int i = 0;
        bool Checkduplication = false; // 노트가 중복되서 들어있지 않은지 검사
        while (i < hit.Length && !DeleteMode)
        {
            //Debug.Log("작동" + hit[i].collider.name);
            if (hit[i].collider.CompareTag("Note"))
            {
                Checkduplication = true;
                Debug.Log("중복입니다.");

                Note n = hit[i].collider.GetComponent<Note>();
                int num = (int)n.GetNoteType() + 1;
                
                if(num > 2)
                {
                    num = 0;
                }
                
                Destroy(hit[i].transform.gameObject);

                //무조건 변경할 예정
                ////
                int j =0;
                while (j < hit.Length)
                {
                    //Debug.Log("돌아가나요"); Debug.Log(num);

                    if (hit[j].collider.CompareTag("NotePlace"))
                    {
                        if (Pos.y > 0)
                        {
                            GameObject AddNote = Instantiate(Note, new Vector3(hit[j].transform.position.x, hit[j].transform.position.y + 2), Quaternion.identity, barNote.RhythmNote.transform);
                            Note nnote = AddNote.GetComponent<Note>();
                            nnote.SetNoteType(num);
                            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                            //슬라이더로 값을 옮기면서 해당 위치가 계속해서 변하기 때문에 변하더라도 유동적으로 대응할 수 있도록 코드 추가


                            DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, 1, 1, 0, (double)RealXpos / GameManager.Instance.speed, num));
                            break;
                        }


                        else if (Pos.y < 0)
                        {
                            GameObject AddNote = Instantiate(Note, new Vector3(hit[j].transform.position.x, hit[j].transform.position.y - 2), Quaternion.identity, barNote.RhythmNote.transform);
                            Note nnote = AddNote.GetComponent<Note>();

                            nnote.SetNoteType(num);


                            float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                            //위와 동일 

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
                    //슬라이더로 값을 옮기면서 해당 위치가 계속해서 변하기 때문에 변하더라도 유동적으로 대응할 수 있도록 코드 추가


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
                    //위와 동일 

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
                //Debug.Log("작동" + hit[i].collider.name);
                

                if (hit[i].collider.CompareTag("Note"))
                {
                    Destroy(hit[i].transform.gameObject);
                }
                i++;
            }
        }



    }

    //public void MakeNote()//노트 생성해주는 메서드 다시 만들어줘야 함 나중에 로드 했을 때도 해당 데이터 잘 사용할 수 있도록 
    //{
    //    GameObject AddNote = Instantiate(Note, new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + 2), Quaternion.identity, hit[i].transform.parent);

    //    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
    //    //슬라이더로 값을 옮기면서 해당 위치가 계속해서 변하기 때문에 변하더라도 유동적으로 대응할 수 있도록 코드 추가


    //    DataManager.Instance.EditNotes.Add(new NoteInfoAll(RealXpos, 1));
    //}

    //public void MakeNote(float xpos, int height)
    //{
    //    //나중에 번호에 따른 수치 값 조정을 원활하게 할 수 있도록 따로 값으로 만들어놔야 할 것 같음
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
    //    Debug.Log("클릭");
    //}
}
