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
    //일단 노트화 시켜서 다른 노트처럼 똑같이 움직이되 보스가 나오는 
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
        //보스만의 특별한 기능



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

        Debug.Log("출현");

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

        Debug.Log("퇴장");
        GameManager.Instance.BossAppear = false;
    }

    public void BossDash(Transform parent)
    {
        //invisble Mode
        //지금은 임의로 색깔을 입힌 상태라 이렇게 코드를 짜야 하지만 나중에는 0,0,0 이렇게 적용할 듯
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
        
        //시간을 어떻게 할지가 필요함
        while (transform.position.x >= 0)
        {
            transform.position = parent.position;
            yield return null;
        }
        
        if(transform.position.x <= 0)
        {
            Hit = true;
            HitAction?.Invoke();
            Debug.Log("눌렀는지 체크할꺼임");
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
            Debug.Log("왜안될까");

            TTime += Time.deltaTime;
            
            //아직 이상함 방식 좀 더 세밀하게 잡아서 처리해야 할 듯
            //해당 코루틴이 실행되는 조건 이런 거 확인해봐야함
            transform.position = Vector3.Lerp(pos, endpos, TTime / MaxTime);
            yield return null;
        }
        transform.position = endpos;

       
    }

    IEnumerator TurnBack_Fail()
    {
        //보스를 히트시키지 못했을 경우 
        //다른 방식으로 돌아와야 함

        //현재 생각중인 방식은 판정 라인을 지나갔다가 안보이는 영역에서 위치를 이동시키고 다시 오른쪽에서 나타나도록 하는 방식


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

     보스 제작 해야 함 

 보스 연출 기본적으로 언제 나오는지 체크를 해줘야 하고
 생성된 보스의 경우 

 expand Line 사용했을 때 처럼 해당 라인이 판정 라인을 거치면 보스가 나타나는 식으로 만들어 주는 게 좋을 것 같음 

 보스가 이미 존재하고 있는 경우 노트 위치들을 조금 수정할 필요가 있을 것 같음 
 아니면 보스가 존재할때만 생성할 수 있는 특수 노트 그런 기능도 있으면 좋을 것 같음 



 이제 마커 표시를 만들어서 보스를 활성화 혹은 비활성화가 가능한 상태로 만들어서 

 에디터 상에서 해당 보스를 일단 특정 영역상에서 나온다는 것은 무조건 표시하되 

 해당 보스를 투명하게 만들어준다거나 아니면 잠시 비활성화 해서 존재한다는 것만 알려주는 식도 괜찮을 것 같음
 어쨌든 보스는 언제부터 언제까지 그런 영역이 존재하기 때문에 그 영역 내에서만 작동가능한 그런 노트들을 생성해주는 방식을 차용하는 것도
 괜찮을 것 같음



 선택창 개편 필요





 보스는 얼마나 존재하는지 
 시간을 롱노트 마냥 존재하도록 해야할 것 같고 

 보스가 존재하는 동안의 
 중형 혹은 소형 노트는 보스의 위치에서 생성되는 것처럼 보여야함 




     */
}
