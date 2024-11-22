using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;




public struct HitSoundChecker
{
    double songtimes;
    bool IsHit;


    public HitSoundChecker(double songtime)
    {
        songtimes = songtime;
        IsHit = false;
    }

    public void Hit()
    {
        IsHit = true;
    }

    public double GetSongTime()
    {
        return songtimes;
    }

    public bool IsHitCheck()
    {
        return IsHit;
    }


}







public class Judgement : MonoBehaviour
{
    [Header("위 아래 체크")]
    public JudgementHeight_State HEIGHT;
    [Space(10f)]

    public GameObject JudgeText;
    public GameObject HitImage;



    //나중에 따로 판정마다 스프라이트를 가지고 있는 스크립트 혹은 변수가 추가될 예정

    [Space(10f)]

    public KeyCode key;
    public KeyCode key2;
    public KeyCode Key3;


    public KeyCode ChangeWeaponKey;
    public KeyCode ChangeWeaponKey2;
    public KeyCode ChangeWeaponKey3;


    public MelodyType melody_type = MelodyType.Normal;


    public GameObject note;


    public SharedNoteList sharedList;


    public List<Note> notes;
    Note LongNote;

    public bool longnotePress = false;
    float longnoteTime = 0;
    bool LongNoteFail = false;

    bool NantaStart = false;


    float pressTime = 0;

    public static float PlayTime;
    //public List<double> songtimes = new List<double>();

    public List<HitSoundChecker> hitSoundCheckers = new List<HitSoundChecker>();
    HitSoundChecker hit;
    AudioSource audioSource;


    LongNoteScript LScript;
    BossMonster BossNote;
    NantaNote nantaNote;

    HitParticlePooling ActivatingParticle;


    public Action<JudgementHeight_State> PressEvent_NoneHit;
    public Action<JudgementHeight_State> PressEvent_Hit;
    public Action<JudgementHeight_State> HoldingEvent;
    public Action<JudgementHeight_State> HoldingEndEvent;
    public Action NantaHit;


    // Start is called before the first frame update
    void Start()
    {
        sharedList = PlayManager.Instance.GetComponent<SharedNoteList>();
        notes = sharedList.GetNoteList(transform.position.y);


        audioSource = GetComponent<AudioSource>();
        ActivatingParticle = GetComponent<HitParticlePooling>();
        //Debug.Log("PlayTime : " + PlayTime);
        //Debug.Log(GameManager.Instance.GetBPS());
        // InitalizeJudgeMents();

        ChangeWeaponKey = KeyCode.Space;
        ChangeWeaponKey2 = KeyCode.UpArrow;
        ChangeWeaponKey3 = KeyCode.DownArrow;


    }

    // Update is called once per frame
    void Update()
    {
        if (AudioListener.pause == false &&
           GameManager.Instance.MainAudio.isPlaying)
        {
            OperatingJudgeMent();
        }

    }

