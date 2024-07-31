using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
        ////m_Cam = Camera.main.GetComponent<Transform>();
        ////slider = GetComponent<Slider>();
        //slider.maxValue = GameManager.Instance.musicInfo.Music.length;
        //slider.onValueChanged.AddListener(delegate { SetMusicTime(); });

        ////NowSongTime = GetComponentInChildren<TextMeshProUGUI>();
        MaxCount = (int)(GameManager.Instance.ClipLength() / GameManager.Instance.GetBPS());
    }

    // Update is called once per frame
    void Update()
    {
        SliderFunc();
    }

   

    public void SliderFunc()
    {
        float Beat = GameManager.Instance.GetBPS();
        Count = (int)(musicTimeSlider.GetSliderValue() / Beat);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(Mathf.Abs((musicTimeSlider.GetSliderValue() % Beat)));
        }


        if (Input.GetKey(KeyCode.D))
        {
            PressTime += Time.deltaTime;

            if (PressTime > 0.3f)
            {
                PressTime_2nd += Time.deltaTime;
                if (PressTime_2nd > 0.05f)
                {
                    if (Mathf.Abs((musicTimeSlider.GetSliderValue() % Beat)) <= 0.00001 || Mathf.Abs((musicTimeSlider.GetSliderValue() % Beat) - Beat) <= 0.00001)
                    {
                        musicTimeSlider.SetSliderValue(Beat);
                        //Debug.Log("잘되는 경우 : " + (slider.value / Beat));
                        Debug.Log("True");
                    }
                    else
                    {
                        Debug.Log("False");
                        Debug.Log("값 : " + (int)(musicTimeSlider.GetSliderValue() / Beat) + " , " + Beat);
                        //Debug.Log("안되는 경우 : " + Mathf.Abs((slider.value % Beat)));
                        //Debug.Log("안되는 경우 : " + (int)(slider.value / Beat) +" , " + (slider.value / Beat) + " , " + (slider.value % Beat) + " , " + Beat);
                        musicTimeSlider.SetSliderValue(-musicTimeSlider.GetSliderValue() + Count * Beat + Beat);

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
                if (Mathf.Abs((musicTimeSlider.GetSliderValue() % Beat)) <= 0.00001 || Mathf.Abs((musicTimeSlider.GetSliderValue() % Beat) - Beat) <= 0.00001)
                {
                    musicTimeSlider.SetSliderValue(Beat);
                    //Debug.Log("잘되는 경우 : " + (slider.value / Beat));
                    Debug.Log("True");
                }
                else
                {
                    Debug.Log("False");
                    Debug.Log("값 : " + (int)(musicTimeSlider.GetSliderValue() / Beat) + " , " + Beat);
                    //Debug.Log("안되는 경우 : " + Mathf.Abs((slider.value % Beat)));
                    //Debug.Log("안되는 경우 : " + (int)(slider.value / Beat) +" , " + (slider.value / Beat) + " , " + (slider.value % Beat) + " , " + Beat);
                    musicTimeSlider.SetSliderValue(-musicTimeSlider.GetSliderValue() + Count * Beat + Beat);

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
                    Debug.Log(Mathf.Abs((musicTimeSlider.GetSliderValue() % Beat)) + " , " + Mathf.Abs((musicTimeSlider.GetSliderValue() % Beat) - Beat));

                    //slider.value -= 60 / GameManager.Instance.musicInfo.BPM;
                    if (Mathf.Abs((musicTimeSlider.GetSliderValue() % Beat)) <= 0.00001 || Mathf.Abs((musicTimeSlider.GetSliderValue() % Beat) - Beat) <= 0.00001)
                    {
                        Debug.Log("TrueDown");
                        musicTimeSlider.SetSliderValue(-Beat);
                    }
                    else
                    {
                        Debug.Log("False");
                        Debug.Log("값 : " + (int)(musicTimeSlider.GetSliderValue() / Beat) + " , " + Beat);
                        //Debug.Log((slider.value % Beat) - Beat);
                        musicTimeSlider.SetSliderValue(-musicTimeSlider.GetSliderValue() + Count * Beat);
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
                if (Mathf.Abs((musicTimeSlider.GetSliderValue() % Beat)) <= 0.00001 || Mathf.Abs((musicTimeSlider.GetSliderValue() % Beat) - Beat) <= 0.00001)
                {
                    Debug.Log("TrueDown");
                    musicTimeSlider.SetSliderValue(-Beat);
                }
                else
                {
                    Debug.Log("False");
                    Debug.Log("값 : " + (int)(musicTimeSlider.GetSliderValue() / Beat) + " , " + Beat);
                    //Debug.Log((slider.value % Beat) - Beat);
                    musicTimeSlider.SetSliderValue(-musicTimeSlider.GetSliderValue() + Count * Beat);
                    //정수형값이 0이 되는 순간 값을 제대로 못받아서 문제가 생기는 것 같음
                    //해결해줘야함

                }
                //if (Count > 0)
                //    Count--;

            }

            PressTime = 0;

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
