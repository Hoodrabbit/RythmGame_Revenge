using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public EventType NowEvent;
    float SongTime; //�ϴ� ������ xpos�� üũ�ϵ��� �ϴ°� ���� ����

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
    void eventchecker()
    {
        //��Ʈ�� ����� ���� �� �ֵ���




    }














}


