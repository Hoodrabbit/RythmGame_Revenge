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
            //Ű �Է� ����

            //����â ���鶧 �����ؼ� Ű�� ������ �� �ð��� üũ�ؼ�
            //�ش� �ð��� ���� �ð��� �Ѿ�ٸ� �̽�ó��
            //���� �̳� Ű�� �ٽ� ������ ��Ʈ ó��

            //bool ���� �־ ��Ʈ�ϸ� true 
            //true�϶� Ű�� ��� �����ִ°� ���� �� ������ �ٽ� ä����� �ʱ�ȭ �����༭ ������ ��� ���� �� �ֵ��� ���ָ� ���� �� ����


            //�����ϴ� �ð� �ʿ� 
            //��Ʈ�� ���ߴ� ��ġ�� �ƴ� Ű�� ���� �� �մ� ��ġ���� �ٷ� Ŭ���� �����ϵ��� ����� ��� ��

        

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
                    Debug.Log("Ű�� ���ð� ���� ������ ����");
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
            Debug.Log("����");
        }
        else
        {
            Debug.Log("����");
        }
    }


}
