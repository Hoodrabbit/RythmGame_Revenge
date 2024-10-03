using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public Sprite BossImg;

    public bool Trigger = false;
    CircleCollider2D bossCollider;

    bool Hit = true;
    float MaxTime = 0.3f;
    //�ϴ� ��Ʈȭ ���Ѽ� �ٸ� ��Ʈó�� �Ȱ��� �����̵� ������ ������ 
    float TTime = 0;
    Vector2 startpos;
    Vector2 endpos;

    Coroutine DashCoroutine;
    Coroutine TurnBack_SuccessCoroutine;
    Coroutine TurnBack_FailCoroutine;

    Action HitAction;



    private void Start()
    {
        bossCollider = GetComponent<CircleCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = BossImg;
        startpos = transform.position;
        endpos = new Vector3(25, 1);

        HitAction += HitCheck;

    }


    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //SwitchToCoroutineB(); //�ش� �ڵ带 ���� ������ ���� ���� ��ġ�� ���ƿ�

        }

        


    }

    public void Appear()
    {
        bossCollider.isTrigger = true;
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
       DashCoroutine = StartCoroutine(Dash(parent));
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


    IEnumerator TurnBack_Success()
    {
        Vector2 pos = transform.position;

        while (TTime <= MaxTime)
        {
            TTime += Time.deltaTime;
            
            //���� �̻��� ��� �� �� �����ϰ� ��Ƽ� ó���ؾ� �� ��
            //�ش� �ڷ�ƾ�� ����Ǵ� ���� �̷� �� Ȯ���غ�����
            transform.position = Vector3.Lerp(pos, endpos, TTime / MaxTime);
            yield return null;
        }

       
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
