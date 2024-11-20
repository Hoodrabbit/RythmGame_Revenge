using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashEvent : NoteEventScript
{

    BossMonster boss;
    public GameObject boss_Real;
    SpriteRenderer spriteRenderer;
    public Sprite bossSprite;

    Note note;


    public bool IsTrigger = false;

    public bool Hit = false;
    float Songtime_value;


    protected override void Start()
    {
        base.Start();
        note = GetComponent<Note>();
    }



    protected override void Update()
    {

        if(boss != null)
        {
            if (!Hit)
            {
            }
            else
            {
                    

                    boss_Real.transform.parent = null;

                    boss.Turnback();
                    boss.VisualizeBoss();

                    spriteRenderer.color = Color.clear;
                    gameObject.SetActive(false);
                



                //gameObject.SetActive(false);
            }
        }
        


        //transform.position = new Vector2(transform.position.x - 5 * Time.deltaTime, transform.position.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BossEventConverter"))
        {
            Debug.Log("할당");
            collision.GetComponent<BossStateQueue>().EnqueueInBossQueue(transform, (float)note.SongTime);
        }   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 collisionPoint = contact.point;


        //플레이 씬에서만 작동
        if (collisionPoint.x > transform.position.x && collision.gameObject.CompareTag("Judgement"))
        {
            Debug.Log("오른쪽에서 충돌");
            boss.Turnback();
            Used = false;
        }
    }




    //오브젝트가 화면을 벗어났는지를 확인해줄 메서드
    bool CheckGetOutofCamera()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    ////protected override void Awake()
    ////{
    ////    //base.Awake();
    ////}
    //protected override void Start()
    //{
    //    base.Start();
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //}






}




//public int TabCount; //얼마나 때릴 수 있도록 만들것인지

//public float NoteLength; //길이를 얼마로 잡을 건지 일단 시간으로 생각하면 됨

//LineRenderer lineRenderer;

//Vector2 StartPos;


//public bool StopBoss = false;


//private void Start()
//{
//    lineRenderer = GetComponent<LineRenderer>();
//    lineRenderer.SetPosition(0, transform.position);
//    lineRenderer.SetPosition(1, new Vector2(transform.position.x + NoteLength, transform.position.y));

//    StartPos = transform.position;

//}

//private void Update()
//{

//    //if(!StopBoss)
//    //{
//    //    transform.position = new Vector2(transform.position.x - 5 * Time.deltaTime, transform.position.y);
//    //    lineRenderer.SetPosition(0, transform.position);
//    //    lineRenderer.SetPosition(1, new Vector2(transform.position.x + NoteLength, transform.position.y));
//    //}





//}