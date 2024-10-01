using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public EventType NowEvent;
    float SongTime; //일단 지금은 xpos로 체크하도록 하는게 좋을 듯함

    public List<NoteEventInfoPos> EventList = new List<NoteEventInfoPos>();
    public Action RefreshNoteEvent;
    //public List<>

    protected override void Awake()
    {
        base.Awake();
       
    }

    private void Start()
    {
        DataManager.Instance.EventCheck += RefreshNoteEventMethod;

    }
    private void OnDisable()
    {
        DataManager.Instance.EventCheck -= RefreshNoteEventMethod;
    }


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
           // EventCheck();
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



    //현재 빌드 상에서 제대로 실행이 되지 않음
    void RefreshNoteEventMethod()
    {
        DataManager.Instance.ListNullCheck();
        EventList = DataManager.Instance.NoteEventList;

        Debug.Log("이벤트 총 갯수 : " + DataManager.Instance.NoteEventList.Count);


        Debug.Log("이벤트 발동");
        RefreshNoteEvent?.Invoke();
    }














}