    void OperatingJudgeMent()
    {
        float offsetValue = 0;
        if (Input.GetKeyDown(key) || Input.GetKeyDown(key2) || Input.GetKeyDown(Key3))
        {

            if (notes.Count > 0)
            {
                foreach (Note note in notes)
                {
                    if (note.melodyType == melody_type || note.melodyType == MelodyType.Normal && note != null)
                    {

                        offsetValue = (note.transform.position.x + GameManager.Instance.OffsetValue - transform.position.x) / GameManager.Instance.speed;


                        LScript = note.GetComponent<LongNoteScript>();
                        BossNote = note.GetComponent<BossMonster>();
                        nantaNote = note.GetComponent<NantaNote>();

                        if (nantaNote != null)
                        {
                            //난타노트의 트리거 온
                            //따로 누를때마다 히트 체크를 해주도록 만들어줘야 함
                            //



                            if (!NantaStart)
                            {
                                NantaStart = true;
                                nantaNote.NantaStart();
                                nantaNote.HitNantaNote();
                                nantaNote.StopNoteMethod();
                                HitText();

                                NantaHit?.Invoke();
                            }
                            else
                            {
                                nantaNote.HitNantaNote();
                                NantaHit?.Invoke();
                                HitText();
                            }
                            break;
                        }

                        if (BossNote != null && LScript == null)
                        {
                            BossNote.StopAllCoroutines();
                            BossNote.HitAction?.Invoke();
                            audioSource.Play();
                            PressEvent_Hit?.Invoke(HEIGHT);
                            PlayManager.Instance.HitNote(note);
                            sharedList.DeleteBossNote();
                            //NantaHit?.Invoke();
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
                                    HoldingEndEvent?.Invoke(HEIGHT);
                                    PlayManager.Instance.MissNote();
                                }
                            }
                            if (!longnotePress)
                            {
                                note.HitNote();
                                audioSource.Play();


                                PressEvent_Hit?.Invoke(HEIGHT);
                                Instantiate_JudgeText(offsetValue);
                                PlayManager.Instance.HitNote(note);
                                //songtimes.Add(GameManager.Instance.MainAudio.time);
                            }


                        }
                        else
                        {
                            audioSource.Play();
                            longnoteTime = 0;
                            longnotePress = true;
                            LongNote = note;
                            LongNote.LongHit();
                            HoldingEvent?.Invoke(HEIGHT);
                            Instantiate_JudgeText(offsetValue);
                            //songtimes.Add(GameManager.Instance.MainAudio.time);
                            float time = (float)LongNote.SongTime;
                            LScript.StopHeadPos(time);
                        }

                        break;
                    }
                    else
                    {
                        Miss();
                        note.MissNote();
                        GameManager.Instance.Increase_Miss();
                        //audioSource.Stop();
                        Debug.Log("미스났어요" + +note.SongTime + "      " + GameManager.Instance.MainAudio.time);
                        PlayManager.Instance.MissNote();
                        break;
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
            //SpriteRenderer SR = GetComponent<SpriteRenderer>();
            if (melody_type == MelodyType.Normal)
            {
                melody_type = MelodyType.Yellow;
                //SR.color = Color.gray;
            }

            else if (melody_type == MelodyType.Yellow)
            {
                melody_type = MelodyType.Purple;
                //SR.color = Color.black;
            }

            else if (melody_type == MelodyType.Purple)
            {
                melody_type = MelodyType.Yellow;
                //SR.color = Color.gray;
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
                HoldingEndEvent?.Invoke(HEIGHT);
                //notes.Remove(LongNote);

                Miss();
                GameManager.Instance.Increase_Miss();


                pressTime = 0;
                PlayManager.Instance.MissNote();

                //떼는 순간 완전히 찾지 못하도록 해야 될 것 같음

                //active = false;
            }


        }
        if (longnotePress == true)
        {
            //롱노트 
            longnoteTime += Time.deltaTime;

            if (longnoteTime > 0.3)
            {
                HoldingText();
                PlayManager.Instance.HoldingLongNote();

                longnoteTime -= 0.3f;
            }



            if (LScript.Delete == true)
            {
                //Debug.Log("꺼짐");
                audioSource.Play();
                HoldingEndEvent?.Invoke(HEIGHT);
                Instantiate_JudgeText(offsetValue);
                PlayManager.Instance.HitLongNote();


                longnotePress = false;
            }
        }

        if (notes.Count == 0)
        {
            NantaStart = false;
        }

    }

    public void Miss()
    {
        TMP_Text judgetext = Instantiate(JudgeText, Vector2.zero, Quaternion.identity, transform.GetComponentInChildren<Canvas>().gameObject.transform).GetComponent<TMP_Text>();
        judgetext.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 100);
        Color32 color_UP = HexToColor32("#bcbbbf");
        Color32 color_Down = HexToColor32("#bcbbbf");
        VertexGradient gradient = judgetext.colorGradient;

        gradient.topLeft = color_UP;
        gradient.topRight = color_UP;
        gradient.bottomLeft = color_Down;
        gradient.bottomRight = color_Down;

        // 변경된 ColorGradient 적용
        judgetext.colorGradient = gradient;
        judgetext.text = "Miss";
    }


    public void HoldingText()
    {
        TMP_Text judgetext = Instantiate(JudgeText, Vector2.zero, Quaternion.identity, transform.GetComponentInChildren<Canvas>().gameObject.transform).GetComponent<TMP_Text>();
        judgetext.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 100);
        Color32 color_UP = HexToColor32("#fbbe1f");
        Color32 color_Down = HexToColor32("#fa4e03");
        VertexGradient gradient = judgetext.colorGradient;

