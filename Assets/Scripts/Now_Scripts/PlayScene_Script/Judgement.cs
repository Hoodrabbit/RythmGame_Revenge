using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Judgement : MonoBehaviour
{

    public GameObject JudgeText;

    //나중에 따로 판정마다 스프라이트를 가지고 있는 스크립트 혹은 변수가 추가될 예정

    [Space ( 10f)]

    public KeyCode key;
    public KeyCode key2;
    public KeyCode Key3;


    public KeyCode ChangeWeaponKey;
    public KeyCode ChangeWeaponKey2;
    public KeyCode ChangeWeaponKey3;


    public MelodyType type = MelodyType.Normal;


    public GameObject note;

    public List<Note> notes;
    Note LongNote;

    //public List<JudgeMentDummy> JudgeMentsColliders;

    bool active = false;

    bool AlreadyDelete = false;


    public bool longnotePress = false;
    bool LongNoteFail = false;

    float pressTime = 0;

    //public string txt;
    //public TMP_Text judgeText;



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

        //오토모드 임시
        //if (notes.Count > 0)
        //{
        //    foreach (Note note in notes)
        //    {
        //        if (note.SongTime == GameManager.Instance.MainAudio.time)
        //        {
        //            LScript = note.GetComponent<LongNoteScript>();

        //            if (LScript == null)
        //            {
        //                note.HitNote();
        //                audioSource.Play();
        //                PlayManager.Instance.HitNote();
        //                songtimes.Add(GameManager.Instance.MainAudio.time);
        //                break;
        //            }
        //            else
        //            {


        //                if (longnotePress == false)
        //                {
        //                    audioSource.Play();
        //                    songtimes.Add(GameManager.Instance.MainAudio.time);
        //                }

        //                longnotePress = true;
        //                break;
        //            }




        //        }


        //    }
        //}





        if (Input.GetKeyDown(key) || Input.GetKeyDown(key2) || Input.GetKeyDown(Key3))
        {
            
            if (notes.Count > 0)
            {
                foreach (Note note in notes)
                {
                    if (note.Type == type || note.Type == MelodyType.Normal && note != null)
                    {
                        if (ManageJudgeMent((note.transform.position.x+ GameManager.Instance.OffsetValue - transform.position.x)/GameManager.Instance.speed))
                        {
                            LScript = note.GetComponent<LongNoteScript>();

                            if (LScript == null)
                            {

                                if(LongNoteFail == true)
                                {
                                    LongNoteTail LNT = note.GetComponent<LongNoteTail>();
                                    if(LNT != null)
                                    {
                                        note.MissNote();
                                        PlayManager.Instance.MissNote();
                                    }
                                }

                                note.HitNote();
                                audioSource.Play();
                                PlayManager.Instance.HitNote();
                                songtimes.Add(GameManager.Instance.MainAudio.time);

                            }
                            else
                            {
                                audioSource.Play();
                                longnotePress = true;
                                LongNote = note;
                                songtimes.Add(GameManager.Instance.MainAudio.time);
                            }

                            break;
                        }
                        else
                        {
                            note.MissNote();
                            Debug.Log("미스났어요" + +note.SongTime + "      " + GameManager.Instance.MainAudio.time);
                            PlayManager.Instance.MissNote();
                            break;
                        }


                    }

                }


            }
        }

        if(Input.GetKeyDown(ChangeWeaponKey) ||  Input.GetKeyDown(ChangeWeaponKey2) || Input.GetKeyDown(ChangeWeaponKey3))
        {
            SpriteRenderer SR = GetComponent<SpriteRenderer>();
            if (type == MelodyType.Normal)
            {
                type = MelodyType.White;
                SR.color = Color.gray;
            }

            else if(type == MelodyType.White) 
            {
                type = MelodyType.Dark;
                SR.color = Color.black;
            }

            else if(type == MelodyType.Dark)
            {
                type = MelodyType.White;
                SR.color = Color.gray;
            }




        }


        if (Input.GetKeyUp(key))
        {
            if (longnotePress == true)
            {
                longnotePress = false;
                LongNote.SetAudioTime();
                LScript.CancelStopHeadPos();
                LongNote.MissNote();
                LongNote= null;
                //notes.Remove(LongNote);
                pressTime = 0;
                PlayManager.Instance.MissNote();
                //떼는 순간 완전히 찾지 못하도록 해야 될 것 같음

                //active = false;
            }


        }
        if (longnotePress == true)
        {
            



            if (LScript.Delete == false)
            {

                    LScript.StopHeadPos(transform.position);
                

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


    public bool ManageJudgeMent(double time)
    {
        Debug.Log(time);
        float f_time = Mathf.Abs((float)time);
        TMP_Text judgetext = Instantiate(JudgeText, Vector3.zero, Quaternion.identity, transform.GetComponentInChildren<Canvas>().gameObject.transform).GetComponent<TMP_Text>();
        if (f_time <= 0.05)
        {
           

            //정확한 판정을 켰을 경우
            if (f_time <= 0.04)
            {
                judgetext.text = "Perfect";
                Debug.Log("Perfect");

            }

            return true;

        }
        else if(f_time < 0.10 && f_time > 0.05)
        {
            judgetext.text = "Great";
            Debug.Log("Great");

            return true;
        }
        else if(f_time >= 0.1)
        {
            judgetext.text = "Miss";
            Debug.Log(f_time + "          " );
            return false;

        }

        return false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Note"))
        {
            //active = true;
            if(collision.GetComponent<LongNoteColliderAdjust>() == null)
            {
                notes.Add(collision.gameObject.GetComponent<Note>());
            }

        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {

            if(notes.Count > 0)
            {
                notes.RemoveAt(0);
            }
                

        }
    }
}
