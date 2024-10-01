using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BarNote : MonoBehaviour
{
    public BeatNoteLine_Visble_Status B_V_S; // 현재 시각화 되어있는 비트 라인들의 상태를 체크하기 위한 변수



    public GameObject BeatNote;
    public GameObject RhythmNote;

    [Header("박자 노트 종류")]
    public GameObject ContanierNote;
    public GameObject NextNote;
    public GameObject MinutenessContanierNote2;
    public GameObject MinutenessContanierNote4;
    public GameObject MinutenessContanierNote8;

    public GameObject SpareNote; // 여분 노트 박자가 끝나지 않았을 때 그만큼 추가로 더 생성시켜줌

    [Space(10f)]
    [Header("각종 변수들")]
    public float TTime = 0;
   
    public int speed;

    float NowBPM;
    float StdBPM = 60;
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



    bool Devide = false; //마디가 끝나기 전에 노래가 끝나는지 체크하여 마디가 끝날 때까지 추가로 제작할지 체크해주는 변수
    bool End = false;






    // Start is called before the first frame update
    void Awake()
    {
        InitializeBarNoteScript();
    }

    private void Update()
    {
      ChangeSpeed();
    }

    void InitializeBarNoteScript() //BarNote 스크립트 내 변수들 초기화 시켜주는 메서드
    {
        B_V_S = BeatNoteLine_Visble_Status.OneNoteInBar;
        Debug.Log(this);
        //EditManager.Instance.barNote = this;

        NowBPM = GameManager.Instance.musicInfo.BPM;

        NextBeat = StdBPM / NowBPM / Beat;

        speed = GameManager.Instance.speed;

        if (GameManager.Instance.musicInfo.Music.length % GameManager.Instance.GetBPS() == 0)
        {
            Devide = true;
        }


        if (Devide == true)
        {
            MakeBar();
            //Debug.Log("나눠짐");
        }
        else
        {
            MakeMoreBar();
        }


    }

    void ChangeSpeed()
    {
        TTime = 0;

        first = true;

        foreach (GameObject obj in CNote) 
        {
            if(first == false)
            {
                obj.transform.position = new Vector3(transform.position.x + (TTime + NextBeat) * GameManager.Instance.speed, 0, 0);
                TTime += NextBeat;
            }
            else
            {
                obj.transform.position = new Vector3(transform.position.x, 0, 0);
                first = false;
            }
            
        }
    }

    public void MakeBar()
    {

        while (GameManager.Instance.musicInfo.Music.length >= TTime)
        {
            if(first == true)
            {
                    GameObject NNote = Instantiate(ContanierNote, new Vector3(transform.position.x, 0, 0), Quaternion.identity, BeatNote.transform);
                    CNote.Add(NNote);
                    count++;
                    first = false;
            }
            else
            {
                if (count >= Beat)
                {
                    GameObject NNote = Instantiate(ContanierNote, new Vector3(transform.position.x + (TTime + NextBeat) * speed, 0, 0), Quaternion.identity, BeatNote.transform);
                    CNote.Add(NNote);
                    TTime += NextBeat;
                    count = 1;
                }
                else if (count % 2 == 0 && count != Beat / 2 && count != 0 && Beat > count)
                {
                    InstantiateAndStoreNote(MinutenessContanierNote4, UnActiveNote_4);
                }
                else if (count % 2 != 0)
                {
                    InstantiateAndStoreNote(MinutenessContanierNote8, UnActiveNote_8);
                }
                else if (count == Beat / 2)
                {
                    InstantiateAndStoreNote(MinutenessContanierNote2, UnActiveNote_2);
                }
               
            }
           
        }

        Distance = CNote[CNote.Count - 1].transform.position.x;

    }
    public void MakeMoreBar()
    {
        
        MakeBar();

        while (GameManager.Instance.musicInfo.Music.length+NextBeat >= TTime || End==false)
        {

            if (first == false)
            {

                if (count >= Beat)
                {
                    GameObject NNote = Instantiate(SpareNote, new Vector3(transform.position.x + (TTime + NextBeat) * speed, 0, 0), Quaternion.identity, BeatNote.transform);
                    CNote.Add(NNote);
                    TTime += NextBeat;
                    count = 1;
                }
                else if (count % 2 == 0 && count != Beat / 2 && count != 0 && Beat > count)
                {
                    InstantiateAndStoreNote(SpareNote, UnActiveNote_4);
                }
                else if (count % 2 != 0)
                {
                    InstantiateAndStoreNote(SpareNote, UnActiveNote_8);
                 
                    if (count == Beat)
                    {
                        End= true;
                        break;
                    }
                    
                }
                else if (count == Beat / 2)
                {
                    InstantiateAndStoreNote(SpareNote, UnActiveNote_2);
                }
              
            }

        }


        Distance = CNote[CNote.Count - 1].transform.position.x;

    }

    //특정 라인들을 생성할 때 추가로 처리해야 하는 작업들을 담고있는 메서드
    void InstantiateAndStoreNote(GameObject notePrefab, List<GameObject> unActiveList)
    {
        // 오브젝트 생성 및 리스트 추가
        GameObject NNote = Instantiate(notePrefab, new Vector3(transform.position.x + (TTime + NextBeat) * speed, 0, 0), Quaternion.identity, BeatNote.transform);
        CNote.Add(NNote);
        unActiveList?.Add(NNote);  // null 체크 후 리스트에 추가

        // TTime 갱신 및 count 증가
        TTime += NextBeat;
        count++;

        // 오브젝트 비활성화
        NNote.SetActive(false);
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
                        Note.SetActive(true); B_V_S = BeatNoteLine_Visble_Status.TwoNoteInBar;
                    }
                    break;
                case 1:
                    foreach (var Note in UnActiveNote_4)
                    {
                        Note.SetActive(true); B_V_S = BeatNoteLine_Visble_Status.FourNoteInBar;
                    }
                    break;
                case 2:
                    foreach (var Note in UnActiveNote_8)
                    {
                        Note.SetActive(true); B_V_S = BeatNoteLine_Visble_Status.EightNoteInBar;
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

            B_V_S = BeatNoteLine_Visble_Status.OneNoteInBar;

        }



    }

}