using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BarNote : MonoBehaviour
{
    public BeatNoteLine_Visble_Status B_V_S; // ���� �ð�ȭ �Ǿ��ִ� ��Ʈ ���ε��� ���¸� üũ�ϱ� ���� ����



    public GameObject BeatNote;
    public GameObject RhythmNote;

    [Header("���� ��Ʈ ����")]
    public GameObject ContanierNote;
    public GameObject NextNote;
    public GameObject MinutenessContanierNote2;
    public GameObject MinutenessContanierNote4;
    public GameObject MinutenessContanierNote8;

    public GameObject SpareNote; // ���� ��Ʈ ���ڰ� ������ �ʾ��� �� �׸�ŭ �߰��� �� ����������

    [Space(10f)]
    [Header("���� ������")]
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
    float NextBeat; //��Ʈ ��ŭ ������ ���ڳ�Ʈ �������
    int Hit_value = 0;
    int count = 0;//���� ���� �� üũ�� ��� ����

    int Beat = 8;



    bool Devide = false; //���� ������ ���� �뷡�� �������� üũ�Ͽ� ���� ���� ������ �߰��� �������� üũ���ִ� ����
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

    void InitializeBarNoteScript() //BarNote ��ũ��Ʈ �� ������ �ʱ�ȭ �����ִ� �޼���
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
            //Debug.Log("������");
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

    //Ư�� ���ε��� ������ �� �߰��� ó���ؾ� �ϴ� �۾����� ����ִ� �޼���
    void InstantiateAndStoreNote(GameObject notePrefab, List<GameObject> unActiveList)
    {
        // ������Ʈ ���� �� ����Ʈ �߰�
        GameObject NNote = Instantiate(notePrefab, new Vector3(transform.position.x + (TTime + NextBeat) * speed, 0, 0), Quaternion.identity, BeatNote.transform);
        CNote.Add(NNote);
        unActiveList?.Add(NNote);  // null üũ �� ����Ʈ�� �߰�

        // TTime ���� �� count ����
        TTime += NextBeat;
        count++;

        // ������Ʈ ��Ȱ��ȭ
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