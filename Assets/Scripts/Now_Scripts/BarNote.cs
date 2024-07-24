using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BarNote : MonoBehaviour
{

    public GameObject ContanierNote;
    public GameObject NextNote;
    public GameObject MinutenessContanierNote2;
    public GameObject MinutenessContanierNote4;
    public GameObject MinutenessContanierNote8;
    public float TTime = 0;
    public float NowBPM;
    public float StdBPM;
    public int speed;
    bool first = true;
    public List<GameObject> CNote;
    public List<GameObject> UnActiveNote;
    float Distance;

    int Beat = 8;

    // Start is called before the first frame update
    void Awake()
    {
        NowBPM = GameManager.Instance.musicInfo.BPM;
        StdBPM = 60;
        float NextBeat = StdBPM/ NowBPM/ Beat; //비트 만큼 나눠서 박자노트 만들어줌
        int count = 0;//현재 박자 수 체크용 멤버 변수
        //Debug.Log(NextBeat);
        //Debug.Log(GameManager.Instance.MainAudio.clip.length);
        while (GameManager.Instance.musicInfo.Music.length >= TTime)
        {

            if (first == false)
            {
                if(count % 2 == 0 && count != Beat/2 && count != 0 && Beat > count)
                {
                    GameObject NNote = Instantiate(MinutenessContanierNote4, new Vector3(transform.position.x + (TTime + NextBeat) * speed, 0, 0), Quaternion.identity, transform);
                    CNote.Add(NNote);
                    TTime += NextBeat;
                    count++;
                }
                else if (count % 2 != 0)
                {
                    GameObject NNote = Instantiate(MinutenessContanierNote8, new Vector3(transform.position.x + (TTime + NextBeat) * speed, 0, 0), Quaternion.identity, transform);
                    CNote.Add(NNote);
                    TTime += NextBeat;
                    count++;
                }
                else if(count == Beat / 2)
                {
                    GameObject NNote = Instantiate(MinutenessContanierNote2, new Vector3(transform.position.x + (TTime + NextBeat) * speed, 0, 0), Quaternion.identity,transform);
                    CNote.Add(NNote);
                    UnActiveNote.Add(NNote);
                    TTime += NextBeat;
                    count++;

                    //NNote.SetActive(false);
                }
                else if(count >= Beat)
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
        //Debug.Log(TTime);

        Distance = CNote[CNote.Count - 1].transform.position.x;

        //Debug.Log(Distance);
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
