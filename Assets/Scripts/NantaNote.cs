using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NantaNote : Note
{
    public GameObject End;
    public GameObject Monster_Rig;

    public Animator Effect;
    public Animator Text_Effect;

    public Animator MonsterAnimator;


    public GameObject HitUI;
    public TMP_Text HitText;

    

    //최소 히트 수 
    public int MaxhitCount;
    int hitCount;

    public float DecreaseAmount = 0.5f;


    bool hitStart = false;
    public bool Slay = false;
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
                if(!Slay)
                {
                    StartCoroutine(SizeUPHitText());
                }
                
                Slay = true;
            }
            
        }
    }


    

    public void NantaStart()
    {
        Debug.Log("디버그 확인 용");
        Camera.main.GetComponent<ResizingCamera>().Camera_Zoom();
        StartCoroutine(DecreaseOverTime());
    }

   public void IncreaseHitText()
    {
        if (!Slay)
        {
            hitCount++;
            if (HitUI.activeSelf == false)
            {
                HitUI.SetActive(true);
                HitText.text = hitCount.ToString();
            }
            else
            {
                HitText.text = hitCount.ToString();
            }
            //히트 텍스트 증가
        }

    }


    private IEnumerator DecreaseOverTime()
    {
        while (TimeCheck > 0)
        {
            yield return new WaitForSeconds(1f); // 1초 주기
            TimeCheck += 1f; // 시간 증가
            Debug.Log($"시간 증가: {TimeCheck}");
        }
        Debug.Log("노트 처리 실패");
    }

    public void HitNantaNote()
    {
        TimeCheck -= DecreaseAmount;
        TimeCheck = Mathf.Max(0, TimeCheck);
        Debug.Log($"현재 시간: {TimeCheck}");
    }

    private IEnumerator SizeUPHitText()
    {
        Camera.main.GetComponent<ResizingCamera>().Camera_ZoomOut();

        Text_Effect.Play("HitTextSizeUP");
        Monster_Rig.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }








}
