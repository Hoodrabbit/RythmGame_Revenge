using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    NoteParsing ppppasring;
    public AudioSource MainBGM;
    public boxbox judgeMentUP;
    public boxbox judgeMentDOWN;
    public GameObject Note;
    public float BPM;
    public double MusicStartCheck;
    float Sync_offset;
    float BPM_S;
    bool musicstart = false;

    // Start is called before the first frame update
    void Start()
    {
        Sync_offset =MainBGM.timeSamples / MainBGM.clip.frequency;
        ppppasring = gameObject.GetComponent<NoteParsing>();
        BPM_S = BPM / 60f;
        //MusicStartCheck = AudioSettings.dspTime;
        Invoke("DelayedPlay",3f);
        
        //노래의 총길이를 받아서 BPM_S 만큼 일정 거리를 두고 라인을 출력하는 것?
    }

    void DelayedPlay()
    {
        MainBGM.Play();
        Debug.Log(AudioSettings.dspTime);
        MusicStartCheck = AudioSettings.dspTime;
        musicstart = true;
        StartCoroutine(MakeNote());
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

   

    IEnumerator MakeNote()
    {
        //yield return new WaitForSeconds(10f);
        int[] NoteLine = ppppasring.noteLaneNum;
        for (int i = 0; i < ppppasring.num; i++)
        {
            //Debug.Log(NoteLine[i]);
            if(NoteLine[i] == 1)
            {
                NoteLine[i] = -2;
            }
            else
            {
                NoteLine[i] = +2;
            }
            //float time = 1;
            
        
        }



        for (int i = 0; i < ppppasring.num; i++)
        {

            if (NoteLine[i] == 2) //Up
            {
                GameObject notee = Instantiate(Note, new Vector3(15 +(float)(AudioSettings.dspTime - MusicStartCheck)+ 0.2f + 10 * (ppppasring.noteTime[i] * 0.001f), NoteLine[i], 0), Quaternion.identity, transform);;
                judgeMentUP.Note.Add(notee);
            }
            else if (NoteLine[i] == -2) //Down
            {
                GameObject notee = Instantiate(Note, new Vector3(15 + 0.2f+(float)(AudioSettings.dspTime - MusicStartCheck) + 10 * (ppppasring.noteTime[i] * 0.001f), NoteLine[i], 0), Quaternion.identity, transform);
                judgeMentDOWN.Note.Add(notee);
            }



            //judgeMent.Note.Add(Instantiate(Note, new Vector3(0.23076923f + 10 * (ppppasring.noteTime[i] * 0.001f), NoteLine[i],0), Quaternion.identity,transform));//얕은 복사가 일어나서 getcomponent
            //GameObject notee = Instantiate(Note, new Vector3(0.23076923f + 10 * (ppppasring.noteTime[i] * 0.001f), NoteLine[i], 0), Quaternion.identity, transform);
            //judgeMent.Note.Add(notee);

        }
            




        yield return null;
    }

}
