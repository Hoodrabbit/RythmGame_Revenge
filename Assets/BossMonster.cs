using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{

    public Sprite BossImg;

    bool check = false;
    float MaxTime = 0.3f;
    //�ϴ� ��Ʈȭ ���Ѽ� �ٸ� ��Ʈó�� �Ȱ��� �����̵� ������ ������ 
    float TTIme = 0;
    Vector2 startpos;
    private void Start()
    {
        startpos = transform.position;
    }


    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {

            check = true;
            



        }

        if(check)
        {
           

            TTIme += Time.deltaTime;
            float t = TTIme / MaxTime;

            transform.position = Vector3.Lerp(startpos, new Vector3(5, 0),t);
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
