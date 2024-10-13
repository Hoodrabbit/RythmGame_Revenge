using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashEvent : NoteEventScript
{

    BossMonster boss;
    public GameObject boss_Real;
    SpriteRenderer spriteRenderer;
    public Sprite bossSprite;

    bool IsTrigger = false;

    public bool Hit = false;
    float Songtime_value;


    protected override void Start()
    {
        base.Start();
       
    }



    protected override void Update()
    {

        if(boss != null)
        {
            if (!Hit)
            {

                //Debug.Log("타격 실패");
                //boss.Turnback();

                //if (boss != null && CheckGetOutofCamera())
                //{
                //    //보스 상태 해제
                //    boss.VisualizeBoss();
                //   // boss.Appear();

                //    IsTrigger = true;
                //}
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
        if (collision.tag == "Boss")
        {

            Debug.Log("보스 체크");


            if (!IsTrigger && !Used)
            {
                //보스 오브젝트에 돌진을 하라는 신호를 보냄
                boss = collision.GetComponent<BossMonster>();


                Songtime_value = (float)GetComponent<Note>().SongTime;
                //Audiotime_value = (float)GetComponent<Note>().AudioTime;
                boss.BossDash(transform,Songtime_value);

                Used = true;

                //boss_Real = boss.gameObject;

                //boss_Real.transform.parent = transform;
                ////boss_Real.transform.position = Vector3.zero;
                //bossSprite = boss.GetComponent<SpriteRenderer>().sprite;

                //spriteRenderer.sprite = bossSprite;
                //IsTrigger = true;
            }
            else
            {
                //역방향으로 노트가 이동하는 중일테고 
                //보스 콜라이더와 닿으면 
            }


            //anim



            //collision.transform.parent = transform;
            //collision.transform.localPosition = Vector3.zero;
        }

        if (collision.tag == "Judgement")
        {
            //Debug.Log("닿음");
          //  boss.Turnback();
            //어느 방향에서 닿았는지 체크해서 적용을 시킬지 안 시킬지 정하면 될 것 같음


        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 collisionPoint = contact.point;

        //if (collisionPoint.x < transform.position.x)
        //{
        //    Debug.Log("왼쪽에서 충돌");
        //}
        if (collisionPoint.x > transform.position.x && collision.gameObject.CompareTag("Judgement"))
        {
            Debug.Log("오른쪽에서 충돌");
            boss.Turnback();

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