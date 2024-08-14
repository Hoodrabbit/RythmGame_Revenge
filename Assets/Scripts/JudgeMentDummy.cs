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

//Perfect : Ÿ�ֿ̹� �°� �Ϻ��ϰ� ģ ���

//Great :  ���� �´� ���

//Good : �ָ��ϰ� ������ ��� 

//Miss : ������ Ʋ�� ��� 

//Null : ����� ���� ���� ����(���� �Ÿ��� ������ ����)

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
            Debug.Log(gameObject.name + "���� ����");
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
                //�뷡�� 16��Ʈ���� �޺� ������Ű��

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


