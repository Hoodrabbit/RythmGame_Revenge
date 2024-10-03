using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public EventType NowEvent;
    float SongTime; //�ϴ� ������ xpos�� üũ�ϵ��� �ϴ°� ���� ����

    public List<EventInfoAll> EventList = new List<EventInfoAll>();
    public Action RefreshNoteEvent = delegate { };

    protected void Awake()
    {
        //base.Awake();
        if (DataManager.Instance == null)
        {
            Debug.Log("�̺�Ʈ�� NULL �Դϴ�.");
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



    //���� ���� �󿡼� ����� ������ ���� ����
    public void RefreshNoteEventMethod()
    {
        DataManager.Instance.ListNullCheck();
        EventList = DataManager.Instance.EventNotes;

        Debug.Log("�̺�Ʈ �� ���� : " + DataManager.Instance.EventNotes.Count);


        Debug.Log("�̺�Ʈ �ߵ�");
        RefreshNoteEvent?.Invoke();
    }














}


