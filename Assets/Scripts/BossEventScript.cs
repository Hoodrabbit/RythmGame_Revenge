using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossNote : Note 
{
    //public int NoteType_Boss;
    public int TapCount;
    public bool Invisble = true;


}

// => 정방향 원래 기능 그대로
// <= 역방향 원래 기능의 반대 입장이었으면 퇴장 퇴장이었으면 입장

public class BossEventScript : NoteEventScript
{
    public bool Use = false;

    bool pass = false;


    protected override void Update()
    {
        if (GameManager.Instance.MainAudio.isPlaying == true)
        {

            if (transform.position.x <= 0)
            {
                if (!Use)
                {
                    Use = true;
                    if (eventType == EventType.Appear)
                    {
                        BossAppear();
                    }
                    else if (eventType == EventType.Disappear)
                    {
                        BossDisappear();
                    }
                }
                else
                {
                    //보스가 존재하는 시간 다시 설정해주기 



                    if (transform.position.x > 0)
                    {
                        if (Use)
                        {
                            Use = false;
                            if (eventType == EventType.Appear )
                            {
                                BossAppear();
                            }
                            else if (eventType == EventType.Disappear )
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

                    if (transform.position.x < 0)
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

        }
        else
        {
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

            }

        }

        void BossAppear()//테스트
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
}





        
