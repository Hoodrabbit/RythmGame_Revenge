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
            //GameObject childobj = GetComponentInChildren<Canvas>().gameObject;
           // childobj.SetActive(false);       
            //SR.color = new Color(0, 0, 0, 0);
        }
    }


    private void Update()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss" && eventType == EventType.Dash)
        {

            //보스 오브젝트에 돌진을 하라는 신호를 보냄
            //수정 예정 보스 오브젝트를 잠시 꺼줌 
            BossMonster boss = collision.GetComponent<BossMonster>();
            boss.BossDash(this.transform);

            //보스 오브젝트와 충돌하면서 보스의 애니메이션 정보를 얻어감
            Boss_Animator = boss.GetAnimator();


            //collision.transform.parent = transform;
            //collision.transform.localPosition = Vector3.zero;
        }
    }






}