        gradient.topLeft = color_UP;
        gradient.topRight = color_UP;
        gradient.bottomLeft = color_Down;
        gradient.bottomRight = color_Down;

        // 변경된 ColorGradient 적용
        judgetext.colorGradient = gradient;
        judgetext.text = "Holdling";

    }

    public void HitText()
    {
        TMP_Text judgetext = Instantiate(JudgeText, Vector2.zero, Quaternion.identity, transform.GetComponentInChildren<Canvas>().gameObject.transform).GetComponent<TMP_Text>();
        judgetext.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 100);

        Color32 color_UP = HexToColor32("#9229ec");
        Color32 color_Down = HexToColor32("#5e12f9");
        VertexGradient gradient = judgetext.colorGradient;

        gradient.topLeft = color_UP;
        gradient.topRight = color_UP;
        gradient.bottomLeft = color_Down;
        gradient.bottomRight = color_Down;

        // 변경된 ColorGradient 적용
        judgetext.colorGradient = gradient;


        judgetext.text = "Hit";
    }


    void Instantiate_JudgeText(double time)
    {

        if (!longnotePress)
        {
            float f_time = Mathf.Abs((float)time);
            TMP_Text judgetext = Instantiate(JudgeText, Vector2.zero, Quaternion.identity, transform.GetComponentInChildren<Canvas>().gameObject.transform).GetComponent<TMP_Text>();
            judgetext.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 100);


            //정확한 판정을 켰을 경우
            if (f_time <= 0.04)
            {


                Color32 Left_UP = HexToColor32("#FC0174");
                Color32 Right_Up = HexToColor32("#B034E4");
                Color32 Left_Down = HexToColor32("#8EA5FEFF");
                Color32 Right_Down = HexToColor32("#0070E0FF");
                VertexGradient gradient = judgetext.colorGradient;

                gradient.topLeft = Left_UP;
                gradient.topRight = Right_Up;
                gradient.bottomLeft = Left_Down;
                gradient.bottomRight = Right_Down;

                // 변경된 ColorGradient 적용
                judgetext.colorGradient = gradient;



                judgetext.text = "Perfect";

                GameManager.Instance.Increase_Perfect();
            }

            //return true;


            else if (f_time > 0.04)
            {

                Color32 Left_UP = HexToColor32("#0047b1");
                Color32 Right_Up = HexToColor32("#0047b1");
                Color32 Left_Down = HexToColor32("#00b6f9");
                Color32 Right_Down = HexToColor32("#00b6f9");
                VertexGradient gradient = judgetext.colorGradient;

                gradient.topLeft = Left_UP;
                gradient.topRight = Right_Up;
                gradient.bottomLeft = Left_Down;
                gradient.bottomRight = Right_Down;

                // 변경된 ColorGradient 적용
                judgetext.colorGradient = gradient;





                judgetext.text = "Great";
                GameManager.Instance.Increase_Great();
            }

            //return false;
        }
        //return false;
    }


    public bool ManageJudgeMent(double time)
    {
        //if(Mathf.Abs((float)time) <= 0.05)
        //{
        return true;
        //}
        //return false;

    }

    IEnumerator DelayHitSound(float time, HitSoundChecker hhit)
    {

        yield return new WaitForSeconds(time);
        Debug.Log("사운드 실행");
        audioSource.PlayOneShot(audioSource.clip);


    }

    Color32 HexToColor32(string hex)
    {
        hex = hex.Replace("#", "");
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Note"))
        {
            //active = true;
            if (collision.GetComponent<LongNoteColliderAdjust>() == null)
            {
                Note note = collision.gameObject.GetComponent<Note>();


                notes.Add(note);






            }

        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            //Debug.Log("보스 노트 트리거 체크되는지 확인");
            notes.Add(collision.gameObject.GetComponent<Note>());
        }




    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Note note_hitcheck;
        if (collision.gameObject.CompareTag("Note"))
        {
            Note note_hitcheck = collision.gameObject.GetComponent<Note>();

            if(note_hitcheck != null) 
            {
                if (!collision.GetComponent<Note>().GetAlreadyHit())
                {
                    Miss();
                }
            }
            
            
            if (notes.Count > 0)
            {
                notes.RemoveAt(0);
            }
           
        }

        if(collision.gameObject.CompareTag("Boss"))
        {
            
        }



    }
}
