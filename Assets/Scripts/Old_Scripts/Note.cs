using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Note : MonoBehaviour
{
    bool Istrigger = false;
    double TTime = 0;
    //float BPM_S = 128f / 60f;
    double spawnTime=0;
    double targetTime;
    Vector2 StartPos;
    //NoteController controller;
    // Start is called before the first frame update
    void Start()
    {
        //StartPos = transform.position;
        //Invoke("CheckTime", 3f);
        //spawnTime = AudioSettings.dspTime;
        //controller = FindObjectOfType<NoteController>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.left * 10 * Time.deltaTime);
            //double currentTime = AudioSettings.dspTime;
            //double timeElapsed = currentTime - spawnTime;
            //double totalTravelTime = targetTime - spawnTime;

            //if (timeElapsed < totalTravelTime)
            //{
            //    float t = (float)(timeElapsed / totalTravelTime);
            //    transform.position = Vector3.Lerp(StartPos, new Vector2(-6.312f, transform.position.y), t);
            //}
            //else
            //{
            //    //transform.position = new Vector2(-6.312f, transform.position.y);
            //    // 타겟에 도달한 이후 처리
            //}
        
        

        //transform.position = new Vector2((transform.position.x - BPM_S * Time.fixedDeltaTime), transform.position.y);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "TimingCheck")
    //    {
    //        controller.PlayMusic();


    //    }
    //}


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }

    void CheckTime()
    {
        spawnTime = AudioSettings.dspTime;
    }
    


}
