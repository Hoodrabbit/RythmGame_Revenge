using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


//여기에서 키를 입력했을 때 현재 마디 노트가 어느 형태까지 제작되었는지 확인하고 그 제작된만큼 이동시켜줘야함
//



public class SliderToNotePos : MonoBehaviour
{

    MusicTimeSlider musicTimeSlider;

    public float Pos = 0;
    public float MovePos = 0;
    float Value = 0;

    float PressTime = 0;
    float PressTime_2nd = 0;

    public int Count = 0;
    public int MaxCount = 0;

    void Start()
    {
        musicTimeSlider = FindObjectOfType<MusicTimeSlider>();
        MaxCount = (int)(GameManager.Instance.ClipLength() / GameManager.Instance.GetBPS());
    }

    // Update is called once per frame
    void Update()
    {
        SliderFunc();
    }

    

    public void SliderFunc()
    {
        float NowBeat = NowBeatCheck();



        Count = (int)(musicTimeSlider.GetSliderValue() / NowBeat);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(Mathf.Abs((musicTimeSlider.GetSliderValue() % NowBeat)));
        }


        if (Input.GetKey(KeyCode.D))
        {
            PressTime += Time.deltaTime;

            if (PressTime > 0.3f)
            {
                PressTime_2nd += Time.deltaTime;
                if (PressTime_2nd > 0.05f)
                {
                    if (Mathf.Abs((musicTimeSlider.GetSliderValue() % NowBeat)) <= 0.00001 || Mathf.Abs((musicTimeSlider.GetSliderValue() % NowBeat) - NowBeat) <= 0.00001)
                    {
                        musicTimeSlider.SetSliderValue(NowBeat);
                        //Debug.Log("잘되는 경우 : " + (slider.value / NowBeat));
                        Debug.Log("True");
                    }
                    else
                    {
                        Debug.Log("False");
                        Debug.Log("값 : " + (int)(musicTimeSlider.GetSliderValue() / NowBeat) + " , " + NowBeat);
                        //Debug.Log("안되는 경우 : " + Mathf.Abs((slider.value % NowBeat)));
                        //Debug.Log("안되는 경우 : " + (int)(slider.value / NowBeat) +" , " + (slider.value / NowBeat) + " , " + (slider.value % NowBeat) + " , " + NowBeat);
                        musicTimeSlider.SetSliderValue(-musicTimeSlider.GetSliderValue() + Count * NowBeat + NowBeat);

                    }
                    //if (MaxCount > Count)
                    //{

                    //    Count++;
                    //}
                        

                    PressTime_2nd = 0;
                }

            }




        }

        if (Input.GetKeyUp((KeyCode.D)))
        {
            if (PressTime < 0.3f)
            {
                if (Mathf.Abs((musicTimeSlider.GetSliderValue() % NowBeat)) <= 0.00001 || Mathf.Abs((musicTimeSlider.GetSliderValue() % NowBeat) - NowBeat) <= 0.00001)
                {
                    musicTimeSlider.SetSliderValue(NowBeat);
                    //Debug.Log("잘되는 경우 : " + (slider.value / NowBeat));
                    Debug.Log("True");
                }
                else
                {
                    Debug.Log("False");
                    Debug.Log("값 : " + (int)(musicTimeSlider.GetSliderValue() / NowBeat) + " , " + NowBeat);
                    //Debug.Log("안되는 경우 : " + Mathf.Abs((slider.value % NowBeat)));
                    //Debug.Log("안되는 경우 : " + (int)(slider.value / NowBeat) +" , " + (slider.value / NowBeat) + " , " + (slider.value % NowBeat) + " , " + NowBeat);
                    musicTimeSlider.SetSliderValue(-musicTimeSlider.GetSliderValue() + Count * NowBeat + NowBeat);

                }
                //if (MaxCount > Count)
                //    Count++;


            }

            PressTime = 0;

        }



