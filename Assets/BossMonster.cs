using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{

    public Sprite BossImg;

    bool check = false;
    float MaxTime = 0.3f;
    //일단 노트화 시켜서 다른 노트처럼 똑같이 움직이되 보스가 나오는 
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

     보스 제작 해야 함 

 보스 연출 기본적으로 언제 나오는지 체크를 해줘야 하고
 생성된 보스의 경우 

 expand Line 사용했을 때 처럼 해당 라인이 판정 라인을 거치면 보스가 나타나는 식으로 만들어 주는 게 좋을 것 같음 

 보스가 이미 존재하고 있는 경우 노트 위치들을 조금 수정할 필요가 있을 것 같음 
 아니면 보스가 존재할때만 생성할 수 있는 특수 노트 그런 기능도 있으면 좋을 것 같음 



 이제 마커 표시를 만들어서 보스를 활성화 혹은 비활성화가 가능한 상태로 만들어서 

 에디터 상에서 해당 보스를 일단 특정 영역상에서 나온다는 것은 무조건 표시하되 

 해당 보스를 투명하게 만들어준다거나 아니면 잠시 비활성화 해서 존재한다는 것만 알려주는 식도 괜찮을 것 같음
 어쨌든 보스는 언제부터 언제까지 그런 영역이 존재하기 때문에 그 영역 내에서만 작동가능한 그런 노트들을 생성해주는 방식을 차용하는 것도
 괜찮을 것 같음



 선택창 개편 필요





 보스는 얼마나 존재하는지 
 시간을 롱노트 마냥 존재하도록 해야할 것 같고 

 보스가 존재하는 동안의 
 중형 혹은 소형 노트는 보스의 위치에서 생성되는 것처럼 보여야함 




     */
}
