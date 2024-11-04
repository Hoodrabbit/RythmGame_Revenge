using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NantaNote : Note
{
    public GameObject End;

    public Animator MonsterAnimator;

    public TMP_Text HitText;

    

    //최소 히트 수 
    public int MaxhitCount;

    bool hitStart = false;
    public bool IsConnect = false;


    //몇초 이내로 눌러야 하는지에 대한 시간
    float hitinSeconds = 0.5f;

    //시간 측정을 위한 변수
    float TimeCheck = 0;


    private void Update()
    {
        if(GameManager.Instance.state == GameState.Play_Mode)
        {
            End.GetComponent<SpriteRenderer>().enabled = false; 
        }

        if (IsConnect == false)
        {
            if (transform.position.x < End.transform.position.x)
            {
                IsConnect = true;
            }

        }
        else
        {
            if (transform.position.x >= End.transform.position.x)
            {
                gameObject.SetActive(false);
            }
            
        }

            if (hitStart)
        {
            //때릴 때 파티클도 나오면 좋을 것 같음
            TimeCheck += Time.deltaTime;
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
    

    public void NantaStart()
    {
        hitStart = true;
    }

    void IncreaseHitText()
    {
        //히트 텍스트 증가
    }



}
