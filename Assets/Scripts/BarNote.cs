using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class BarNote : MonoBehaviour
{

    public GameObject ContanierNote;
    public GameObject MinutenessContanierNote;
    public float TTime = 0;
    public float NowBPM;
    public float StdBPM;
    public int speed;
    bool first = true;
    public List<GameObject> CNote;
    float Distance;

    // Start is called before the first frame update
    void Start()
    {
        float NextBeat = StdBPM/ NowBPM;
        int count = 0;
        Debug.Log(NextBeat);
        Debug.Log(GameManager.Instance.MainAudio.clip.length);
        while (GameManager.Instance.MainAudio.clip.length > TTime)
        {

            if (first == false)
            {
                if(count <4)
                {
                    GameObject NNote = Instantiate(MinutenessContanierNote, new Vector3(transform.position.x + (TTime + NextBeat) * speed, 0, 0), Quaternion.identity,transform);
                    CNote.Add(NNote);
                    TTime += NextBeat;
                    count++;
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
                GameObject NNote = Instantiate(ContanierNote, new Vector3(0, 0, 0), Quaternion.identity, transform);
                CNote.Add(NNote);
                count++;
                first = false;
            }
            //TTime += NextBeat * 4;
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

}
