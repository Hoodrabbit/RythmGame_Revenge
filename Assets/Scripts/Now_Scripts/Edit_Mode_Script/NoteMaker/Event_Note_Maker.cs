using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Event_Note_Maker : NoteMakerBase
{
    public GameObject EventNote;

    //고른 노트의 데이터 타입을 확인해서 번호를 얻어가는 메서드도 필요함

    
    public override GameObject Note { get => EventNote; set => EventNote = value; }


    protected override void Awake()
    {
        base.Awake();
        //NoteType = EventNote.GetComponent<Note>().TypeNum;
    }

    protected override void Update()
    {
        base.Update();
    } // 클릭 이벤트를 처리하는 메서드



    protected override void AreaCheck(GameObject Note, Vector2 Pos, bool DeleteMode)
    {
        hit = Physics2D.RaycastAll(Pos, transform.forward, 10);
        int i = 0;

        while (i < hit.Length && !DeleteMode)
        {

            if (hit[i].collider.CompareTag("NotePlace"))
            {

                Vector2 InstantiatePos = new Vector3(hit[i].transform.position.x, EditManager.MIDDLE);

                if (NoteCheck(InstantiatePos))
                {

                    GameObject AddEvent = Instantiate(Note, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform);

                    float RealXpos = AddEvent.transform.position.x - EditManager.Instance.GetNPXpos();
                    //위와 동일 

                    AddEvent.GetComponent<NoteEventScript>().SetSongTime(RealXpos / GameManager.Instance.speed);
                    DataManager.Instance.EventNotes.Add(new EventInfoAll(AddEvent, RealXpos, 0, NoteType, (double)RealXpos / GameManager.Instance.speed));
                    
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


                if (hit[i].collider.CompareTag("EventNote"))
                {

                    Destroy(hit[i].collider.gameObject);



                }
                i++;
            }
        }



    }



    protected override bool NoteCheck(Vector2 Pos)
    {

        Vector2 rayDirection = Pos;
        Debug.Log("Posup : " + rayDirection);
        RaycastHit2D[] hit_Detail;
        int count = 0;
        hit_Detail = Physics2D.RaycastAll(rayDirection, transform.forward, 10);

        while (count < hit_Detail.Length)
        {
            if (hit_Detail[count].collider.CompareTag("BossActionNote") || hit_Detail[count].collider.CompareTag("EventNote"))
            {
                Debug.Log("중복입니다.");
                return false;
            }
            count++;
        }

        return true;

    }
}
