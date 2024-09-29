using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public EventType NowEvent;
    float SongTime; //�ϴ� ������ xpos�� üũ�ϵ��� �ϴ°� ���� ����

    public List<EventInfoAll> EventList = new List<EventInfoAll>();
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


                

                Debug.Log("�ٱ��� ��Ʈ ����");

                break;
            case EventType.SpawnOutside_Reverse:

                Debug.Log("���� �ٱ��� ��Ʈ ����");

                break;

            case EventType.End:

                Debug.Log("�̺�Ʈ ����");

                break;
        }
    }


    //����
    void RefreshNoteEventMethod()
    {
        DataManager.Instance.ListNullCheck();
        EventList = DataManager.Instance.EventNotes;

        Debug.Log("�̺�Ʈ �ߵ�");
        RefreshNoteEvent?.Invoke();
    }














}


