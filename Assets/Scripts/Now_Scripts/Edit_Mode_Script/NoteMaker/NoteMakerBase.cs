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
    ///  (0 : Obstacle 1 : NormalNote , 2 : LongNote, 3 : GhostNote   100 : BossAppearNote      특수 노트가 될 예정 여기에서 더 추가 될 수 있음)
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
            transform.position = Pos; //마우스의 현재 위치에 노트를 생성시켜주는 오브젝트가 위치할 수 있도록 함

            if (Input.GetMouseButtonDown(0) && Note != null)
            {
                AreaCheck(Note, transform.position, false);
            }

            if (Input.GetMouseButton(1))
            {
                //Debug.Log("제거모드");
                AreaCheck(Note, transform.position, true);
            }
        }
        else
        {
            //현재 체크 상태가 키보드인 경우 아래 코드를 사용 못하도록 막아줘야 함

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
        bool Checkduplication = false; // 노트가 중복되서 들어있지 않은지 검사
        while (i < hit.Length && !DeleteMode)
        {
            //Debug.Log("작동" + hit[i].collider.name);
            if (hit[i].collider.CompareTag("Note"))
            {
                Checkduplication = true;
                Debug.Log("중복입니다.");
            }

            if (hit[i].collider.CompareTag("NotePlace") && Checkduplication == false)
            {
                //수정할 부분
                //레이를 쏴서 노트가 존재하는지 감지를 하는 것 뿐만아니라 생성시키는 곳에 노트가 존재하는지도 확인해야 함 그렇지 않으면 같은 자리에 노트가 중복으로 생성됨


                //1
                if (Pos.y > 0)
                {
                    Vector2 InstantiatePos = new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + EditManager.UP);


                    if (NoteCheck(InstantiatePos))
                    {
                        GameObject AddNote = Instantiate(Note, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform);

                        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                        //슬라이더로 값을 옮기면서 해당 위치가 계속해서 변하기 때문에 변하더라도 유동적으로 대응할 수 있도록 코드 추가


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
                        //위와 동일 

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
                //Debug.Log("작동" + hit[i].collider.name);


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
                Debug.Log("중복입니다.");
                return false;
            }
            count++;
        }

        return true;

    }




}
