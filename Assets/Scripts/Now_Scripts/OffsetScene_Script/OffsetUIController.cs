using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class OffsetUIController : MonoBehaviour
{
    public TMP_Text Offset_Value_T;

    public OffsetJudgeLineController OffsetJudgeLine;
    public GameObject OffsetNote;

    public static int OffsetValue = 0;
    int MaxValue = 500;
    int MinValue = -500;


    public static float MaxOfsetJudgeLinePos = 5f;
    public static float MinOfsetJudgeLinePos = -5f;


    float PressTime = 0f;
    float PressTime_2nd = 0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OffsetJudgeLine.Click == true)
        {
            OffsetValue = OffsetJudgeLine.GetOffsetValue();
            Debug.Log(OffsetValue);

        }


        if(OffsetValue < MaxValue)
        {
            
            if (Input.GetKey(KeyCode.RightArrow))
            {
                PressTime += Time.deltaTime;
                if (PressTime > 0.3f)
                {
                    PressTime_2nd += Time.deltaTime;
                    if (PressTime_2nd >= 0.05f)
                    {
                        
                        OffsetValue += 10;
                        if(OffsetValue <= MaxValue)
                        {
                            OffsetJudgeLine.transform.position = new Vector3(OffsetJudgeLine.transform.position.x + 10/100f, 0);
                        }
                        else
                        {
                            OffsetJudgeLine.transform.position = new Vector3(OffsetJudgeLine.transform.position.x + (OffsetValue - MaxValue) / 100f, 0);
                            OffsetValue -= OffsetValue - MaxValue;
                        }
                       
                        PressTime_2nd = 0f;

                    }

                }
                else
                {
                    //OffsetValue += 1;
                    // PressTime = 0;
                }
            }

           

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                if (PressTime <= 0.3f)
                {
                    OffsetValue += 1;
                    OffsetJudgeLine.transform.position = new Vector3(OffsetJudgeLine.transform.position.x + 0.01f, 0);
                }

                PressTime = 0;
            }
        }

        if(OffsetValue > MinValue)
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

                        if (OffsetValue >= MinValue)
                        {
                            OffsetJudgeLine.transform.position = new Vector3(OffsetJudgeLine.transform.position.x - 10/100f, 0);
                        }
                        else
                        {
                            OffsetJudgeLine.transform.position = new Vector3(OffsetJudgeLine.transform.position.x - (OffsetValue - MinValue) / 100f, 0);
                            OffsetValue += OffsetValue - MinValue;
                        }
                        
                        PressTime_2nd = 0f;
                    }
                }
                else
                {
                    //OffsetValue -= 1;
                    //PressTime = 0;
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                if (PressTime <= 0.3f)
                {
                    OffsetValue -= 1;
                    OffsetJudgeLine.transform.position = new Vector3(OffsetJudgeLine.transform.position.x - 0.01f, 0);
                }
                PressTime = 0;
            }
        }
       
        Offset_Value_T.text = ((float)OffsetValue / 1000).ToString("F3");
    }

    public void SetOffset()
    {
        GameManager.Instance.SetOffset((float)OffsetValue / 1000);
    }


}


