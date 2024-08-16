using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Judgement : MonoBehaviour
{
    public KeyCode key;
    public KeyCode key2;

    public KeyCode ChangeWeaponKey;
    public KeyCode ChangeWeaponKey2;
    public KeyCode ChangeWeaponKey3;


    public NoteType type = NoteType.Normal;


    public GameObject note;

    public List<Note> notes;

    public List<JudgeMentDummy> JudgeMentsColliders;

    bool active = false;

    public bool longnotePress = false;
    float pressTime = 0;

    //public string txt;
    public TMP_Text judgeText;



    public static float PlayTime;
    public List<double> songtimes = new List<double>();
    AudioSource audioSource;
    LongNoteScript LScript;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //Debug.Log("PlayTime : " + PlayTime);
        Debug.Log(GameManager.Instance.GetBPS());
        // InitalizeJudgeMents();

        ChangeWeaponKey = KeyCode.Space;
        ChangeWeaponKey2 = KeyCode.UpArrow;
        ChangeWeaponKey3 = KeyCode.DownArrow;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) || Input.GetKeyDown(key2))
        {

            if (notes.Count > 0)
            {
                foreach (Note note in notes)
                {
                    //Debug.Log(Mathf.Abs((float)(note.SongTime - GameManager.Instance.MainAudio.time)));

                    if (note.Type == type || note.Type == NoteType.Normal)
                    {
                        if (ManageJudgeMent(Mathf.Abs((float)(note.SongTime - GameManager.Instance.MainAudio.time))))
                        {
                            LScript = note.GetComponent<LongNoteScript>();

                            if (LScript == null)
                            {
                                note.gameObject.SetActive(false);
                                audioSource.Play();
                                //        songtimes.Add(GameManager.Instance.MainAudio.time);
                                PlayManager.Instance.HitNote();


                            }
                            else
                            {
                                audioSource.Play();
                                longnotePress = true;

                                //songtimes.Add(GameManager.Instance.MainAudio.time);


                            }

                            break;
                        }
                        else
                        {
                            note.gameObject.SetActive(false);
                            Debug.Log("미스났어요" + +note.SongTime + "      " + GameManager.Instance.MainAudio.time);
                            PlayManager.Instance.MissNote();
                            break;
                        }


                    }

                }


            }





            //foreach(var judgementCollider in JudgeMentsColliders)
            //{
            //    judgementCollider.bboxcollider2D.enabled = true;
            //    if(judgementCollider.GetState() != JudgeMentState.Null)
            //    {
            //        txt = judgementCollider.GetState().ToString();
            //        Debug.Log(txt);
            //        if(judgementCollider.IsLongNote())
            //        {
            //            Debug.Log("롱노트에요");
            //            longnotePress = true;
            //        }
            //        else
            //        {
            //            foreach (var obj in JudgeMentsColliders)
            //            {
            //                if (obj.bboxcollider2D.enabled == true)
            //                {
            //                    obj.bboxcollider2D.enabled = false;
            //                }
            //            }
            //            break;
            //        }
            //    }


            // }




            //if (active == true)
            //{
            //    aa = note.GetComponent<LongNoteScript>();

            //    if (aa == null && longnotePress == false)
            //    {

            //        //Debug.Log("내가 눌러서 작동");
            //        note.SetActive(false);
            //        note = null;
            //        audioSource.Play();
            //        songtimes.Add(GameManager.Instance.MainAudio.time);
            //        PlayManager.Instance.HitNote();
            //    }
            //    else
            //    {
            //        Debug.Log("작동하나요");
            //        aa.StopHeadPos(transform.position);
            //        longnotePress = true;
            //        songtimes.Add(GameManager.Instance.MainAudio.time);
            //    }

            //}




            //미스는 다른 방식으로
        }

        if(Input.GetKeyDown(ChangeWeaponKey) ||  Input.GetKeyDown(ChangeWeaponKey2) || Input.GetKeyDown(ChangeWeaponKey3))
        {
            SpriteRenderer SR = GetComponent<SpriteRenderer>();
            if (type == NoteType.Normal)
            {
                type = NoteType.White;
                SR.color = Color.gray;
            }

            else if(type == NoteType.White) 
            {
                type = NoteType.Dark;
                SR.color = Color.black;
            }

            else if(type == NoteType.Dark)
            {
                type = NoteType.White;
                SR.color = Color.gray;
            }




        }


        if (Input.GetKeyUp(key))
        {
            if (longnotePress == true)
            {
                longnotePress = false;
                LScript.CancelStopHeadPos();
                pressTime = 0;
                //떼는 순간 완전히 찾지 못하도록 해야 될 것 같음

                //active = false;
            }


        }
        if (longnotePress == true)
        {
            if (Vector2.Distance(LScript.transform.position, transform.position) <= 0.4f)
            {
                LScript.StopHeadPos(transform.position);

                
            }



            if (LScript.gameObject.activeSelf == true)
            {
                pressTime += Time.deltaTime;

                if (pressTime >= GameManager.Instance.GetBPS() / 2)
                {
                    PlayManager.Instance.HitNote();
                    pressTime -= GameManager.Instance.GetBPS() / 2;
                }
            }
            else
            {
                Debug.Log("꺼짐");
                longnotePress = false;
            }

            
            //노래의 16비트마다 콤보 증가시키기

            //if()

        }


    }


    public bool ManageJudgeMent(float time)
    {
        Debug.Log(time);
        if (time <= 0.05)
        {
            judgeText.text = "Perfect";
            Debug.Log("Perfect");

            return true;

        }
        else if(time <= 0.13 && time >0.05)
        {
            judgeText.text = "Great";
            Debug.Log("Great");

            return true;
        }
        else if(time > 0.15)
        {
            Debug.Log(time + "          " );
            judgeText.text = "Miss!";

            return false;
            //Debug.Log("Miss");
            //미스가 일어나면 일정 시간 후 무조건 바로 꺼져야 함 아니면 미스 표시가 아예 안뜸
        }

        return false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Note"))
        {
            //active = true;

            notes.Add(collision.gameObject.GetComponent<Note>());

            //note = collision.gameObject;



        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {
            //active = true;
            //judgeText.text = "Miss!";
            notes.RemoveAt(0);
            //제일 처음 노트부터 사라져야 하기 때문에 작동됨


            //note = collision.gameObject;



        }
    }

    void InitalizeJudgeMents()
    {
        foreach (var obj in JudgeMentsColliders)
        {
            if (obj.bboxcollider2D.enabled == true)
            {
                obj.bboxcollider2D.enabled = false;
            }
        }
    }



}
