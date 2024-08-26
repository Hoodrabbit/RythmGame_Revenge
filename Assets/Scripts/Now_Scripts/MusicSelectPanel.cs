using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class MusicSelectPanel : MonoBehaviour
{
    public GameObject MusicImage;
    public List<MusicSlot> MusicSlots = new List<MusicSlot>();
    public int musicCount;

    int NowSelect;
    int medianValue;
    Animator MusicSlotPanelAnimator;

    public List<MusicInfo> slotList_Sorting = new List<MusicInfo>();

    public static Action SlotClicked;

    //public GameObject Left;
    //public GameObject Right;

    public GameObject SelectDetailPanel;



    float pressTime = 0;
    public float HoldTime = 0.2f;

    public bool KeyHold = false;

    Vector2 NextPos;


    


    private void Start()
    {
        //SelectDetailPanel = GameObject.Find("SelectDetailPanel");

        musicCount = MusicManager.Instance.musicInfos.Count;
        SlotClicked += OpenDetailPanel;
        MusicSlotPanelAnimator = GetComponent<Animator>();
        NextPos = new Vector2(600, 0);
        int median = musicCount / 2;
        medianValue = median;
        NowSelect = median;
        for (int i = 0; i < musicCount; i++)
        {
            MusicSlot MusicSlot_ = Instantiate(MusicImage, transform).GetComponent<MusicSlot>();

            if (i == medianValue - 1 || i == medianValue || i == medianValue + 1)
            {
               
            }


            RectTransform rectT = MusicSlot_.GetComponent<RectTransform>();

            int indexOffset = (i - (medianValue));

            rectT.anchoredPosition = new Vector2(600 * indexOffset, 0);
            MusicSlot_.SetMusic(MusicManager.Instance.musicInfos[i]);
            MusicSlots.Add(MusicSlot_);
        }


        foreach (var slot in MusicManager.Instance.musicInfos)
        {
            slotList_Sorting.Add(slot);
            //슬롯 데이터를 하나씩 저장해줌
        }


        RectTransform centerRect = transform.GetChild(medianValue).GetComponent<RectTransform>();
        centerRect.anchoredPosition = Vector2.zero;

        if(GameManager.Instance.NowSelectValue != 0)
        {
            median = SetNowSelectValue();
        }
        SortingMusic(median);

        
        //foreach(var musicinfo in MusicManager.Instance.musicInfos.Count)
        //{

        //}
    }

    private void OnDestroy()
    {
        SlotClicked -= OpenDetailPanel;
    }

    private void Update()
    {

        //중복으로 키 값 받지 못하도록 이렇게 함
        if (Input.GetKey(KeyCode.A))
        {
            pressTime += Time.deltaTime;
            if (pressTime > HoldTime)
            {

                KeyHold = true;
     
                        SlotChangedLeft();
                

                //MusicSlotPanelAnimator.SetBool("Left", true);
               // SlotChangedLeft();
                pressTime -=HoldTime;
            }
            else
            {
                // Debug.Log("작동되요");
                //   MusicSlotPanelAnimator.SetBool("Left", true);

            }


        }
        else if (Input.GetKey(KeyCode.D))
        {
            pressTime += Time.deltaTime;
            if (pressTime > HoldTime)
            {
                KeyHold = true;

                        SlotChangedRight();
                pressTime -= HoldTime;

                // MusicSlotPanelAnimator.SetBool("Right", true);
                // SlotChangedRight();
            }
            else
            {
                // MusicSlotPanelAnimator.SetBool("Right", true);
            }


        }


        if (Input.GetKeyUp(KeyCode.A))
        {
            if (KeyHold == false)
            {


                SlotChangedLeft();
                
                // SlotChangedLeft();
            }
            else
            {
             //   MusicSlotPanelAnimator.SetBool("Left", false);


                if (KeyHold == true)
                {
                    KeyHold = false;
                }
            }
            pressTime = 0;

          //if (MusicSlotPanelAnimator.GetBool("Left"))
          //  {
          //      StopAllCoroutines();
          //  }

        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (KeyHold == false)
            {

                       SlotChangedRight();
                
                // StartCoroutine(Increase());
                //SlotChangedRight();
            }
            else
            {
             //   MusicSlotPanelAnimator.SetBool("Right", false);


                if (KeyHold == true)
                {
                    KeyHold = false;
                }
            }
            pressTime = 0;

            //if(MusicSlotPanelAnimator.GetBool("Right"))
            //{
            //    StopAllCoroutines();
            //}


        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDetailPanel();
        }



    }

   


    //현재 노래 슬롯을 체크해서 반으로 나눈다음에 노래가 몇곡 정도 있는지 체크해서 정렬해주기

    public void SortingMusic(int median)
    {




        int i = 0;

        //Debug.Log(median);

        while (i < musicCount)
        {
            if (median > musicCount - 1) //최대
            {
                median = 0;
            }

            //   Debug.Log(i + "   바뀌기 전  " + median + "    " + slotList_Sorting[i]);
            MusicSlots[median++].SetMusic(slotList_Sorting[i++]);
            // Debug.Log(i + "   바뀐 후  " + median);
        }





        //123456
        //561234


        //중앙에서 부터 차례 차례 데이터를 변경해줌
        //일단 게임을 시작했을 때(켰을 때) 무조건 1의 데이터를 중앙으로 해줘야 함


    }


    IEnumerator Decrease()
    {
        //float poscheck = transform.position.x - NextPos.x;
        //Debug.Log(poscheck);
        //while(Mathf.Abs(poscheck - transform.position.x)>0.1f)
        //{
        //    transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - NextPos.x, transform.position.y), Time.deltaTime * 5f);
        //}

        //yield return new WaitForSeconds(0.5f);
        //transform.position = new Vector2(transform.position.x - NextPos.x, transform.position.y);
        MusicSlotPanelAnimator.SetBool("Left", true);
        yield return new WaitUntil(() => MusicSlotPanelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f);
        MusicSlotPanelAnimator.SetBool("Left", false);
    }

    IEnumerator Increase()
    {
        //float poscheck = transform.position.x - NextPos.x;
        //while (Mathf.Abs(poscheck - transform.position.x)> 0.1f)
        //{

        //    transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + NextPos.x, transform.position.y), Time.deltaTime * 5f);

        //}        
        //yield return new WaitForSeconds(0.5f);
        //transform.position = new Vector2(transform.position.x + NextPos.x, transform.position.y);
        MusicSlotPanelAnimator.SetBool("Right", true);
        yield return new WaitUntil(() => MusicSlotPanelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f);
        MusicSlotPanelAnimator.SetBool("Right", false);
    }




    public void SlotChangedRight()
    {
        //MusicSlotPanelAnimator.SetTrigger("Changed");
        //if (MusicSlotPanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("MusicSlotChange") == true)
        //{
        //    Debug.Log("check");
        //    if (MusicSlotPanelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >0f)
        //    {
                NowSelect--;
                if (NowSelect < 0)
                {
                    NowSelect = musicCount - 1;
                }

                SortingMusic(NowSelect);
                AudioManager.Instance.SetValue(MusicSlots[medianValue].musicInfo);
                MusicManager.Instance.SetMusic(MusicSlots[medianValue].musicInfo);
                Debug.Log(NowSelect);



        //    }
        //}




       
    }

    public void SlotChangedLeft()
    {
        //MusicSlotPanelAnimator.SetTrigger("Changed");
        //if (MusicSlotPanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("MusicSlotChange") == true)
        //{
        //    Debug.Log("check");
        //    if (MusicSlotPanelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >0f)
        //    {
        NowSelect++;
        if (NowSelect > musicCount - 1)
        {
            NowSelect = 0;
        }
        SortingMusic(NowSelect);
                AudioManager.Instance.SetValue(MusicSlots[medianValue].musicInfo);
                Debug.Log(NowSelect);



        //    }
        //}




       
    }

    public int GetNowSelect()
    {
        return NowSelect;
    }

    public int SetNowSelectValue()
    {
        NowSelect = GameManager.Instance.NowSelectValue;
        return GameManager.Instance.NowSelectValue;
    }


    void OpenDetailPanel()
    {
        GameManager.Instance.NowSelectValue = NowSelect;
        SelectDetailPanel.SetActive(true);
    }

    void CloseDetailPanel()
    {
        if(SelectDetailPanel.activeSelf == true)
        {
            SelectDetailPanel.SetActive(false);
        }
    }

}
