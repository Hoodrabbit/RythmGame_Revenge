using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public EventType NowEvent;
    float SongTime; //일단 지금은 xpos로 체크하도록 하는게 좋을 듯함

    public List<EventInfoAll> EventList = new List<EventInfoAll>();
    public Action RefreshNoteEvent = delegate { };

    protected void Awake()
    {
        //base.Awake();
        if (DataManager.Instance == null)
        {
            Debug.Log("이벤트가 NULL 입니다.");
        }
        else
        {
           // DataManager.Instance.EventCheck += RefreshNoteEventMethod;
        }
        
    }

    private void Start()
    {
        

    }
    private void OnDestroy()
    {
       // DataManager.Instance.EventCheck -= RefreshNoteEventMethod;
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
    public void RefreshNoteEventMethod()
    {
        DataManager.Instance.ListNullCheck();
        EventList = DataManager.Instance.EventNotes;

        Debug.Log("이벤트 총 갯수 : " + DataManager.Instance.EventNotes.Count);


        Debug.Log("이벤트 발동");
        RefreshNoteEvent?.Invoke();
    }














}


