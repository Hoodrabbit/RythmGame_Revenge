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
    bool Use = false;

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
                    //보스가 존재하는 시간 다시 설정해주기 



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




            //BossNote bossNote = new BossNote();

            //public BossNoteType bossEventType;





            ////bool UnPlaying

            //private float clickTime = 0f;  // 이전 클릭 시간
            //private float clickDelay = 0.3f;  // 더블 클릭 허용 시간 (0.3초 이내로 연속 클릭해야 더블 클릭으로 인식)
            //private int clickCount = 0;  // 클릭 횟수




            //// Start is called before the first frame update
            //protected override void Start()
            //{
            //    base.Start();
            //    //bossNote.NoteType_Boss = 1;
            //    //bossNote.TapCount = 0;
            //    //bossNote.Invisble = true;

            //    if(GameManager.Instance.state == GameState.Play_Mode)
            //    {
            //        SpriteRenderer SR = GetComponent<SpriteRenderer>();
            //        SR.color = new Color(0, 0, 0, 0);
            //    }

            //}

            //// Update is called once per frame
            //void Update()
            //{


            //    //if (Input.GetMouseButtonDown(0))
            //    //{
            //    //    HandleClick();
            //    //}



            //    if (GameManager.Instance.MainAudio.isPlaying == true)
            //    {

            //        if (transform.position.x <= 0)
            //        {
            //            if (!Use)
            //            {
            //                Use = true;
            //                if (!GameManager.Instance.BossAppear)
            //                {
            //                    BossAppear();
            //                }
            //                else
            //                {
            //                    BossDisappear();
            //                }
            //            }


            //            //boss Appear Method;
            //            //event on
            //        }

            //    }
            //    else
            //    {
            //        //보스가 존재하는 시간 다시 설정해주기 



            //        if (transform.position.x >= 0)
            //        {
            //            if (Use)
            //            {
            //                Use = false;
            //                if (!GameManager.Instance.BossAppear)
            //                {
            //                    BossAppear();
            //                }
            //                else
            //                {
            //                    BossDisappear();
            //                }
            //            }
            //            else
            //            {

            //            }


            //            //boss Appear Method;
            //            //event on
            //        }

            //        if(transform.position.x < 0)
            //        {
            //            pass = true;

            //            if (!Use)
            //            {
            //                Use = true;
            //                if (!GameManager.Instance.BossAppear)
            //                {
            //                    BossAppear();
            //                }
            //                else
            //                {
            //                    BossDisappear();
            //                }
            //            }

            //        }


            //    }

            //}


            //void BossAppear()//테스트
            //{
            //    BossMonster BM = FindObjectOfType<BossMonster>();
            //    BM.Appear();

            //}

            //void BossDisappear()
            //{
            //    BossMonster BM = FindObjectOfType<BossMonster>();
            //    BM.Disappear();

            //}










        
