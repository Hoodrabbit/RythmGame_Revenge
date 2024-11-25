using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackNoteMaker : Normal_Note_Maker
{
    protected override void AreaCheck(GameObject Note, Vector2 Pos, bool DeleteMode)
    {

        //hit = Physics2D.BoxCastAll(Pos, new Vector2(2, 50), 0, transform.forward,10);

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

            i++;
        }
        i = 0;
        while (i < hit.Length && !DeleteMode)
        {

            if (!Checkduplication)
            {

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
                            AddNote = Instantiate(NormalNote_UP, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform);
                        }
                        else
                        {
                            AddNote = Instantiate(Note, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform);
                        }




                        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                        //슬라이더로 값을 옮기면서 해당 위치가 계속해서 변하기 때문에 변하더라도 유동적으로 대응할 수 있도록 코드 추가

                        Debug.Log("노트 생성");

                        AddNote.GetComponent<Note>().SongTime = (double)RealXpos / GameManager.Instance.speed;
                        DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, (int)InstantiatePos.y, NoteType, 0, (double)RealXpos / GameManager.Instance.speed));
                        //변경해야 됨 현재 로직 변경함


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
                if (hit[i].collider.CompareTag("Note"))
                {
                    Destroy(hit[i].transform.gameObject);
                }
                i++;
            }
        }
    }
}