        if (Input.GetKey(KeyCode.A))
        {
            PressTime += Time.deltaTime;

            if (PressTime > 0.3f)
            {
                PressTime_2nd += Time.deltaTime;
                if (PressTime_2nd > 0.05f)
                {
                    Debug.Log(Mathf.Abs((musicTimeSlider.GetSliderValue() % NowBeat)) + " , " + Mathf.Abs((musicTimeSlider.GetSliderValue() % NowBeat) - NowBeat));

                    //slider.value -= 60 / GameManager.Instance.musicInfo.BPM;
                    if (Mathf.Abs((musicTimeSlider.GetSliderValue() % NowBeat)) <= 0.00001 || Mathf.Abs((musicTimeSlider.GetSliderValue() % NowBeat) - NowBeat) <= 0.00001)
                    {
                        Debug.Log("TrueDown");
                        musicTimeSlider.SetSliderValue(-NowBeat);
                    }
                    else
                    {
                        Debug.Log("False");
                        Debug.Log("값 : " + (int)(musicTimeSlider.GetSliderValue() / NowBeat) + " , " + NowBeat);
                        //Debug.Log((slider.value % NowBeat) - NowBeat);
                        musicTimeSlider.SetSliderValue(-musicTimeSlider.GetSliderValue() + Count * NowBeat);
                        //정수형값이 0이 되는 순간 값을 제대로 못받아서 문제가 생기는 것 같음
                        //해결해줘야함

                    }
                    //if (Count > 0)
                    //    Count--;

                    PressTime_2nd = 0;
                }


                //MoveTime(Pos, MovePos, Value);

                //SetValue();

                //NowSongTime.text = GetSliderValue().ToString("0.000");
            }
        }

        if (Input.GetKeyUp((KeyCode.A)))
        {
            if (PressTime < 0.3f)
            {
                if (Mathf.Abs((musicTimeSlider.GetSliderValue() % NowBeat)) <= 0.00001 || Mathf.Abs((musicTimeSlider.GetSliderValue() % NowBeat) - NowBeat) <= 0.00001)
                {
                    Debug.Log("TrueDown");
                    musicTimeSlider.SetSliderValue(-NowBeat);
                }
                else
                {
                    Debug.Log("False");
                    Debug.Log("값 : " + (int)(musicTimeSlider.GetSliderValue() / NowBeat) + " , " + NowBeat);
                    //Debug.Log((slider.value % NowBeat) - NowBeat);
                    musicTimeSlider.SetSliderValue(-musicTimeSlider.GetSliderValue() + Count * NowBeat);
                    //정수형값이 0이 되는 순간 값을 제대로 못받아서 문제가 생기는 것 같음
                    //해결해줘야함

                }
                //if (Count > 0)
                //    Count--;

            }

            PressTime = 0;

        }


    }




    float NowBeatCheck()
    {
        switch (EditManager.Instance.barNote.B_V_S)
        {
            case BeatNoteLine_Visble_Status.OneNoteInBar:
                return GameManager.Instance.GetBPS();

            case BeatNoteLine_Visble_Status.TwoNoteInBar:
                return GameManager.Instance.GetBPS() / 2;

            case BeatNoteLine_Visble_Status.FourNoteInBar:
                return GameManager.Instance.GetBPS() / 4;

            case BeatNoteLine_Visble_Status.EightNoteInBar:
                return GameManager.Instance.GetBPS() / 8;

            
            default:
                return 0;
        }
    }





    public void MoveTime(float Pos, float MovePos, float Value)
    {

        if (Input.GetMouseButtonDown(0))
        {

            //Pos = slider.value;
            Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            Debug.Log(Pos);
            //Debug.Log(slider.value);
            //Value = slider.value;
            Debug.Log(musicTimeSlider.GetSliderValue());
        }

        if (Input.GetMouseButton(0))
        {
            MovePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            //Debug.Log(Value);
            musicTimeSlider.SetSliderValue(Value + MovePos - Pos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Pos = 0;
            MovePos = 0;
            Value = musicTimeSlider.GetSliderValue();
        }

    }
}
