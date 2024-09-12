using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossNote : Note 
{
    public int NoteType_Boss;
    public int TapCount;
    public bool Invisble = true;


}

// => ������ ���� ��� �״��
// <= ������ ���� ����� �ݴ� �����̾����� ���� �����̾����� ����

public class BossAppearNote : BossNote
{
    BossNote bossNote = new BossNote();



    bool Use = false;

    bool pass = false;


    //bool UnPlaying

    private float clickTime = 0f;  // ���� Ŭ�� �ð�
    private float clickDelay = 0.3f;  // ���� Ŭ�� ��� �ð� (0.3�� �̳��� ���� Ŭ���ؾ� ���� Ŭ������ �ν�)
    private int clickCount = 0;  // Ŭ�� Ƚ��




    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        bossNote.NoteType_Boss = 1;
        bossNote.TapCount = 0;
        bossNote.Invisble = true;

        if(GameManager.Instance.state == GameState.Play_Mode)
        {
            SpriteRenderer SR = GetComponent<SpriteRenderer>();
            SR.color = new Color(0, 0, 0, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }



        if (GameManager.Instance.MainAudio.isPlaying == true)
        {

            if (transform.position.x <= 0)
            {
                if (!Use)
                {
                    Use = true;
                    if (!GameManager.Instance.BossAppear)
                    {
                        BossAppear();
                    }
                    else
                    {
                        BossDisappear();
                    }
                }


                //boss Appear Method;
                //event on
            }

        }
        else
        {
            //������ �����ϴ� �ð� �ٽ� �������ֱ� 



            if (transform.position.x >= 0)
            {
                if (Use)
                {
                    Use = false;
                    if (!GameManager.Instance.BossAppear)
                    {
                        BossAppear();
                    }
                    else
                    {
                        BossDisappear();
                    }
                }
                else
                {

                }


                //boss Appear Method;
                //event on
            }

            if(transform.position.x < 0)
            {
                pass = true;

                if (!Use)
                {
                    Use = true;
                    if (!GameManager.Instance.BossAppear)
                    {
                        BossAppear();
                    }
                    else
                    {
                        BossDisappear();
                    }
                }
               
            }


        }

    }

    private void HandleClick()
    {
        // ���� �ð��� ���� Ŭ�� �ð��� ���̸� Ȯ��
        if (Time.time - clickTime < clickDelay)
        {
            clickCount++;

            // ���� Ŭ������ �ν�
            if (clickCount == 2)
            {
                clickCount = 0;  // Ŭ�� ī��Ʈ �ʱ�ȭ
                OnDoubleClick();  // ���� Ŭ�� �� ������ �޼��� ȣ��
               
            }
        }
        else
        {
            // Ŭ�� �ð� ������ �ʹ� ���, �ٽ� ù Ŭ������ ����
            clickCount = 1;
        }

        // Ŭ�� �ð� ���
        clickTime = Time.time;
    }

    private void OnDoubleClick()
    {
        //Debug.Log("���� Ŭ���� �����Ǿ����ϴ�: " + gameObject.name);
        // ���⼭ ���ϴ� ������ ������ �� �ֽ��ϴ�.

        BossNoteDetailScript DetailBossNote = UIManager.Instance.BossNoteDetailPanel;
        if (UIManager.Instance.BossNoteDetailPanel.gameObject.activeSelf == false)
        {
            UIManager.Instance.BossNoteDetailPanel.gameObject.SetActive(true);
        }
       
        DetailBossNote.SetNoteData(bossNote);


    }




    void BossAppear()//�׽�Ʈ
    {
        BossMonster BM = FindObjectOfType<BossMonster>();
        BM.Appear();
       
    }

    void BossDisappear()
    {
        BossMonster BM = FindObjectOfType<BossMonster>();
        BM.Disappear();
        
    }










}
