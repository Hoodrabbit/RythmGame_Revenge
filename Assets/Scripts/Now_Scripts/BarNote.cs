using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BarNote : MonoBehaviour
{

    public GameObject ContanierNote;
    public GameObject MinutenessContanierNote;
    public GameObject MinutenessContanierNoteDetail;
    public float TTime = 0;
    public float NowBPM;
    public float StdBPM;
    public int speed;
    bool first = true;
    public List<GameObject> CNote;
    public List<GameObject> UnActiveNote;
    float Distance;

    // Start is called before the first frame update
    void Start()
    {
        NowBPM = GameManager.Instance.musicInfo.BPM;
        StdBPM = 60;
        float NextBeat = StdBPM/ NowBPM/4;
        int count = 0;//현재 박자 수 체크용 멤버 변수
        Debug.Log(NextBeat);
        //Debug.Log(GameManager.Instance.MainAudio.clip.length);
        while (GameManager.Instance.musicInfo.Music.length > TTime)
        {

            if (first == false)
            {
                if(count == 2)
                {
                    GameObject NNote = Instantiate(MinutenessContanierNote, new Vector3(transform.position.x + (TTime + NextBeat) * speed, 0, 0), Quaternion.identity, transform);
                    CNote.Add(NNote);
                    TTime += NextBeat;
                    count++;
                }

                if(count <4)
                {
                    GameObject NNote = Instantiate(MinutenessContanierNoteDetail, new Vector3(transform.position.x + (TTime + NextBeat) * speed, 0, 0), Quaternion.identity,transform);
                    CNote.Add(NNote);
                    UnActiveNote.Add(NNote);
                    TTime += NextBeat;
                    count++;

                    NNote.SetActive(false);
                }
                else if(count >=4)
                {
                    GameObject NNote = Instantiate(ContanierNote, new Vector3(transform.position.x + (TTime + NextBeat) * speed, 0, 0), Quaternion.identity, transform);
                    CNote.Add(NNote);
                    TTime += NextBeat;
                    count = 1;
                }
                
            }
            else
            {
                GameObject NNote = Instantiate(ContanierNote, new Vector3(transform.position.x, 0, 0), Quaternion.identity, transform);
                CNote.Add(NNote);
                count++;
                first = false;
            }
        }
        Debug.Log(TTime);

        Distance = CNote[CNote.Count - 1].transform.position.x;

        Debug.Log(Distance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetDistance()
    {
        return Distance;
    }

    public void ActiveNote()
    {
        for(int i=0; i< UnActiveNote.Count; i++)
        {
            if (UnActiveNote[i].activeSelf == false)
            {
                UnActiveNote[i].SetActive(true);
            }
            else
            {
                UnActiveNote[i].SetActive(false);
            }
            
        }
    }



}
