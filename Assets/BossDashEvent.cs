using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashEvent : BossEventNote
{

    BossMonster boss;
    public GameObject boss_Real;
    SpriteRenderer spriteRenderer;
    public Sprite bossSprite;

    bool IsTrigger = false;

    public bool Hit = false;




    //protected override void Awake()
    //{
    //    //base.Awake();
    //}
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();


        if(!Hit)
        {
            if (boss != null && CheckGetOutofCamera())
            {
                //���� ���� ����
                boss.VisualizeBoss();
                boss.Appear();

                IsTrigger = true;
            }
        }
        else
        {
            if(boss!= null)
            {
               
                boss_Real.transform.parent = null;

                boss.Turnback();
                boss.VisualizeBoss();

                spriteRenderer.color = Color.clear;
                gameObject.SetActive(false);
            }
            
           
           
            //gameObject.SetActive(false);
        }
       

        //transform.position = new Vector2(transform.position.x - 5 * Time.deltaTime, transform.position.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Boss")
        {

            if (!IsTrigger)
            {
                //���� ������Ʈ�� ������ �϶�� ��ȣ�� ����
                boss = collision.GetComponent<BossMonster>();
                boss.BossDash(this.transform);
                boss_Real = boss.gameObject;

                boss_Real.transform.parent = transform;
                //boss_Real.transform.position = Vector3.zero;
                bossSprite = boss.GetComponent<SpriteRenderer>().sprite;

                spriteRenderer.sprite = bossSprite;
                IsTrigger = true;
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

        if(collision.tag == "Judgement")
        {





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