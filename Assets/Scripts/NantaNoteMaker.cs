using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NantaNoteMaker : NoteMakerBase
{

    public GameObject NantaNote;
    public GameObject NoteEnd;
    public override GameObject Note { get => NantaNote; set => NantaNote = value; }


    bool EndCheck = false;


    private void OnEnable()
    {
        EndCheck = false;
        NoteEnd = null;
    }

    private void OnDisable()
    {
        if (NoteEnd != null)
        {
            if (EndCheck == true)
            {
                Destroy(NoteEnd.transform.parent.gameObject);
            }
        }
    }




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
                break;
            }
            i++;
        }
        i = 0;
        if (!Checkduplication)
        {
            while (i < hit.Length && !DeleteMode)
            {
                Vector2 InstantiatePos = new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + EditManager.OBSTACLE_DOWN);


                if (hit[i].collider.CompareTag("NotePlace") && Checkduplication == false)
                {

                    if(!EndCheck)
                    {
                        EndCheck = true;
                        GameObject AddNote = Instantiate(Note, InstantiatePos, Quaternion.identity, barNote.RhythmNote.transform);

                        float RealXpos = AddNote.transform.position.x - EditManager.Instance.GetNPXpos();
                        //위와 동일 

                        NoteEnd = AddNote.transform.GetChild(1).gameObject;

                        DataManager.Instance.EditNotes.Add(new NoteInfoAll(AddNote, RealXpos, EditManager.OBSTACLE_DOWN, NoteType, 1, (double)RealXpos / GameManager.Instance.speed));
                    }
                    else
                    {
                        if(NoteEnd != null)
                        {
                            if (hit[i].transform.position.x > NantaNote.transform.position.x) //기본적으로 꼬리는 머리보다 앞에 있거나 같은 위치에 있으면 안되기 때문에 해당 조건을 설정해줌
                            {
                                NoteEnd.transform.position = new Vector2(hit[i].transform.position.x, NantaNote.transform.position.y-2); //꼬리 위치 지정

                                EndCheck = false; //다시 머리 생성해주기 위해 false로 값 변경

                                float RealXpos = NoteEnd.transform.position.x - EditManager.Instance.GetNPXpos();

                                DataManager.Instance.EditNotes.Add(new NoteInfoAll(NoteEnd, RealXpos, EditManager.OBSTACLE_DOWN, NoteType, 2, (double)RealXpos / GameManager.Instance.speed));

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
                //Debug.Log("작동" + hit[i].collider.name);


                if (hit[i].collider.CompareTag("Note") || hit[i].collider.CompareTag("NoteEnd"))
                {
                    Transform NantaTransform = hit[i].collider.transform;
                    NantaNote Nanta = NantaTransform.parent.GetComponent<NantaNote>();
                    if (Nanta != null)
                    {
                        //  Debug.Log(longNoteScript.gameObject.name);
                        Destroy(Nanta.gameObject);
                    }
                    else
                    {
                        Destroy(hit[i].collider.gameObject);
                    }


                }
                i++;
            }
        }





    }

}
