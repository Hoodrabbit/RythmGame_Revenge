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

            //���� ������Ʈ�� ������ �϶�� ��ȣ�� ����
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




//public int TabCount; //�󸶳� ���� �� �ֵ��� ���������

//public float NoteLength; //���̸� �󸶷� ���� ���� �ϴ� �ð����� �����ϸ� ��

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