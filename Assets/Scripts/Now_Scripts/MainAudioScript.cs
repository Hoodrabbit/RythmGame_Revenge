using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudioScript : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();


        if(GameManager.Instance.state != GameState.Offset_Mode)
        {
            InitalAudio();
        }
        else
        {
            InitalAudio();
            GameManager.Instance.PlayMusicOnly();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    ExpandScreen();
        //}
    }

    public void ExpandScreen()
    {
        if (Camera.main.orthographicSize < 15)
        {
            StartCoroutine(CameraSizeUP());
        }
        else
        {
            StartCoroutine(CameraSizeDown());
        }



    }

    IEnumerator CameraSizeUP()
    {
        int CameraSize = 13;
        float CurrentTime = 0f;
        while (Camera.main.orthographicSize < CameraSize)
        {
            CurrentTime += Time.deltaTime;

            float CurrentValue = Mathf.Lerp(9, CameraSize, CurrentTime / 0.3f);
            float CurrentSize = Mathf.Lerp(1, 2, CurrentTime / 5f);


            Debug.Log("작동");
            Camera.main.orthographicSize = CurrentValue;
            //transform.localScale = new Vector2(CurrentSize, CurrentSize);



            yield return null;
        }
    }

    IEnumerator CameraSizeDown()
    {
        int CameraSize = 13;
        float CurrentTime = 0f;
        while (Camera.main.orthographicSize > 9)
        {
            CurrentTime += Time.deltaTime;

            float CurrentValue = Mathf.Lerp(CameraSize, 9, CurrentTime / 0.3f);
            float CurrentSize = Mathf.Lerp(2, 1, CurrentTime / 5f);


            Debug.Log("작동");
            Camera.main.orthographicSize = CurrentValue;
            //transform.localScale = new Vector2(CurrentSize, CurrentSize);


            yield return null;
        }
    }




    void InitalAudio()
    {
        if(GameManager.Instance.state != GameState.Offset_Mode) 
        {
            audioSource.clip = GameManager.Instance.musicInfo.Music;
        }
        
        
        GameManager.Instance.SetAudio(audioSource);

        if(GameManager.Instance.state == GameState.Play_Mode)
        {
            GameManager.Instance.PlayMusic();
        }
        
    }

}
