using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public int hp = 300;
    int TakeHP; // 감소할 HP 값

    public Animator Playeranimator;

    public void Start()
    {
        Playeranimator = GetComponent<Animator>();
    
        
    
    }





    public void TakeHPMethod(int hp_Get)
    {
        TakeHP = hp_Get;
        hp -= TakeHP;
        //플레이어 데미지 받는 애니메이션 실행

    }

    public void NoteHitAnimation_UP()
    {
        //윗 노트 히트 애니메이션 실행
    }

    public void NoteHitAnimation_DOWN() 
    { 
        //아래 노트 히트 애니메이션 실행
    }

    public void LongNoteHitAnimation_Hit()
    {
        //롱 노트 히트 애니메이션 실행
    }

    public void NoteMissAnimation()
    {

        //노트를 못쳤을 때 실행될 애니메이션
        //미스가 됬을 때 캐릭터 
    
    }

    public void GameOver()
    {
        //게임오버 창 출력 혹은 씬 변경??
    }




    // 
    //키를 눌렀을 때 애니메이션 실행되는 메서드







}
