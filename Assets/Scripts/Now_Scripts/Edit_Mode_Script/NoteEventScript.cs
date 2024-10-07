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

    public Animator Boss_Animator;



    private void Start()
    {
        if (GameManager.Instance.state == GameState.Play_Mode)
        {
            SpriteRenderer SR = GetComponent<SpriteRenderer>();
            GameObject childobj = GetComponentInChildren<Canvas>().gameObject;
            childobj.SetActive(false);       
            SR.color = new Color(0, 0, 0, 0);
        }
    }


    protected virtual void Update()
    {
        if(!Used && transform.position.x <0)
        { 
            //EventManager.Instance.SetEvent(eventType, SongTime);
            Used = true;

        }
        
        if(Used && transform.position.x >0) 
        { 
            //EventManager.Instance.EndEvent(); 
            Used = false; 
        }
    }




    public void SetSongTime(float songtime)
    {
        SongTime = songtime;
    }

}
