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

// => ������ ���� ��� �״��
// <= ������ ���� ����� �ݴ� �����̾����� ���� �����̾����� ����

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




            //BossNote bossNote = new BossNote();

            //public BossNoteType bossEventType;





            ////bool UnPlaying

            //private float clickTime = 0f;  // ���� Ŭ�� �ð�
            //private float clickDelay = 0.3f;  // ���� Ŭ�� ��� �ð� (0.3�� �̳��� ���� Ŭ���ؾ� ���� Ŭ������ �ν�)
            //private int clickCount = 0;  // Ŭ�� Ƚ��




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
            //        //������ �����ϴ� �ð� �ٽ� �������ֱ� 



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


            //void BossAppear()//�׽�Ʈ
            //{
            //    BossMonster BM = FindObjectOfType<BossMonster>();
            //    BM.Appear();

            //}

            //void BossDisappear()
            //{
            //    BossMonster BM = FindObjectOfType<BossMonster>();
            //    BM.Disappear();

            //}










        
