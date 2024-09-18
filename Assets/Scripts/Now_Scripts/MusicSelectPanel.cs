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

    public List<MusicInfo> slotList_Sorting = new List<MusicInfo>();

    public static Action SlotClicked;

    public GameObject SelectDetailPanel;

    float pressTime = 0;
    public float HoldTime = 0.2f;

    public bool KeyHold = false;

    int NowSelect;
    int medianValue;
    Animator MusicSlotPanelAnimator;

    Vector2 NextPos;


    private void Start()
    {
        InitializingMusicSelectPanelScript();
    }

    void InitializingMusicSelectPanelScript()
    {
        musicCount = MusicManager.Instance.musicInfos.Count;
        SlotClicked += OpenDetailPanel;
        SlotClicked += SlotChangeCheck;
        MusicSlotPanelAnimator = GetComponent<Animator>();
        NextPos = new Vector2(600, 0);
        int median = musicCount / 2;
        medianValue = median;
        NowSelect = median;
        for (int i = 0; i < musicCount; i++)
        {
            MusicSlot MusicSlot_ = Instantiate(MusicImage, transform).GetComponent<MusicSlot>();
            RectTransform rectT = MusicSlot_.GetComponent<RectTransform>();

            int indexOffset = (i - (medianValue));

            rectT.anchoredPosition = new Vector2(600 * indexOffset, 0);
            MusicSlot_.SetMusic(MusicManager.Instance.musicInfos[i]);
            MusicSlots.Add(MusicSlot_);
        }


        foreach (var slot in MusicManager.Instance.musicInfos)
        {
            slotList_Sorting.Add(slot);
            //���� �����͸� �ϳ��� ��������
        }


        RectTransform centerRect = transform.GetChild(medianValue).GetComponent<RectTransform>();
        centerRect.anchoredPosition = Vector2.zero;

        if (GameManager.Instance.NowSelectValue != 0)
        {
            median = SetNowSelectValue();
        }
        SortingMusic(median);
    }



    private void OnDestroy()
    {
        SlotClicked -= OpenDetailPanel;
        SlotClicked -= SlotChangeCheck;
    }

    private void Update()
    {
        ChoosingSong();
    }


    void ChoosingSong()
    {
        //�ߺ����� Ű �� ���� ���ϵ��� �̷��� ��
        if (Input.GetKey(KeyCode.A))
        {
            pressTime += Time.deltaTime;
            if (pressTime > HoldTime)
            {

                KeyHold = true;

                SlotChangedLeft();

                pressTime -= HoldTime;
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
            }


        }


        if (Input.GetKeyUp(KeyCode.A))
        {
            if (KeyHold == false)
            {
                SlotChangedLeft();
            }
            else
            {
                if (KeyHold == true)
                {
                    KeyHold = false;
                }
            }
            pressTime = 0;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (KeyHold == false)
            {

                SlotChangedRight();
            }
            else
            {
                if (KeyHold == true)
                {
                    KeyHold = false;
                }
            }
            pressTime = 0;

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDetailPanel();
        }


    }

    //���� �뷡 ������ üũ�ؼ� ������ ���������� �뷡�� ��� ���� �ִ��� üũ�ؼ� �������ֱ�

    public void SortingMusic(int median)
    {
        int i = 0;
        while (i < musicCount)
        {
            if (median > musicCount - 1) //�ִ�
            {
                median = 0;
            }

            MusicSlots[median++].SetMusic(slotList_Sorting[i++]);
        }

    }


    void SlotChangeCheck()
    {
        Debug.Log("���� ���� ��");


        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);


        Debug.Log(Input.mousePosition - screenCenter);


        if ((Input.mousePosition - screenCenter).x < -100)
        {
            Debug.Log("Left�۵�");
            NowSelect++;
        }
        else if ((Input.mousePosition - screenCenter).x > 100)
        {
            Debug.Log("Right�۵�");
            NowSelect--;
        }
        SortingMusic(NowSelect);
        AudioManager.Instance.SetValue(MusicSlots[medianValue].musicInfo);
        MusicManager.Instance.SetMusic(MusicSlots[medianValue].musicInfo);
        Debug.Log(NowSelect);

        Debug.Log("���� ���� ��");
    }






    public void SlotChangedRight()
    {
        NowSelect--;
        if (NowSelect < 0)
        {
            NowSelect = musicCount - 1;
        }

        SortingMusic(NowSelect);
        AudioManager.Instance.SetValue(MusicSlots[medianValue].musicInfo);
        MusicManager.Instance.SetMusic(MusicSlots[medianValue].musicInfo);
        Debug.Log(NowSelect);



    }


    public void SlotChangedLeft()
    {

        NowSelect++;
        if (NowSelect > musicCount - 1)
        {
            NowSelect = 0;
        }
        SortingMusic(NowSelect);
        AudioManager.Instance.SetValue(MusicSlots[medianValue].musicInfo);
        MusicManager.Instance.SetMusic(MusicSlots[medianValue].musicInfo);
        Debug.Log(NowSelect);

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
        if (SelectDetailPanel.activeSelf == true)
        {
            SelectDetailPanel.SetActive(false);
        }
    }

}


//IEnumerator Decrease()
//{
//    //float poscheck = transform.position.x - NextPos.x;
//    //Debug.Log(poscheck);
//    //while(Mathf.Abs(poscheck - transform.position.x)>0.1f)
//    //{
//    //    transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - NextPos.x, transform.position.y), Time.deltaTime * 5f);
//    //}

//    //yield return new WaitForSeconds(0.5f);
//    //transform.position = new Vector2(transform.position.x - NextPos.x, transform.position.y);
//    MusicSlotPanelAnimator.SetBool("Left", true);
//    yield return new WaitUntil(() => MusicSlotPanelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f);
//    MusicSlotPanelAnimator.SetBool("Left", false);
//}

//IEnumerator Increase()
//{
//    //float poscheck = transform.position.x - NextPos.x;
//    //while (Mathf.Abs(poscheck - transform.position.x)> 0.1f)
//    //{

//    //    transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + NextPos.x, transform.position.y), Time.deltaTime * 5f);

//    //}        
//    //yield return new WaitForSeconds(0.5f);
//    //transform.position = new Vector2(transform.position.x + NextPos.x, transform.position.y);
//    MusicSlotPanelAnimator.SetBool("Right", true);
//    yield return new WaitUntil(() => MusicSlotPanelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f);
//    MusicSlotPanelAnimator.SetBool("Right", false);
//}