using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetJudgeLineController : MonoBehaviour
{
    //������ ���� ������ ���콺�� ���� ��ġ�� �̵���ų �� �־��.

    //�� �ִ� �ּ� ��ġ ���� ������ 
    //�� �κ��� mathf.clamp ����ϸ� �� �� 

    bool Click = false;



    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Click = true;
        }

        if(Click == true)
        {
            Vector2 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 Pos_Changed = new Vector2(Mathf.Clamp(Pos.x, -50, 50), 0);

            transform.position = Pos_Changed;
        }



        if(Input.GetMouseButtonUp(0)) 
        {
            Click = false;
        }



    }





}
