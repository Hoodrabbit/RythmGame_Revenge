using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public int hp = 300;
    int TakeHP; // ������ HP ��

    public Animator Playeranimator;

    public void Start()
    {
        Playeranimator = GetComponent<Animator>();
    
        
    
    }





    public void TakeHPMethod(int hp_Get)
    {
        TakeHP = hp_Get;
        hp -= TakeHP;
        //�÷��̾� ������ �޴� �ִϸ��̼� ����

    }

    public void NoteHitAnimation_UP()
    {
        //�� ��Ʈ ��Ʈ �ִϸ��̼� ����
    }

    public void NoteHitAnimation_DOWN() 
    { 
        //�Ʒ� ��Ʈ ��Ʈ �ִϸ��̼� ����
    }

    public void LongNoteHitAnimation_Hit()
    {
        //�� ��Ʈ ��Ʈ �ִϸ��̼� ����
    }

    public void NoteMissAnimation()
    {

        //��Ʈ�� ������ �� ����� �ִϸ��̼�
        //�̽��� ���� �� ĳ���� 
    
    }

    public void GameOver()
    {
        //���ӿ��� â ��� Ȥ�� �� ����??
    }




    // 
    //Ű�� ������ �� �ִϸ��̼� ����Ǵ� �޼���







}
