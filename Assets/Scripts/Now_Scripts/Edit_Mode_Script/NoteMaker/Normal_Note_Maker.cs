using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


//기본 노트가 의외로 확장될 수 있는 부분이 상당히 많아서 속성을 변경해야 할듯 함




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
                    //슬라이더로 값을 옮기면서 해당 위치가 계속해서 변하기 때문에 변하더라도 유동적으로 대응할 수 있도록 코드 추가
                    AddNote.GetComponent<Note>().SongTime = (double)RealXpos / GameManager.Instance.speed;
                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, SetHeight_Event(Pos.y), NoteType, 0, (double)RealXpos / GameManager.Instance.speed));
                    //변경해야 됨 현재 로직 변경함


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


