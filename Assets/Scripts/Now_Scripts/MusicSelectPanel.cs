using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class MusicSelectPanel : MonoBehaviour
{
    public GameObject MusicImage;
    GameObject MusicImage_Selected;
    public List<MusicSlot> MusicSlots = new List<MusicSlot>();

    public int musicCount;

    public List<MusicInfo> slotList_Sorting = new List<MusicInfo>();

    public static Action SlotClicked;

    public GameObject SelectDetailPanel;

    [Header("화면에 출력시킬 횟수 6개 고정으로 제작해서 다른 수로는 변경하면 오류남")]
    public int visualCount;
    [Space(30f)]



    float RotateAngle;

    float pressTime = 0;
    public float HoldTime = 0.2f;

    public bool KeyHold = false;

    public int NowSelect;
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
        //int median = musicCount / 2;
        //medianValue = median;
        NowSelect = 0;

        MakingMusicSlot();
        if (GameManager.Instance.NowSelectValue != 0)
        {
            NowSelect = SetNowSelectValue();
            SortingMusic(NowSelect);
        }

        SelectingSlot();
        SortingMusic(NowSelect);

    }



    private void OnDestroy()
    {
        SlotClicked -= OpenDetailPanel;
        SlotClicked -= SlotChangeCheck;
    }

    private void Update()
    {
        UpdatingMusicSlot();
    }

    //현재 노래 슬롯을 체크해서 반으로 나눈다음에 노래가 몇곡 정도 있는지 체크해서 정렬해주기
    void UpdatingMusicSlot()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(UpRotate());
            ChangingSlot_UP();
            if (NowSelect + 1 == musicCount)
            {
                NowSelect = 0;
            }
            else
            {
                NowSelect++;
            }
            SortingMusic(NowSelect);

          
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
           {
            StartCoroutine(DownRotate());
            ChangingSlot_Down();
            if (NowSelect == 0)
            {
                NowSelect = musicCount - 1;
            }
            else
            {
                NowSelect--;
            }
            SortingMusic(NowSelect);
           
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OpenDetailPanel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDetailPanel();
        }
    }



    void ChangingSlot_UP()
    {
        List<MusicSlot> slots = new List<MusicSlot>();

        for(int i=0; i<MusicSlots.Count; i++)
        {
            if(i>0)
            {
                slots.Add(MusicSlots[i]);
            }
        }
        slots.Add(MusicSlots[0]);

        MusicSlots = slots;
    }
    
    void ChangingSlot_Down()
    {
        List<MusicSlot> slots = new List<MusicSlot>();
        
        slots.Add(MusicSlots[MusicSlots.Count-1]);

        for (int i = 0; i < MusicSlots.Count-1; i++)
        {

           slots.Add(MusicSlots[i]);
        }
        MusicSlots = slots;
    }

    public void SortingMusic(int selectValue)
    {
        int changevalue = selectValue;



        for (int i = 0; i < 6; i++)
        {
           // Debug.Log("실행됨");
            if (i < 4)
            {
                    if (changevalue == musicCount)
                    {
                        changevalue = 0;
                        MusicSlots[i].SetMusic(MusicManager.Instance.musicInfos[changevalue++]);
                    }
                    else
                    {
                        MusicSlots[i].SetMusic(MusicManager.Instance.musicInfos[changevalue++]);
                    }
               
            }
            else if (i == 4)
            {
                int check = selectValue;


                if (check == 0)
                {
                    check = musicCount - 2;
                    MusicSlots[i].SetMusic(MusicManager.Instance.musicInfos[check]);
                }
                else if (check == 1)
                {
                    check = musicCount - 1;
                    MusicSlots[i].SetMusic(MusicManager.Instance.musicInfos[check]);
                }
                else
                {
                    MusicSlots[i].SetMusic(MusicManager.Instance.musicInfos[check-1]);
                }
            }
            else
            {
                int check = selectValue;
                if (check == 0)
                {
                    check = musicCount - 1;
                    MusicSlots[i].SetMusic(MusicManager.Instance.musicInfos[check]);
                }
                else
                {
                    MusicSlots[i].SetMusic(MusicManager.Instance.musicInfos[check - 1]);
                }

            }
        }

        SlotChangeCheck();
    }
    void SlotChangeCheck()
    {
       // Debug.Log("check");

        GameManager.Instance.NowSelectValue = NowSelect;
        AudioManager.Instance.SetValue(MusicSlots[0].musicInfo);
        MusicManager.Instance.SetMusic(MusicSlots[0].musicInfo);
        //SelectingSlot();
    }

    void SelectingSlot()
    {
        MusicImage_Selected.SetActive(true);
        MusicSlot SelectSlot = MusicImage_Selected.GetComponent<MusicSlot>();
        SelectSlot.SetMusic(MusicSlots[0].musicInfo);

        if (SelectSlot.slotAnimator.enabled == false)
        {
            SelectSlot.slotAnimator.enabled = true;
        }
        
        SelectSlot.slotAnimator.Play("MusicSlotChoosed");

        MusicSlots[0].gameObject.SetActive(false);
        

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


    void MakingMusicSlot()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float centerX = rectTransform.anchoredPosition.x;
        float centerY = rectTransform.anchoredPosition.y - 30;


        RotateAngle = (360 / 6);



        float radius = rectTransform.rect.width;
        for (int i = 0; i < 6; i++)
        {

            int angle = i * (360 / 6);
            float angleRadians = i * (2 * Mathf.PI / /*musicCount*/6);
            float x = radius * Mathf.Cos(angleRadians);
            float y = radius * Mathf.Sin(angleRadians);

            MusicSlot MusicSlot_ = Instantiate(MusicImage, transform.position, Quaternion.Euler(0, 0, angle), transform).GetComponent<MusicSlot>();
            RectTransform childrect = MusicSlot_.GetComponent<RectTransform>();
            if (childrect != rectTransform)
            {
                childrect.anchoredPosition = new Vector3(x, y, 0);
            }


            //기본 곡 셋팅단계 1234는 원래 순서대로의 곡들 삽입 56은 뒤에서 두곡으로 함
            if (i < 4)
            {
                MusicSlot_.SetMusic(MusicManager.Instance.musicInfos[i]);
            }
            else if (i == 4)
            {
                MusicSlot_.SetMusic(MusicManager.Instance.musicInfos[musicCount - 2]);
            }
            else
            {
                MusicSlot_.SetMusic(MusicManager.Instance.musicInfos[musicCount - 1]);
            }
            MusicSlots.Add(MusicSlot_);

        }

        MusicImage_Selected=Instantiate(MusicImage, Vector2.zero, Quaternion.Euler(0, 0, 0), transform.parent);
        MusicImage_Selected.GetComponent<RectTransform>().anchoredPosition = new Vector2(-660, 0);
        MusicImage_Selected.SetActive(false);
    }

    IEnumerator DownRotate()
    {

        if (MusicSlots[0].gameObject.activeSelf == false)
        {
            MusicSlots[0].gameObject.SetActive(true);
            MusicImage_Selected.SetActive(false);
        }



        RectTransform rectt = GetComponent<RectTransform>();

        Vector3 currentRotation = rectt.rotation.eulerAngles;
        Vector3 targetRotation = new Vector3(0, 0, (int)currentRotation.z + (int)(RotateAngle));
        Debug.Log(RotateAngle);

        float rotationDuration = 0.1f;
        float timeElapsed = 0f;


        Quaternion startRotation = Quaternion.Euler(currentRotation);
        Quaternion endRotation = Quaternion.Euler(targetRotation);


        while (timeElapsed < rotationDuration)
        {
            float t = timeElapsed / rotationDuration;

            rectt.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }


        rectt.rotation = endRotation;

        SelectingSlot();


    }

    IEnumerator UpRotate()
    {
        if (MusicSlots[0].gameObject.activeSelf == false)
        {
            MusicSlots[0].gameObject.SetActive(true);
            MusicImage_Selected.SetActive(false);
        }


        Vector3 currentRotation = transform.rotation.eulerAngles;

        Debug.Log(currentRotation);

        Quaternion transformR = Quaternion.Euler(currentRotation);
        Quaternion ChangeR = Quaternion.Euler(0, 0, (int)currentRotation.z - (int)RotateAngle);


        float rotationDuration = 0.1f;
        float timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            float t = timeElapsed / rotationDuration;

            transform.rotation = Quaternion.Lerp(transformR, ChangeR, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = ChangeR;
        SelectingSlot();

    }

}