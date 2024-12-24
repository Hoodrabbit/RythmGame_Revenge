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
            // 큐가 2개 이상인지 확인
            if (BossDashTime.Count > 1)
            {
                // 큐 복사를 통해 마지막 노트의 정보를 얻음
                DashEventNoteInfo[] tempArray = BossDashTime.ToArray(); // 큐를 배열로 변환
                DashEventNoteInfo lastNote = tempArray[tempArray.Length - 1]; // 마지막 요소 참조

                // 마지막 노트의 시간 정보 확인
                if (lastNote.GetSongTime() > 1f)
                {
                    Debug.Log($"마지막 노트의 시간이 1보다 큼: {lastNote.GetSongTime()}");
                }
                else
                {
                    Debug.Log($"마지막 노트의 시간이 1 이하임: {lastNote.GetSongTime()}");
                }
            }

            // 대쉬 시작 로직
            DashEventNoteInfo info = BossDashTime.Dequeue();
            bossMonster.BossDash(info.GetEventNotetransform(), info.GetSongTime());
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
