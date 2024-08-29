using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class OffsetJudgeLineController : MonoBehaviour
{
    //������ ���� ������ ���콺�� ���� ��ġ�� �̵���ų �� �־��.

    //�� �ִ� �ּ� ��ġ ���� ������ 
    //�� �κ��� mathf.clamp ����ϸ� �� �� 

    public bool Click = false;



    public void Update()
    {
        if (Click == true)
        {
            Vector2 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(Pos);
            
            if(Mathf.Abs(Pos.x) >5)
            {
                if(Pos.x < 0)
                {
                    Pos.x = -5;
                }
                else
                {
                    Pos.x = 5;
                }
            }

            if (Pos.x <= 5 && Pos.x >= -5)
            {
                //Vector2 Pos_Changed = new Vector2(Pos.x, 0);

                transform.position = new Vector2(Pos.x, 0);
            }

        }



        if (Input.GetMouseButtonUp(0))
        {
            Click = false;
        }



    }

    private void OnMouseDown()
    {
      //  if (Input.GetMouseButtonDown(0))
    //    {
            Click = true;
            //�ش� ����� ������ ������Ʈ�� Ŭ������ �� �۵��ǵ��� �ؾ� ��
      //  }

    
    }


    public int GetOffsetValue()
    {
        return (int)(transform.position.x * 100);
    }




}
