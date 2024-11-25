using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackNote : Note
{
    public Vector2 Startpos;
    public Vector2 Endpos;
    public float StartRotation;
    public float DelayTime = 0.6f;

    public GameObject Effect;


    bool Coroutine_Check = false;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        

        if(GameManager.Instance.state == GameState.Play_Mode)
        {
            transform.position = new Vector3(transform.position.x, 1);
            spriteRenderer.color = Color.clear;
            Effect.SetActive(false);


        }

        
        



    }

    protected override void FixedUpdate()
    {
        if(!Coroutine_Check)
        {
            base.FixedUpdate();
        }
        
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.CompareTag("BossEventConverter"))
        {
            //보스에 닿았다는 걸로 가정하고 현재 오브젝트에 따라 회전 애니메이션을 실행시켜줌
            Startpos = transform.position;
            Coroutine_Check = true;
            StartCoroutine(SwitchingRotation((float)SongTime));
        }
    }

    IEnumerator SwitchingRotation(float songTime)
    {
        float nowTime = 0;

        float actualMoveTime = (float)SongTime - GameManager.Instance.MainAudio.time;

        float waitTime = actualMoveTime * DelayTime;

        float actionTime = actualMoveTime * (1 - DelayTime);


        //노트도 대기 시간 가지도록 만들고 남은시간 빠르게 이동하는 걸로 가면 괜찮지 않을까 싶음
        //처음에는 투명하게도 만들어주고

        while(waitTime >nowTime)
        {
            nowTime += Time.deltaTime;
            yield return null;
        }


        nowTime = 0;

        //보스 공격명령 이벤트 온
        BossAnimationController.Instance.ShootNote();
        spriteRenderer.color = Color.white;
        Effect.SetActive(true);
        while (actionTime >= nowTime )
        {
            //이 시간의 절반동안 특정 높이로 이동시키면서 현재 각도에서 0도로 맞춰줌
            float t = nowTime / actionTime;
            transform.position = Vector2.Lerp(Startpos, Endpos, t);
            float currentRotation = Mathf.LerpAngle(StartRotation, 0, t);
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
            nowTime += Time.deltaTime;
            yield return null;
        }
        Coroutine_Check = false;



        //현재 노래의 시간과 노트 고유의 시간의 차 만큼 이동함

    }





}
