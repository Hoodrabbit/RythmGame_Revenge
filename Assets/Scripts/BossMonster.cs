using System;
using System.Collections;
using Unity.VisualScripting;
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

    public Action HitAction;

    Animator Boss_animator;

    protected override void Awake()
    {
        //base.Awake();
        Boss_animator= GetComponent<Animator>();
    }

    //�̺�Ʈ ��ȣ�� �޵� ��ȣ�� �����ٴϸ� ���� ��������
    //�ָ��� ��Ģ�� ���� 
    //���� ���� 
    //ū ���� 




    protected override void Start()
    {
        DataManager.Instance.eventManager.RefreshNoteEvent += EventChangeMethod;


        bossCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = BossImg;
        startpos = transform.position;
        //endpos = new Vector2(GameManager.Instance.GetBPS() * 30, transform.position.y);
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
        GameManager.Instance.BossAppear = true;
        bossCollider.isTrigger = true;
        while (TTime <= MaxTime)
        {
            TTime += Time.deltaTime;
            float t = TTime / MaxTime;

            transform.position =Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }

        Debug.Log("����");

        

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

    //�뷡 �ð��� ���������� �ؼ� ���� ������ ��� �ð��� ���޹��� �ð����� �� �����̵���
    public void BossDash(Transform DashEvent, float songTime)
    {
        //xpos - GameManager.Instance.speed * (float)(AudioSettings.dspTime - AudioTime)
        StartCoroutine(Dash(DashEvent, songTime));

    }

    public void VisualizeBoss()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
    }





    //�ش� �ڷ�ƾ�� �۵��Ǵ� �߿� ��Ʈ�� ������ �ڷ�ƾ�� �����ϰ� ��Ʈ ó�� �߻���Ű��
    //����
    IEnumerator Dash(Transform DashEvent, float songTime)
    {
        
        TTime = 0;
        SongTime = songTime;

        //�ܼ��� �ӵ��� �ϴ°� �ƴ϶� ���� ����ð� üũ�ϰ� �� �ð��� �°� �̵� ��ġ �������ֵ��� �ٽ� �������� ��
        Vector2 startpos = transform.position;

        float offsetTime = GameManager.Instance.GetBPS() * 1.5f;
        Debug.Log("offsetTime : " + offsetTime);

        float actualMoveTime = (float)SongTime - GameManager.Instance.MainAudio.time - offsetTime;
        Debug.Log("�ð� : " + actualMoveTime);

        float elapsedTime = 0;

        while (elapsedTime < offsetTime)
        {
            float t = elapsedTime / offsetTime;
            
            if(t >= 0.7f)
            {
                spriteRenderer.color = Color.red;
            }
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        //yield return new WaitForSeconds(offsetTime);
        spriteRenderer.color = Color.red;
       elapsedTime = 0;
        while (elapsedTime <= actualMoveTime)
        {
            //Debug.Log("�Ǵ��� Ȯ��");
            //transform.position = DashEvent.position;
            float t = elapsedTime / actualMoveTime;
            transform.position = Vector2.Lerp(startpos, new Vector3(0, startpos.y), t * GameManager.Instance.speed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        //float xpos = pos.x;

        //Debug.Log("�ð� �� : " + songTime + " , " +  GameManager.Instance.MainAudio.time);





        ////�ð��� ��� ������ �ʿ���
        //while (songTime > GameManager.Instance.MainAudio.time)
        //{
        //    //��� �̵����� ����� �ٸ��� �����ؾ� �� �� ����

        //    transform.position = new Vector3(pos.x - GameManager.Instance.speed * GameManager.Instance.GetBPS(), transform.position.y);



        //    yield return null;
        //}

        //Debug.Log("��� ����Ǵ��� Ȯ�ο�");


        //if(transform.position.x <= 0)
        //{
        //    Hit = true;
        //    HitAction?.Invoke();
        //    Debug.Log("�������� üũ�Ҳ���");
        //}
           


        ////yield return null;

    }

    public void Turnback()
    {
        StartCoroutine(TurnBack_Success());
    }


    IEnumerator TurnBack_Success()
    {
        Vector2 pos = transform.position;
        TTime = 0;
        spriteRenderer.color = Color.white;

        while (TTime <= MaxTime)
        {

            TTime += Time.deltaTime;
            
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
