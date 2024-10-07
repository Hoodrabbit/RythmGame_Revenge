using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyjudgement : MonoBehaviour
{

    private int clickCount = 0;
    private bool isCollided = false;
    private float keyPressInterval = 0.5f;
    private float totalTimeLimit = 10f;


    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Boss"))
        {

            BossDashEvent bossDashScript = collision.GetComponent<BossDashEvent>();
            if (bossDashScript != null /*!bossDashScript.StopBoss*/)
            {
                if (Input.anyKeyDown)
                {
                    StartCoroutine(CheckKeyPresses());
                }

                //bossDashScript.StopBoss = true;
                isCollided = true;
            }
                
                
        }
            //키 입력 받음

            //선택창 만들때 참고해서 키를 누르고 뗀 시간을 체크해서
            //해당 시간이 일정 시간을 넘어섰다면 미스처리
            //일정 이내 키를 다시 누르면 히트 처리

            //bool 값을 넣어서 히트하면 true 
            //true일때 키를 계속 눌러주는게 이제 뭐 게이지 다시 채우듯이 초기화 시켜줘서 보스를 계속 때릴 수 있도록 해주면 좋을 것 같음


            //존재하는 시간 필요 
            //노트가 멈추는 위치가 아닌 키를 누를 수 잇는 위치부터 바로 클릭이 가능하도록 만들어 줘야 함

        

    }

    private IEnumerator CheckKeyPresses()
    {
        float totalElapsedTime = 0f;
        float lastKeyPressTime = 0f;
        clickCount = 0;

        while (totalElapsedTime < totalTimeLimit && clickCount < 10)
        {
            if (Input.anyKeyDown)
            {
                if (Time.time - lastKeyPressTime > keyPressInterval)
                {
                    Debug.Log("키를 제시간 내에 누르지 못함");
                    yield break;
                }

                clickCount++;
                lastKeyPressTime = Time.time;
            }

            totalElapsedTime += Time.deltaTime;
            yield return null;
        }

        if (clickCount < 10)
        {
            Debug.Log("실패");
        }
        else
        {
            Debug.Log("성공");
        }
    }


}
