using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppearNote : Note
{

    bool Use = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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


                //boss Appear Method;
                //event on
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


                //boss Appear Method;
                //event on
            }
        }

    }

    void BossAppear()//Å×½ºÆ®
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
