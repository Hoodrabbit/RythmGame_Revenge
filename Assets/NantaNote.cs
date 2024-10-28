using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NantaNote : Note
{
    public GameObject End;

    public Animator MonsterAnimator;
    
    //최소 히트 수 
    public int MaxhitCount;

    bool hitStart = false;


    //몇초 이내로 눌러야 하는지에 대한 시간
    float hitinSeconds = 0.3f;

    //시간 측정을 위한 변수
    float TimeCheck = 0;

    private void Update()
    {
        if(hitStart)
        {
            TimeCheck += Time.deltaTime;

            //메인 노트 정지시키기
            //끝 노트 움직이도록 함
            //플레이 상에서는 안보임





        }
        if (hitinSeconds < TimeCheck)
        {
            Debug.Log("노트 처리 실패");
            hitStart = false;
        }


    }

    public void HitNantaNote()
    {
       TimeCheck -= Time.deltaTime;
    }
    









}
