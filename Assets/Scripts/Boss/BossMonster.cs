using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossMonster : Note
{
    [Header("보스 돌진 상태 전 전조 애니메이션 시간")]
    public float DelayTime = 0;
    [Space(30f)]


    public bool Trigger = false;
    CircleCollider2D bossCollider;

    public bool Hit = true;

    bool DashHit = false;


    float MaxTime = 0.1f;
    float TTime = 0;
    public Vector2 startpos;
    public Vector2 endpos;

    BossStateQueue BossState;


    public Action HitAction;

    public BossAnimationController bossAnimation;


    protected override void Awake()
    {
        Debug.Log(transform.position);
        bossAnimation = GetComponent<BossAnimationController>();
        BossState = FindObjectOfType<BossStateQueue>();
    }

    protected override void Start()
    {
        GameManager.Instance.BossAppear = false;

        DataManager.Instance.eventManager.RefreshNoteEvent += EventChangeMethod;


        bossCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startpos = transform.position;
        HitAction += HitCheck;

    }



    protected override void FixedUpdate()
    {
        //보스만의 특별한 기능
    }

    public void Appear()
    {
        bossAnimation.BossAnimator.SetBool("GetOut", false);
        StartCoroutine(AppearBoss());

    }

    public void Disappear()
    {
        bossAnimation.BossAnimator.SetBool("GetOut", true);
        bossCollider.isTrigger = false;
        StartCoroutine (DisappearBoss());
    }

    IEnumerator AppearBoss()
    {
       
        TTime = 0;
        
        bossCollider.isTrigger = true;
        while (TTime <= MaxTime)
        {
            TTime += Time.deltaTime;
            float t = TTime / MaxTime;

            transform.position =Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }
        transform.position = endpos;
        GameManager.Instance.BossAppear = true;
        if(BossState.IsBossQueueExist())
        {
            BossState.StartDash();
        }
        Debug.Log("출현");

        

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

    public void BossDash(Transform DashEvent, float songTime)
    {
        StartCoroutine(Dash(DashEvent, songTime));
    }

    public void VisualizeBoss()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
    }





    //해당 코루틴이 작동되는 중에 노트를 누르면 코루틴을 종료하고 히트 처리 발생시키기
    //보스
    IEnumerator Dash(Transform DashEvent, float songTime)
    {
        Hit = false;


        TTime = 0;
        SongTime = songTime;

        //단순히 속도로 하는게 아니라 현재 재생시간 체크하고 그 시간에 맞게 이동 위치 조절해주도록 다시 만들어줘야 함
        Vector2 startpos = transform.position;

        float actualMoveTime = (float)SongTime - GameManager.Instance.MainAudio.time;

        float waitTime = actualMoveTime * DelayTime;

        float actionTime = actualMoveTime * (1- DelayTime);
        
        float elapsedTime = 0;




        if (actualMoveTime >1f)
        {
            bossAnimation.PlayDashAniStart();

            Debug.Log("1111대쉬" + DashEvent.position + " . " + SongTime + " , " + GameManager.Instance.MainAudio.time);
            if (actualMoveTime > 0)
            {
                Debug.Log(1);

                while (elapsedTime < waitTime)
                {
                    float t = elapsedTime / waitTime;

                    if (t >= 0.4f)
                    {
                        //spriteRenderer.color = Color.red;
                    }
                    elapsedTime += Time.deltaTime;
                    yield return null;

                }
                bossAnimation.PlayDashAniEnd();
                elapsedTime = 0;
                while (elapsedTime < actionTime)
                {
                    float t = elapsedTime / actionTime;
                    transform.position = Vector2.Lerp(startpos, new Vector3(0, startpos.y), t);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

            }
            else
            {
                Debug.Log(2);
                while (elapsedTime <= actualMoveTime)
                {
                    float t = elapsedTime / actualMoveTime;
                    transform.position = Vector2.Lerp(startpos, new Vector3(0, startpos.y), t);

                    elapsedTime += Time.deltaTime;
                    yield return null;

                }
            }


        }
        else
        {
           bossAnimation.PlayDashAniEnd();

            Debug.Log("2222대쉬" + DashEvent.position + " . " + SongTime + " , " + GameManager.Instance.MainAudio.time);
            if (actualMoveTime > 0)
            {
                Debug.Log(1);

                while (elapsedTime < waitTime)
                {
                    float t = elapsedTime / waitTime;

                    if (t >= 0.7f)
                    {
                        //spriteRenderer.color = Color.red;
                    }
                    elapsedTime += Time.deltaTime;
                    yield return null;

                }
                elapsedTime = 0;
                while (elapsedTime < actionTime)
                {
                    float t = elapsedTime / actionTime;
                    transform.position = Vector2.Lerp(startpos, new Vector3(0, startpos.y), t);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

            }
            else
            {
                Debug.Log(2);
                while (elapsedTime <= actualMoveTime)
                {
                    float t = elapsedTime / actualMoveTime;
                    transform.position = Vector2.Lerp(startpos, new Vector3(0, startpos.y), t);

                    elapsedTime += Time.deltaTime;
                    yield return null;

                }
            }
        }



       


        yield return new WaitForSeconds(0.2f);
        Debug.Log("히트못함");
        PlayerController.Instance.TakeHPMethod(HP);
        SharedNoteList.Instance.DeleteBossNote();
        HitLate();



    }

    public void Turnback()
    {
        
        StartCoroutine(TurnBack_Success());
    }


    IEnumerator TurnBack_Success()
    {
        Vector2 pos = transform.position;
        TTime = 0;
        bossAnimation.BossAnimator.SetBool("Dash", false);
        bossAnimation.BossAnimator.SetTrigger("Damaged");
        while (TTime <= MaxTime)
        {

            TTime += Time.deltaTime;
            
            transform.position = Vector3.Lerp(pos, endpos, TTime / MaxTime);
            yield return null;
        }
        transform.position = endpos;

        bossAnimation.TurnOffDash();

       
    }

    IEnumerator TurnBack_Fail()
    {
        Vector2 pos = transform.position;
        TTime = 0;
        bossAnimation.BossAnimator.SetBool("Dash", false);
        while (TTime <= MaxTime)
        {

            TTime += Time.deltaTime;

            transform.position = Vector3.Lerp(pos, endpos, TTime / MaxTime);
            yield return null;
        }
        transform.position = endpos;

        bossAnimation.TurnOffDash();

    }

    void HitCheck()
    {
        Hit = true;
        StartCoroutine(TurnBack_Success());
        BossState.StartDash();
        
    }

    void HitLate()
    {

        Hit = false;
        StartCoroutine(TurnBack_Fail());
        BossState.StartDash();


    }
}
