using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Note
{
    public Sprite BossImg;

    public bool Trigger = false;
    CircleCollider2D bossCollider;
    SpriteRenderer spriteRenderer;

    bool Hit = true;
    float MaxTime = 0.3f;
    //�ϴ� ��Ʈȭ ���Ѽ� �ٸ� ��Ʈó�� �Ȱ��� �����̵� ������ ������ 
    float TTime = 0;
    Vector2 startpos;
    public Vector2 endpos;

    Coroutine DashCoroutine;
    Coroutine TurnBack_SuccessCoroutine;
    Coroutine TurnBack_FailCoroutine;

    Action HitAction;

    Animator Boss_animator;

    protected override void Awake()
    {
        base.Awake();
        Boss_animator= GetComponent<Animator>();
    }


    protected override void Start()
    {
        DataManager.Instance.eventManager.RefreshNoteEvent += EventChangeMethod;


        bossCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = BossImg;
        startpos = transform.position;
        //endpos = new Vector3(25, 1);

        HitAction += HitCheck;

    }



    protected override void FixedUpdate()
    {
        //�������� Ư���� ���



    }

    public void Appear()
    {
        
        StartCoroutine(AppearBoss());

    }

    public void Disappear()
    {
        bossCollider.isTrigger = false;
        StartCoroutine (DisappearBoss());
    }

    IEnumerator AppearBoss()
    {
       
        TTime = 0;
        
        while(TTime <= MaxTime)
        {
            TTime += Time.deltaTime;
            float t = TTime / MaxTime;

            transform.position =Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }

        Debug.Log("����");

        GameManager.Instance.BossAppear = true;
        bossCollider.isTrigger = true;

    }

    IEnumerator DisappearBoss()
    {
      
        TTime = 0;

        while(TTime <= MaxTime)
        {
            TTime += Time.deltaTime;
            float t = TTime / MaxTime;

            transform.position = Vector3.Lerp(endpos, startpos, t);
            yield return null;
        }

        Debug.Log("����");
        GameManager.Instance.BossAppear = false;
    }

    public void BossDash(Transform parent)
    {
        //invisble Mode
        //������ ���Ƿ� ������ ���� ���¶� �̷��� �ڵ带 ¥�� ������ ���߿��� 0,0,0 �̷��� ������ ��
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
       //DashCoroutine = StartCoroutine(Dash(parent));
    }

    public void VisualizeBoss()
    {

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
    }


    IEnumerator Dash(Transform parent)
    {

        TTime = 0;

        Vector2 pos = transform.position;
        float xpos = pos.x;
        
        //�ð��� ��� ������ �ʿ���
        while (transform.position.x >= 0)
        {
            transform.position = parent.position;
            yield return null;
        }
        
        if(transform.position.x <= 0)
        {
            Hit = true;
            HitAction?.Invoke();
            Debug.Log("�������� üũ�Ҳ���");
        }
           


        //yield return null;

    }

    public void Turnback()
    {
        StartCoroutine(TurnBack_Success());
    }


    IEnumerator TurnBack_Success()
    {
        Vector2 pos = transform.position;
        TTime = 0;


        while (TTime <= MaxTime)
        {
            Debug.Log("�־ȵɱ�");

            TTime += Time.deltaTime;
            
            //���� �̻��� ��� �� �� �����ϰ� ��Ƽ� ó���ؾ� �� ��
            //�ش� �ڷ�ƾ�� ����Ǵ� ���� �̷� �� Ȯ���غ�����
            transform.position = Vector3.Lerp(pos, endpos, TTime / MaxTime);
            yield return null;
        }
        transform.position = endpos;

       
    }

    IEnumerator TurnBack_Fail()
    {
        //������ ��Ʈ��Ű�� ������ ��� 
        //�ٸ� ������� ���ƿ;� ��

        //���� �������� ����� ���� ������ �������ٰ� �Ⱥ��̴� �������� ��ġ�� �̵���Ű�� �ٽ� �����ʿ��� ��Ÿ������ �ϴ� ���


        yield return null;

    }

    void HitCheck()
    {
        if (Hit)
        {
            StartCoroutine(TurnBack_Success());
        }
        else
        {

        }
    }

    public Animator GetAnimator()
    {
        return Boss_animator;
    }






    /*

     ���� ���� �ؾ� �� 

 ���� ���� �⺻������ ���� �������� üũ�� ����� �ϰ�
 ������ ������ ��� 

 expand Line ������� �� ó�� �ش� ������ ���� ������ ��ġ�� ������ ��Ÿ���� ������ ����� �ִ� �� ���� �� ���� 

 ������ �̹� �����ϰ� �ִ� ��� ��Ʈ ��ġ���� ���� ������ �ʿ䰡 ���� �� ���� 
 �ƴϸ� ������ �����Ҷ��� ������ �� �ִ� Ư�� ��Ʈ �׷� ��ɵ� ������ ���� �� ���� 



 ���� ��Ŀ ǥ�ø� ���� ������ Ȱ��ȭ Ȥ�� ��Ȱ��ȭ�� ������ ���·� ���� 

 ������ �󿡼� �ش� ������ �ϴ� Ư�� �����󿡼� ���´ٴ� ���� ������ ǥ���ϵ� 

 �ش� ������ �����ϰ� ������شٰų� �ƴϸ� ��� ��Ȱ��ȭ �ؼ� �����Ѵٴ� �͸� �˷��ִ� �ĵ� ������ �� ����
 ��·�� ������ �������� �������� �׷� ������ �����ϱ� ������ �� ���� �������� �۵������� �׷� ��Ʈ���� �������ִ� ����� �����ϴ� �͵�
 ������ �� ����



 ����â ���� �ʿ�





 ������ �󸶳� �����ϴ��� 
 �ð��� �ճ�Ʈ ���� �����ϵ��� �ؾ��� �� ���� 

 ������ �����ϴ� ������ 
 ���� Ȥ�� ���� ��Ʈ�� ������ ��ġ���� �����Ǵ� ��ó�� �������� 




     */
}
