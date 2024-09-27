using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public EventType NowEvent;
    float SongTime; //일단 지금은 xpos로 체크하도록 하는게 좋을 듯함

    //public List<>



    public EventType GetEvent()
    {
        return NowEvent;
    }
    
    public void SetEvent(EventType EventT, float Time)
    {
        NowEvent = EventT;
        SongTime = Time;
    }

    public void EndEvent()
    {
        NowEvent = EventType.End;
    }


    private void Update()
    {
        if(SongTime < GameManager.Instance.MainAudio.time && NowEvent != EventType.End)
        {
            EventCheck();
        }
    }

    public void EventCheck()
    {
        switch(NowEvent)
        {
            case EventType.SpawnOutside:


                

                Debug.Log("바깥쪽 노트 스폰");

                break;
            case EventType.SpawnOutside_Reverse:

                Debug.Log("역순 바깥쪽 노트 스폰");

                break;

            case EventType.End:

                Debug.Log("이벤트 종료");

                break;
        }
    }


    //더미
    void eventchecker()
    {
        //노트가 디버그 찍을 수 있도록




    }














}


