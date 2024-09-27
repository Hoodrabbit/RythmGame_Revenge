using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteEventScript : MonoBehaviour
{
    public EventType eventType;

    float SongTime;


    bool Used = false;

    private void Update()
    {
        if(!Used && transform.position.x <0)
        { 
            EventManager.Instance.SetEvent(eventType, SongTime);
            Used = true;

        }
        
        if(Used && transform.position.x >0) 
        { 
            EventManager.Instance.EndEvent(); 
            Used = false; 
        }
    }




    public void SetSongTime(float songtime)
    {
        SongTime = songtime;
    }

    public void EventOn()
    {
        //���ķ� �����ϴ� ��Ʈ�� ī�޶� �ۿ��� �������� �������

        //���ķ� �����ϴ� ��Ʈ�� ī�޶� �ۿ��� �����Ǽ� �������� �������
        //ũ�ν��� �Ͼ���� ��

        //�ش� �̺�Ʈ�� �����Ŵ
    }







}
