using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetJudgeLineController : MonoBehaviour
{
    //오프셋 판정 라인을 마우스를 통해 위치를 이동시킬 수 있어요.

    //단 최대 최소 위치 값이 존재함 
    //그 부분은 mathf.clamp 사용하면 될 듯 

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
