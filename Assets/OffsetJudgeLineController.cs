using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class OffsetJudgeLineController : MonoBehaviour
{
    //오프셋 판정 라인을 마우스를 통해 위치를 이동시킬 수 있어요.

    //단 최대 최소 위치 값이 존재함 
    //그 부분은 mathf.clamp 사용하면 될 듯 

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
            //해당 기능을 오로지 오브젝트를 클릭했을 때 작동되도록 해야 함
      //  }

    
    }


    public int GetOffsetValue()
    {
        return (int)(transform.position.x * 100);
    }




}
