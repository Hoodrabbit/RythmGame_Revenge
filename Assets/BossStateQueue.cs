using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEventNoteInfo
{
    Transform DashEventNote;
    float Songtime;

    public DashEventNoteInfo(Transform dashevent, float songTime)
    {
        DashEventNote = dashevent; Songtime = songTime;
    }

    public Transform GetEventNotetransform()
    {
        return DashEventNote;
    }

    public float GetSongTime()
    {
        return Songtime;
    }



}

public class BossStateQueue : MonoBehaviour
{
    //보스의 상태를 구분하고 현재 제일 위의 큐가 실행되고 바로 다음 큐가 작동될 수 있도록 
    //그러면 돌진 등 속도가 더 빠르게 해야 하지 않나?
    bool StartQueue = true;

    public BossMonster bossMonster;

    public void Awake()
    {
        bossMonster = FindObjectOfType<BossMonster>();
    }


    private Queue<DashEventNoteInfo> BossDashTime = new Queue<DashEventNoteInfo>();

    public void EnqueueInBossQueue(Transform dashevent, float songTime)
    {
        DashEventNoteInfo info = new DashEventNoteInfo(dashevent, songTime);

        BossDashTime.Enqueue(info);

        if (StartQueue && GameManager.Instance.BossAppear == true)
        {
            Debug.Log("여기에서 실행 됬어요");

            StartDash();
            StartQueue = false;
        }

    }


    //처음 시작할 때도 명령을 내려야 함
    //히트 혹은 히트 미스 시에 바로 startdash 명령을 내리도록 해야 함
    //보스에게 지금 대쉬를 하라고 명령을 내리고 명령을 내리면서 nowtime을 시간도 같이 전해줌
    public void StartDash()
    {
        if (BossDashTime.Count > 0)
        {

            DashEventNoteInfo info = BossDashTime.Dequeue();
            //if (info.GetSongTime() < 1.6f)
            //{
            //    bossMonster.bossAnimation.PlayDashAniEnd();
            //}
            //else
            //{
            //    bossMonster.bossAnimation.PlayDashAniStart();
            //}


            bossMonster.BossDash(info.GetEventNotetransform(), info.GetSongTime());
            //보스 스크립트에 해당 시간을 매개변수로 한 함수 전달함
        }
        else
        {
            StartQueue = true;
        }
    }
    public bool IsBossQueueExist()
    {
        if (BossDashTime.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }






}
