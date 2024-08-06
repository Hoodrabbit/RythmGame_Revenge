using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Long_Note_Maker : NoteMakerBase
{
    public GameObject Head;
    public GameObject Tail;

    Vector2 HeadPos; //머리위치 측정해줌

    public bool TailOn = false;

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
                if (Pos.y > 0)
                {

                    if(TailOn == false)
                    {

                        GameObject LongNote; //생성한 노트에 대한 정보를 잠시 사용해야 해서 만들어준 오브젝트입니다. 나중에는 아마도 list에다가 정보를 전부 저장할 것 같네요.

                        LongNote = Instantiate(Head, new Vector3(hit[i].transform.position.x, hit[i].transform.position.y + 2), Quaternion.identity, barNote.RhythmNote.transform); //머리 생성

                        HeadPos = new Vector2(LongNote.transform.position.x, LongNote.transform.position.y); //머리 위치 할당

                        Tail = LongNote.transform.GetChild(1).gameObject; //꼬리는 머리 오브젝트의 2번째 하위 오브젝트로 위치해 있기 때문에 이렇게 작성함

                        TailOn = true; //머리 노트 생성했으니 이제 꼬리 위치 지정해야 하니까 true로 값 변경

                        float RealXpos = LongNote.transform.position.x - EditManager.Instance.GetNPXpos();
                        //슬라이더로 값을 옮기면서 해당 위치가 계속해서 변하기 때문에 변하더라도 유동적으로 대응할 수 있도록 코드 추가

                        DataManager.Instance.EditNotes.Add(new NoteInfoAll(LongNote, RealXpos, 1, 2, 1));
                    }
                    else
                    {
                        if(Tail != null)
                        {
                            if (hit[i].transform.position.x > HeadPos.x) //기본적으로 꼬리는 머리보다 앞에 있거나 같은 위치에 있으면 안되기 때문에 해당 조건을 설정해줌
                            {
                                Tail.transform.position = new Vector2(hit[i].transform.position.x, HeadPos.y); //꼬리 위치 지정

                                TailOn = false; //다시 머리 생성해주기 위해 false로 값 변경

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
                        GameObject LongNote; //생성한 노트에 대한 정보를 잠시 사용해야 해서 만들어준 오브젝트입니다. 나중에는 아마도 list에다가 정보를 전부 저장할 것 같네요.

                        LongNote = Instantiate(Head, new Vector3(hit[i].transform.position.x, hit[i].transform.position.y - 2), Quaternion.identity, barNote.RhythmNote.transform); //머리 생성

                        HeadPos = new Vector2(LongNote.transform.position.x, LongNote.transform.position.y); //머리 위치 할당

                        Tail = LongNote.transform.GetChild(1).gameObject; //꼬리는 머리 오브젝트의 2번째 하위 오브젝트로 위치해 있기 때문에 이렇게 작성함

                        TailOn = true; //머리 노트 생성했으니 이제 꼬리 위치 지정해야 하니까 true로 값 변경

                        float RealXpos = LongNote.transform.position.x - EditManager.Instance.GetNPXpos();

                        DataManager.Instance.EditNotes.Add(new NoteInfoAll(LongNote, RealXpos, 2, 2, 1));


                    }
                    else
                    {
                        if (Tail != null)
                        {
                            //Debug.Log("가끔씩 안됨");
                            if (hit[i].transform.position.x > HeadPos.x) //기본적으로 꼬리는 머리보다 앞에 있거나 같은 위치에 있으면 안되기 때문에 해당 조건을 설정해줌
                            {
                                Tail.transform.position = new Vector2(hit[i].transform.position.x, HeadPos.y); //꼬리 위치 지정

                                TailOn = false; //다시 머리 생성해주기 위해 false로 값 변경

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
                //Debug.Log("작동" + hit[i].collider.name);


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
