using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossDash : Note
{
    //protected override void Awake()
    //{
    //    //base.Awake();
    //}
    //protected override void Start()
    //{
    //    //base.Start();
    //}
    //protected override void FixedUpdate()
    //{
    //    //transform.position = new Vector2(transform.position.x - 5 * Time.deltaTime, transform.position.y);

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Boss")
        {

            //보스 오브젝트에 돌진을 하라는 신호를 보냄
            BossMonster boss = collision.GetComponent<BossMonster>();
            boss.BossDash(this.transform);

            //anim



            //collision.transform.parent = transform;
            //collision.transform.localPosition = Vector3.zero;
        }

        if(collision.tag == "Judgement")
        {





        }


    }





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