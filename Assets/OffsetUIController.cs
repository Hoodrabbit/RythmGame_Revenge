using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class OffsetUIController : MonoBehaviour
{
    public TMP_Text Offset_Value_T;

    public GameObject OffsetJudgeLine;


    public static int OffsetValue = 0;
    int MaxValue = 500;
    int MinValue = -500;



    float PressTime = 0f;
    float PressTime_2nd = 0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            PressTime += Time.deltaTime;
            if (PressTime > 0.3f)
            {
                PressTime_2nd += Time.deltaTime;
                if (PressTime_2nd >= 0.05f)
                {
                    OffsetValue -= 10;
                    PressTime_2nd = 0f;
                }
            }
            else
            {
                //OffsetValue -= 1;
                //PressTime = 0;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            PressTime += Time.deltaTime;
            if (PressTime > 0.3f)
            {
                PressTime_2nd += Time.deltaTime;
                if (PressTime_2nd >= 0.05f)
                {
                    OffsetValue += 10;
                    PressTime_2nd = 0f;
                }

            }
            else
            {
                //OffsetValue += 1;
                // PressTime = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (PressTime <= 0.3f)
            {
                OffsetValue -= 1;

            }
            PressTime = 0;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (PressTime <= 0.3f)
            {
                OffsetValue += 1;

            }

            PressTime = 0;
        }


      if(OffsetValue > MaxValue)
        {
            OffsetValue = MaxValue;
        }
      else if (OffsetValue < MinValue) 
        {
            OffsetValue = MinValue;
        }



        OffsetJudgeLine.transform.position = new Vector3((float)OffsetValue/1000 * GameManager.Instance.speed, OffsetJudgeLine.transform.position.y);
        Offset_Value_T.text = ((float)OffsetValue / 1000).ToString("F3");
    }
}


/*
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
                        //Debug.Log("�ߵǴ� ��� : " + (slider.value / Beat));
                        Debug.Log("True");
                    }
                    else
                    {
                        Debug.Log("False");
                        Debug.Log("�� : " + (int)(musicTimeSlider.GetSliderValue() / Beat) + " , " + Beat);
                        //Debug.Log("�ȵǴ� ��� : " + Mathf.Abs((slider.value % Beat)));
                        //Debug.Log("�ȵǴ� ��� : " + (int)(slider.value / Beat) +" , " + (slider.value / Beat) + " , " + (slider.value % Beat) + " , " + Beat);
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
                    //Debug.Log("�ߵǴ� ��� : " + (slider.value / Beat));
                    Debug.Log("True");
                }
                else
                {
                    Debug.Log("False");
                    Debug.Log("�� : " + (int)(musicTimeSlider.GetSliderValue() / Beat) + " , " + Beat);
                    //Debug.Log("�ȵǴ� ��� : " + Mathf.Abs((slider.value % Beat)));
                    //Debug.Log("�ȵǴ� ��� : " + (int)(slider.value / Beat) +" , " + (slider.value / Beat) + " , " + (slider.value % Beat) + " , " + Beat);
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
                        Debug.Log("�� : " + (int)(musicTimeSlider.GetSliderValue() / Beat) + " , " + Beat);
                        //Debug.Log((slider.value % Beat) - Beat);
                        musicTimeSlider.SetSliderValue(-musicTimeSlider.GetSliderValue() + Count * Beat);
                        //���������� 0�� �Ǵ� ���� ���� ����� ���޾Ƽ� ������ ����� �� ����
                        //�ذ��������

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
                    Debug.Log("�� : " + (int)(musicTimeSlider.GetSliderValue() / Beat) + " , " + Beat);
                    //Debug.Log((slider.value % Beat) - Beat);
                    musicTimeSlider.SetSliderValue(-musicTimeSlider.GetSliderValue() + Count * Beat);
                    //���������� 0�� �Ǵ� ���� ���� ����� ���޾Ƽ� ������ ����� �� ����
                    //�ذ��������

                }
                //if (Count > 0)
                //    Count--;

            }

            PressTime = 0;

        }
 
 
 */
