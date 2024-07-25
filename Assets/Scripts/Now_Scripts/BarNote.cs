using JetBrains.Annotations;
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

    public GameObject Barrrr;

    public float TTime = 0;
    public float NowBPM;
    public float StdBPM;
    public int speed;

    public float OffsetTime = 0;

    bool first = true;
    public List<GameObject> CNote;
    public List<GameObject> UnActiveNote_2;
    public List<GameObject> UnActiveNote_4;
    public List<GameObject> UnActiveNote_8;
    float Distance =0;
    float NextBeat; //비트 만큼 나눠서 박자노트 만들어줌
    int Hit_value = 0;
    int count = 0;//현재 박자 수 체크용 멤버 변수

    int Beat = 8;

    bool Devide = false;
    bool End = false;

    // Start is called before the first frame update
    void Awake()
    {
        NowBPM = GameManager.Instance.musicInfo.BPM;
        StdBPM = 60;
        NextBeat = StdBPM / NowBPM / Beat;
        speed = GameManager.Instance.speed;
        //Debug.Log(NextBeat);
        //Debug.Log(GameManager.Instance.MainAudio.clip.length);
        if (GameManager.Instance.musicInfo.Music.length % GameManager.Instance.GetBPS() == 0)
        {
            Debug.Log(GameManager.Instance.musicInfo.Music.length);
            Debug.Log(GameManager.Instance.GetBPS());
            Debug.Log(NextBeat);

            Devide = true;
        }
        else
        {
            Debug.Log(GameManager.Instance.musicInfo.Music.length);
            Debug.Log(GameManager.Instance.GetBPS());
        }


        if (Devide == true)
        {
            MakeBar();
            Debug.Log("나눠짐");
        }
        else
        {
            MakeMoreBar();
        }


    }

    public void MakeBar()
    {

        while (GameManager.Instance.musicInfo.Music.length >= TTime)
        {
            if (first == false)
            {
                if (count % 2 == 0 && count != Beat / 2 && count != 0 && Beat > count)
                {
                    GameObject NNote = Instantiate(MinutenessContanierNote4, new Vector3(transform.position.x + (TTime + NextBeat) * speed + OffsetTime, 0, 0), Quaternion.identity, transform);
                    CNote.Add(NNote);
                    UnActiveNote_4.Add(NNote);
                    TTime += NextBeat;
                    count++;
                    
                    NNote.SetActive(false);
                }
                else if (count % 2 != 0)
                {
                    GameObject NNote = Instantiate(MinutenessContanierNote8, new Vector3(transform.position.x + (TTime + NextBeat) * speed+ OffsetTime, 0, 0), Quaternion.identity, transform);
                    CNote.Add(NNote);
                    UnActiveNote_8.Add(NNote);
                    TTime += NextBeat;
                    count++;

                    NNote.SetActive(false);
                }
                else if (count == Beat / 2)
                {
                    GameObject NNote = Instantiate(MinutenessContanierNote2, new Vector3(transform.position.x + (TTime + NextBeat) * speed + OffsetTime, 0, 0), Quaternion.identity, transform);
                    CNote.Add(NNote);
                    UnActiveNote_2.Add(NNote);
                    TTime += NextBeat;
                    count++;
                    NNote.SetActive(false);
                    //NNote.SetActive(false);
                }
                else if (count >= Beat)
                {
                    GameObject NNote = Instantiate(ContanierNote, new Vector3(transform.position.x + (TTime + NextBeat) * speed + OffsetTime, 0, 0), Quaternion.identity, transform);
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

            OffsetTime +=NextBeat/2;
            Debug.Log(OffsetTime);
        }
        //Debug.Log(TTime);

        Distance = CNote[CNote.Count - 1].transform.position.x;

        //Debug.Log(Distance);
    }
    public void MakeMoreBar()
    {
        
        MakeBar();

        while (GameManager.Instance.musicInfo.Music.length+NextBeat >= TTime || End==false)
        {
            //OffsetTime = NextBeat / 20;
            if (first == false)
            {
                if (count % 2 == 0 && count != Beat / 2 && count != 0 && Beat > count)
                {
                    GameObject NNote = Instantiate(Barrrr, new Vector3(transform.position.x + (TTime + NextBeat) * speed + OffsetTime, 0, 0), Quaternion.identity, transform);
                    CNote.Add(NNote);
                    UnActiveNote_4.Add(NNote);
                    TTime += NextBeat;
                    count++;

                    NNote.SetActive(false);
                }
                else if (count % 2 != 0)
                {
                    GameObject NNote = Instantiate(Barrrr, new Vector3(transform.position.x + (TTime + NextBeat) * speed + OffsetTime, 0, 0), Quaternion.identity, transform);
                    CNote.Add(NNote);
                    UnActiveNote_8.Add(NNote);
                    TTime += NextBeat;
                    count++;
                    NNote.SetActive(false);
                    if (count == Beat)
                    {
                        End= true;
                        break;
                    }
                    
                }
                else if (count == Beat / 2)
                {
                    GameObject NNote = Instantiate(Barrrr, new Vector3(transform.position.x + (TTime + NextBeat) * speed + OffsetTime, 0, 0), Quaternion.identity, transform);
                    CNote.Add(NNote);
                    UnActiveNote_2.Add(NNote);
                    TTime += NextBeat;
                    count++;
                    NNote.SetActive(false);
                    //NNote.SetActive(false);
                }
                else if (count >= Beat)
                {
                    GameObject NNote = Instantiate(Barrrr, new Vector3(transform.position.x + (TTime + NextBeat) * speed + OffsetTime, 0, 0), Quaternion.identity, transform);
                    CNote.Add(NNote);
                    TTime += NextBeat;
                    count = 1;
                }

            }

            OffsetTime += NextBeat;

        }
        //Debug.Log(TTime);

        Distance = CNote[CNote.Count - 1].transform.position.x;

        //Debug.Log(Distance);
    }
    public float GetDistance()
    {
        return Distance;
    }

    public void ActiveNote()
    {
        if (Hit_value < 3)
        {
            Active(Hit_value, true);
            Hit_value++;
        }
        else
        {
            Active(Hit_value, false);
            Hit_value = 0;
        }
    }

    void Active(int value, bool IsVisible)
    {

        if (IsVisible)
        {
            switch (value)
            {
                case 0:
                    foreach (var Note in UnActiveNote_2)
                    {
                        Note.SetActive(true);
                    }
                    break;
                case 1:
                    foreach (var Note in UnActiveNote_4)
                    {
                        Note.SetActive(true);
                    }
                    break;
                case 2:
                    foreach (var Note in UnActiveNote_8)
                    {
                        Note.SetActive(true);
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            foreach (var Note in UnActiveNote_2)
            {
                Note.SetActive(false);
            }
            foreach (var Note in UnActiveNote_4)
            {
                Note.SetActive(false);
            }
            foreach (var Note in UnActiveNote_8)
            {
                Note.SetActive(false);
            }

        }



    }

}
