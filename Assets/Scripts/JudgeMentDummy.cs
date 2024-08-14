using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GlobalEnum
{ 
    Perfect,
    
    Great,

    Good,

    Miss,

    Null
}

//Perfect : 타이밍에 맞게 완벽하게 친 경우

//Great :  거의 맞는 경우

//Good : 애매하게 근접한 경우 

//Miss : 완전히 틀린 경우 

//Null : 제대로 받지 못한 상태(일정 거리에 들어오지 않음)

public class JudgeMentDummy : MonoBehaviour
{
    public JudgeMentState JState;

    public BoxCollider2D bboxcollider2D;

    bool check = false;

    float pressTime = 0;

    public GameObject Note_Press;
    bool IsLong = false;


    public List<double> PressTime = new List<double>();

    public void Awake()
    {
        //bboxcollider2D = GetComponent<BoxCollider2D>();
    }

    public void Update()
    {
        if(bboxcollider2D.enabled == true)
        {
            Debug.Log(gameObject.name + "에서 켜짐");
        }
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(GetState());
        if (collision.CompareTag("Note"))
        {
            Note_Press = collision.gameObject;
            Debug.Log(Note_Press.name);
            check = true;
            LongNoteScript LnoteScript = Note_Press.GetComponent<LongNoteScript>();
            if (LnoteScript != null) 
            {
                IsLong = true;
                LnoteScript.StopHeadPos(transform.position);
                PlayManager.Instance.HitNote();
                PressTime.Add(GameManager.Instance.MainAudio.time);
                //LnoteScript.StopHeadPos(transform.position);




                //        longnotePress = true;
                //        songtimes.Add(GameManager.Instance.MainAudio.time);

            }
            else
            {
                PlayManager.Instance.HitNote();
                collision.gameObject.SetActive(false);
            }

            
            


        }

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (IsLong == true)
            {
                pressTime += Time.deltaTime;

                if (pressTime >= GameManager.Instance.GetBPS() / 2)
                {
                    PlayManager.Instance.HitNote();
                    pressTime -= GameManager.Instance.GetBPS() / 2;
                }
                //노래의 16비트마다 콤보 증가시키기

                //if()

            }




        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (IsLong == true)
            {
                IsLong = false;
                pressTime = 0;
            }




        }
    }


    public JudgeMentState GetState()
    { 
        if(check)
        {
            return JudgeMentState.Null;
        }
        else
        {
            check = false;
            return JState;
        }

    }


    public bool IsLongNote()
    {
        if(IsLong)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }




}


