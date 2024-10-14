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

    float RotateAngle;


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

        MakingMusicSlot();


        //for (int i = 0; i < musicCount; i++)
        //{
        //    MusicSlot MusicSlot_ = Instantiate(MusicImage, transform).GetComponent<MusicSlot>();
        //    RectTransform rectT = MusicSlot_.GetComponent<RectTransform>();

        //    int indexOffset = (i - (medianValue));

        //    rectT.anchoredPosition = new Vector2(600 * indexOffset, 0);
        //    MusicSlot_.SetMusic(MusicManager.Instance.musicInfos[i]);
        //    MusicSlots.Add(MusicSlot_);
        //}


        //foreach (var slot in MusicManager.Instance.musicInfos)
        //{
        //    slotList_Sorting.Add(slot);
        //    //���� �����͸� �ϳ��� ��������
        //}


        ////RectTransform centerRect = transform.GetChild(medianValue).GetComponent<RectTransform>();
        ////centerRect.anchoredPosition = Vector2.zero;

        //if (GameManager.Instance.NowSelectValue != 0)
        //{
        //    median = SetNowSelectValue();
        //}
        //SortingMusic(median);
    }



    private void OnDestroy()
    {
        SlotClicked -= OpenDetailPanel;
        SlotClicked -= SlotChangeCheck;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("������Ű ����");


            StartCoroutine(UpRotate());
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(DownRotate());


        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDetailPanel();
        }

        //ChoosingSong();
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


      //  Debug.Log(Input.mousePosition - screenCenter);


        if ((Input.mousePosition - screenCenter).x < -100)
        {
            Debug.Log("Left�۵�");
            NowSelect++;
            if (NowSelect > musicCount - 1)
            {
                NowSelect = 0;
            }
        }
        else if ((Input.mousePosition - screenCenter).x > 100)
        {
            //Debug.Log("Right�۵�");
            NowSelect--;
            if (NowSelect < 0)
            {
                NowSelect = musicCount - 1;
            }
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


    void MakingMusicSlot()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float centerX = rectTransform.anchoredPosition.x;
        float centerY = rectTransform.anchoredPosition.y;

        //Debug.Log("pos : " + centerX + ", " + centerY);
        RotateAngle = (360 / musicCount);

        float radius = rectTransform.rect.width * 3;
        for (int i = 0; i < musicCount; i++)
        {
            float angle = i * (360 / musicCount);
            float angleRadians = i * (2 * Mathf.PI / musicCount);
            float x = radius * Mathf.Cos(angleRadians);
            float y = radius * Mathf.Sin(angleRadians);

            MusicSlot MusicSlot_ = Instantiate(MusicImage, transform.position, Quaternion.Euler(0, 0, angle), transform).GetComponent<MusicSlot>();
            RectTransform childrect = MusicSlot_.GetComponent<RectTransform>();
            if (childrect != rectTransform)
            {
               // Debug.Log("anchoredpos : " + x + ", " + y);
                childrect.anchoredPosition = new Vector3(x, y, 0);
                //Debug.Log(childrect.anchoredPosition);
            }
            MusicSlot_.SetMusic(MusicManager.Instance.musicInfos[i]);
            MusicSlots.Add(MusicSlot_);

        }
        transform.GetChild(1).transform.SetAsLastSibling();
        transform.GetChild(0).transform.SetAsLastSibling();

    }

    IEnumerator DownRotate()
    {
        RectTransform rectt = GetComponent<RectTransform>();

        Vector3 currentRotation = rectt.rotation.eulerAngles;
        Vector3 targetRotation = new Vector3(0, 0, currentRotation.z - (RotateAngle));
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

        Debug.Log("�۵�");

        rectt.rotation = endRotation;

        transform.GetChild(1).transform.SetAsLastSibling();
        transform.GetChild(0).transform.SetAsLastSibling();
    }

    IEnumerator UpRotate()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;

        Debug.Log(currentRotation);

        Quaternion transformR = Quaternion.Euler(currentRotation);
        Quaternion ChangeR = Quaternion.Euler(0, 0, currentRotation.z + RotateAngle);


        float rotationDuration = 0.1f;
        float timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            float t = timeElapsed / rotationDuration;

            transform.rotation = Quaternion.Lerp(transformR, ChangeR, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("�۵�");



        Debug.Log("1 " + ChangeR.z);
        Debug.Log("2 " + Quaternion.Euler(0, 0, ChangeR.z));


        transform.rotation = ChangeR;

        transform.GetChild(musicCount - 2).transform.SetAsLastSibling();
    }

}

