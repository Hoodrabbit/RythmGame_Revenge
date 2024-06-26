using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MusicStart : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject noteprefab;
    public GameObject metronome;
    public GameObject metronomeLine;

    bool SongStart = false;

    float CheckSongStart =0;
    float ttime = 0;
    int count = 4;

    private void Start()
    {
        Invoke("DelayedPlay", 3f);
    }

    void DelayedPlay()
    {
        audioSource.Play();
        CheckSongStart = (float)AudioSettings.dspTime;
        metronome.SetActive(true);
        SongStart = true;
        //StartCoroutine(MakeNote());
        //MusicStartCheck = AudioSettings.dspTime;
    }

    private void Update()
    {

        if (SongStart == true)
        {
            ttime += Time.deltaTime;

            if (ttime >= (float)60 / 130f)
            {
                Instantiate(noteprefab, new Vector2(transform.position.x + 0.0001f * ((float)AudioSettings.dspTime - CheckSongStart) - 0.2f, transform.position.y), Quaternion.identity);
                if (count == 4)
                {
                    Instantiate(metronomeLine, new Vector2(transform.position.x + 0.0001f * ((float)AudioSettings.dspTime - CheckSongStart) - 0.2f, transform.position.y), Quaternion.identity);
                    count = 0;
                }
                count++;

                ttime -= (float)60 / 130f;
            }
        }
        


    }



}
