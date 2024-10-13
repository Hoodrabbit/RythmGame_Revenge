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

                //Debug.Log("Ÿ�� ����");
                //boss.Turnback();

                //if (boss != null && CheckGetOutofCamera())
                //{
                //    //���� ���� ����
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

            Debug.Log("���� üũ");


            if (!IsTrigger && !Used)
            {
                //���� ������Ʈ�� ������ �϶�� ��ȣ�� ����
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
                //���������� ��Ʈ�� �̵��ϴ� �����װ� 
                //���� �ݶ��̴��� ������ 
            }


            //anim



            //collision.transform.parent = transform;
            //collision.transform.localPosition = Vector3.zero;
        }

        if (collision.tag == "Judgement")
        {
            //Debug.Log("����");
          //  boss.Turnback();
            //��� ���⿡�� ��Ҵ��� üũ�ؼ� ������ ��ų�� �� ��ų�� ���ϸ� �� �� ����


        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 collisionPoint = contact.point;

        //if (collisionPoint.x < transform.position.x)
        //{
        //    Debug.Log("���ʿ��� �浹");
        //}
        if (collisionPoint.x > transform.position.x && collision.gameObject.CompareTag("Judgement"))
        {
            Debug.Log("�����ʿ��� �浹");
            boss.Turnback();

        }
    }




    //������Ʈ�� ȭ���� ��������� Ȯ������ �޼���
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