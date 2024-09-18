using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SongSelect : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public AudioSource SongAudio;
    public float Volume;

    public void Start()
    {
        Debug.Log("내가 먼저 실행");
        Debug.Log("내가 먼저 실행");
        InitializeSongSelect();
    }

    public void InitializeSongSelect()
    {
        Debug.Log("초기화 진행중");
        if(AudioManager.Instance.GetAudio() == null)
        {
            AudioManager.Instance.SetAudio(this.GetComponent<SongSelect>());
        }
        

        //SongAudio = GetComponent<AudioSource>();
        GameManager.Instance.MainAudio = SongAudio;
        
        //if (GameManager.Instance.DataState == GameDataState.FinishData_Load)
        //{
        //    PlaySong();
        //}
        
    }

    public void PlaySong() //이벤트로 설정해줘야 할 것 같음
    {
        if(!GameManager.Instance.MainAudio.isPlaying)
        {
            SongAudio.clip = GameManager.Instance.musicInfo.Music;
            
            StartCoroutine(PreviewSong());
        }
    }



    IEnumerator PreviewSong()
    {
        while(true)
        {
            if(!SongAudio.isPlaying)
            {
                SongAudio.volume = 0.5f;
                SongAudio.PlayScheduled(0);
            }

            if (SongAudio.time <= 20 && SongAudio.time > 15)
            {
                if (SongAudio.volume > 0)
                {
                    SongAudio.volume -= 0.15f * Time.deltaTime;
                }
            }

            if (SongAudio.time > 20)
            {
                SongAudio.Stop();
            }
            yield return null;

        }

        
    }
}
