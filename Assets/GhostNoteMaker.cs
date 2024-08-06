using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class GhostNoteMaker : NoteMakerBase
{
    public GameObject GhostNote;

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
            }

            if (hit[i].collider.CompareTag("NotePlace") && Checkduplication == false)
            {

                //3
                if (Pos.y > 4)
                {
                    GameObject AddNote = Instantiate(GhostNote, new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + 6), Quaternion.identity, barNote.RhythmNote.transform);

                    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                    //위와 동일 

                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, 3, 3, 0));
                }

                //1
                else if (Pos.y < 3 && Pos.y > 0)
                {
                    GameObject AddNote = Instantiate(GhostNote, new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + 2), Quaternion.identity, barNote.RhythmNote.transform);

                    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                    //슬라이더로 값을 옮기면서 해당 위치가 계속해서 변하기 때문에 변하더라도 유동적으로 대응할 수 있도록 코드 추가


                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, 1, 3, 0));
                }

                //2
                else if (Pos.y > -3 && Pos.y < 0)
                {
                    GameObject AddNote = Instantiate(GhostNote, new Vector3(hit[i].transform.position.x, hit[i].transform.position.y - 2), Quaternion.identity, barNote.RhythmNote.transform);

                    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                    //위와 동일 

                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, 2, 3, 0));
                }

                //4
                else
                {
                    GameObject AddNote = Instantiate(GhostNote, new Vector3(hit[i].transform.position.x, hit[i].transform.position.y - 6), Quaternion.identity, barNote.RhythmNote.transform);

                    float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                    //위와 동일 

                    DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, 4, 3, 0));
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

}
