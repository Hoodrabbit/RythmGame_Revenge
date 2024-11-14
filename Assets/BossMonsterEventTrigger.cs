using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterEventTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BossIn")
        {
            //asdfasf
        }

        //이벤트가 트리거에 닿았을 시 보스 이벤트 노트에 따라 보스의 상태가 결정됨 
        //+ 이벤트 종료 버튼에도 영향을 받음

        //보스의 공격 



    }
}
