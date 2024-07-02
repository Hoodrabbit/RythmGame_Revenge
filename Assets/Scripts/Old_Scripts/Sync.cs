using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : MonoBehaviour
{
    float MusicBPM = 128;
    float stdBPM = 60;
    float MusicTempo = 4;
    float stdTempo = 4;
    float NextTime = 0;
    float Temp;
    int count = 0;
    double MusicStartCheck;

    AudioSource PlayTik;
    public AudioClip Tik;
    public RotateNote note;

    private void Start()
    {
        PlayTik = GetComponent<AudioSource>();
        //MusicStartCheck = AudioSettings.dspTime;
        //PlayTik.PlayOneShot(Tik);
        Temp = (stdBPM / MusicBPM) * (MusicTempo / stdTempo);

    }

    private void Update()
    {
       
        NextTime += Time.smoothDeltaTime;

        if (NextTime >= Temp)
        {
            PlayTik.PlayOneShot(Tik);
            //StartCoroutine(PlayTikSound(Temp));
            //NextTime = 0;
            count++;
            NextTime -= Temp;
            
        }

    }


    //IEnumerator PlayTikSound(float TickTime)
    //{
    //    Debug.Log(NextTime);
    //    PlayTik.PlayOneShot(Tik);
    //    note.Rotate();
    //    yield return null;
    //    //yield return new WaitForSeconds(TickTime);
    //}


}
