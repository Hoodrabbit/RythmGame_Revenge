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
        // 현재 시간
        float nowTime = 0;

        // SongTime과 현재 오디오 시간에 따른 실제 이동 시간 계산
        float actualMoveTime = songTime - GameManager.Instance.MainAudio.time;

        // 대기 시간 계산 (노트 발사 전 대기)
        float waitTime = actualMoveTime * DelayTime / 2;

        // 실제 동작 시간 (대기 시간 이후 이동 및 회전)
        float actionTime = actualMoveTime * (1 - DelayTime);

        // 보스 공격명령: 노트를 발사하기 전에 발생
        // 먼저 공격을 시작하도록 설정
        BossAnimationController.Instance.ShootNote();

        // 보스를 활성화하고 투명도 초기화 (대기 시간 동안 Y만 이동하기 전에 활성화)
        spriteRenderer.color = Color.white;
        Effect.SetActive(true);

        // 대기 시간 동안 처리: 이 시간 동안은 Y값만 이동하고 보스가 바라보도록 회전
        while (waitTime > nowTime)
        {
            if (GameManager.Instance.MainAudio.isPlaying)
            {
                nowTime += Time.deltaTime;
            }

            // Y값만 이동 (X값은 고정)
            float t = nowTime / waitTime; // t는 0에서 1로 변함
            float currentY = Mathf.Lerp(Startpos.y, Endpos.y, t);

            // 보스의 위치 업데이트 (X는 고정, Y값만 변동)
            transform.position = new Vector2(Startpos.x, currentY);

            // 보스가 이동하는 방향을 바라보게 회전 (현재 Y값으로 이동 중이므로 X값이 고정)
            Vector2 direction = new Vector2(Endpos.x - Startpos.x, currentY - Startpos.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // 각도 계산
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            yield return null;
        }

        // 대기 시간 후 실행할 코드
        nowTime = 0;

        // 남은 시간 동안 이동 및 회전 (X, Y 모두 이동)
        while (actionTime > nowTime)
        {
            if (GameManager.Instance.MainAudio.isPlaying)
            {
                // 남은 시간 동안 X와 Y를 동시에 이동
                float t = nowTime / actionTime; // t는 0에서 1로 변함

                // Y값은 대기 시간 동안 이미 이동했으므로, X값만 보간하여 이동
                float currentX = Mathf.Lerp(Startpos.x, Endpos.x, t);
                //float currentY = Mathf.Lerp(Startpos.y, Endpos.y, t); // Y값은 이미 대기 시간 동안 설정된 값

                // 보스의 위치와 회전
                transform.position = new Vector2(currentX, Endpos.y);

                // 회전 처리: 시작 각도에서 0으로 회전
                float currentRotation = Mathf.LerpAngle(StartRotation, 0, t);
                transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

                // 시간 업데이트
                nowTime += Time.deltaTime;
            }
            yield return null;
        }

        // 완료 후 코루틴 종료 처리
        Coroutine_Check = false;
    }






}
