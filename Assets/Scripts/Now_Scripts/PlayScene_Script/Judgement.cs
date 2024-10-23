using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Judgement : MonoBehaviour
{
    [Header("위 아래 체크")]
    public JudgementHeight_State HEIGHT;
    [Space(10f)]

    public GameObject JudgeText;




    //나중에 따로 판정마다 스프라이트를 가지고 있는 스크립트 혹은 변수가 추가될 예정

    [Space ( 10f)]

    public KeyCode key;
    public KeyCode key2;
    public KeyCode Key3;


    public KeyCode ChangeWeaponKey;
    public KeyCode ChangeWeaponKey2;
    public KeyCode ChangeWeaponKey3;


    public MelodyType melody_type = MelodyType.Normal;


    public GameObject note;

    public List<Note> notes;
    Note LongNote;

    public bool longnotePress = false;
    bool LongNoteFail = false;

    float pressTime = 0;

    public static float PlayTime;
    public List<double> songtimes = new List<double>();
    AudioSource audioSource;


    LongNoteScript LScript;
    BossMonster BossNote;

    public Action<JudgementHeight_State> PressEvent_NoneHit;
    public Action<JudgementHeight_State> PressEvent_Hit;
    public Action<JudgementHeight_State> HoldingEvent;
    public Action<JudgementHeight_State> HoldingEndEvent;

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

        OperatingJudgeMent();




    }

    void OperatingJudgeMent()
    {
        if (Input.GetKeyDown(key) || Input.GetKeyDown(key2) || Input.GetKeyDown(Key3))
        {
           




            if (notes.Count > 0)
            {
                foreach (Note note in notes)
                {
                    if (note.melodyType == melody_type || note.melodyType == MelodyType.Normal && note != null)
                    {
                        if (ManageJudgeMent((note.transform.position.x + GameManager.Instance.OffsetValue - transform.position.x) / GameManager.Instance.speed))
                        {
                            LScript = note.GetComponent<LongNoteScript>();
                            BossNote = note.GetComponent<BossMonster>();

                            if (BossNote != null && LScript == null)
                            {
                                //note.HitNote();
                                Debug.Log("작동ㅇㅇㅇㅇㅇㅇㅇㅇㅇ");

                                BossNote.StopAllCoroutines();
                                BossNote.HitAction?.Invoke();
                                audioSource.Play();
                                PlayManager.Instance.HitNote(note);
                                
                                notes.Remove(note);
                            }
                            else if (BossNote == null && LScript == null)
                            {

                                if (LongNoteFail == true)
                                {
                                    LongNoteTail LNT = note.GetComponent<LongNoteTail>();
                                    if (LNT != null)
                                    {
                                        note.MissNote();
                                        PlayManager.Instance.MissNote();
                                    }
                                }

                                note.HitNote();
                                audioSource.Play();
                                Debug.Log("여기에서 발동");
                                PressEvent_Hit?.Invoke(HEIGHT);
                                //if (!LScript.n_Y.GetAlreadyHit())
                                //{
                                //    LScript.n_Y.HitNoteCheck();
                                PlayManager.Instance.HitNote(note);
                                //}
                                songtimes.Add(GameManager.Instance.MainAudio.time);

                            }
                            else
                            {
                                audioSource.Play();
                                longnotePress = true;
                                LongNote = note;
                                HoldingEvent?.Invoke(HEIGHT);
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
            else
            {
                PressEvent_NoneHit?.Invoke(HEIGHT);
            }
        }

        if (Input.GetKeyDown(ChangeWeaponKey) || Input.GetKeyDown(ChangeWeaponKey2) || Input.GetKeyDown(ChangeWeaponKey3))
        {
            SpriteRenderer SR = GetComponent<SpriteRenderer>();
            if (melody_type == MelodyType.Normal)
            {
                melody_type = MelodyType.Yellow;
                SR.color = Color.gray;
            }

            else if (melody_type == MelodyType.Yellow)
            {
                melody_type = MelodyType.Purple;
                SR.color = Color.black;
            }

            else if (melody_type == MelodyType.Purple)
            {
                melody_type = MelodyType.Yellow;
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
                LongNote = null;
                //notes.Remove(LongNote);
                pressTime = 0;
                PlayManager.Instance.MissNote();
                HoldingEndEvent?.Invoke(HEIGHT);
                //떼는 순간 완전히 찾지 못하도록 해야 될 것 같음

                //active = false;
            }


        }
        if (longnotePress == true)
        {




            if (LScript.Delete == false)
            {

                LScript.StopHeadPos(transform.position);
            }
            else
            {
                Debug.Log("꺼짐");

                PlayManager.Instance.HitLongNote();
                HoldingEndEvent?.Invoke(HEIGHT);

                longnotePress = false;
            }
        }
    }







    public bool ManageJudgeMent(double time)
    {
        Debug.Log(time);
        float f_time = Mathf.Abs((float)time);
        TMP_Text judgetext = Instantiate(JudgeText, Vector2.zero, Quaternion.identity, transform.GetComponentInChildren<Canvas>().gameObject.transform).GetComponent<TMP_Text>();
        judgetext.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 100);
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
        else if(f_time <= 0.10 && f_time > 0.04)
        {
            judgetext.text = "Great";
            Debug.Log("Great");

            return true;
        }
        else if(f_time > 0.1)
        {
            judgetext.text = "Miss";
            Debug.Log(f_time + "          " );


            //PlayerController.Instance.TakeHPMethod(100);


            return false;

        }

        return false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Note") )
        {
            //active = true;
            if(collision.GetComponent<LongNoteColliderAdjust>() == null)
            {
                notes.Add(collision.gameObject.GetComponent<Note>());
            }

        }

        if(collision.gameObject.CompareTag("Boss"))
        {
            Debug.Log("보스 노트 트리거 체크되는지 확인");
            notes.Add(collision.gameObject.GetComponent<Note>());
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
