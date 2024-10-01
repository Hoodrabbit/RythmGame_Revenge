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
        //이후로 생성하는 노트를 카메라 밖에서 나오도록 만들어줌

        //이후로 생성하는 노트를 카메라 밖에서 반전되서 나오도록 만들어줌
        //크로스가 일어나도록 함

        //해당 이벤트를 종료시킴
    }







}
