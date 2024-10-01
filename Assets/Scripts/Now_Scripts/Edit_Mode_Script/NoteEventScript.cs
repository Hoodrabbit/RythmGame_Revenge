using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class NoteEventScript : MonoBehaviour
{
    public EventType eventType;

    float SongTime;


    bool Used = false;

    private void Start()
    {
        if (GameManager.Instance.state == GameState.Play_Mode)
        {
            SpriteRenderer SR = GetComponent<SpriteRenderer>();
            //GameObject childobj = GetComponentInChildren<Canvas>().gameObject;
           // childobj.SetActive(false);       
            //SR.color = new Color(0, 0, 0, 0);
        }
    }


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
