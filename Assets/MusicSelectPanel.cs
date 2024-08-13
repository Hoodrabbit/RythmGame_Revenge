using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MusicSelectPanel : MonoBehaviour
{
    public GameObject MusicImage;
    public List<MusicSlot> MusicSlots = new List<MusicSlot>();
    public int musicCount;

    int NowSelect;
    int medianValue;
    Animator MusicSlotPanelAnimator;

    public List<MusicInfo> slotList_Sorting = new List<MusicInfo>();


    float pressTime = 0;
    public float HoldTime = 0.5f;

    bool KeyHold = false;

    Vector2 NextPos;


    private void Start()
    {
        musicCount = MusicManager.Instance.musicInfos.Count;

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


        SortingMusic(median);

        //foreach(var musicinfo in MusicManager.Instance.musicInfos.Count)
        //{

        //}
    }

    private void Update()
    {

        //�ߺ����� Ű �� ���� ���ϵ��� �̷��� ��
        if (Input.GetKey(KeyCode.A))
        {
            pressTime += Time.deltaTime;
            if (pressTime > HoldTime)
            {
                KeyHold = true;
                MusicSlotPanelAnimator.SetBool("Left", true);
            }
            else
            {
                // Debug.Log("�۵��ǿ�");
                //   MusicSlotPanelAnimator.SetBool("Left", true);

            }


        }
        else if (Input.GetKey(KeyCode.D))
        {
            pressTime += Time.deltaTime;
            if (pressTime > HoldTime)
            {
                KeyHold = true;
               MusicSlotPanelAnimator.SetBool("Right", true);
            }
            else
            {
                // MusicSlotPanelAnimator.SetBool("Right", true);
            }


        }


        if (Input.GetKeyUp(KeyCode.A))
        {
            if(KeyHold == false)
            {
               StartCoroutine(Decrease());
                
            }
            else
            {
                MusicSlotPanelAnimator.SetBool("Left", false);
               

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
                StartCoroutine(Increase());
            }
            else
            {
               MusicSlotPanelAnimator.SetBool("Right", false);


                if (KeyHold == true)
                {
                    KeyHold = false;
                }
            }
            pressTime = 0;




        }
       
    }




    //���� �뷡 ������ üũ�ؼ� ������ ���������� �뷡�� ��� ���� �ִ��� üũ�ؼ� �������ֱ�

    public void SortingMusic(int median)
    {




        int i = 0;

        //Debug.Log(median);

        while (i < musicCount)
        {
            if (median > musicCount - 1) //�ִ�
            {
                median = 0;
            }

            //   Debug.Log(i + "   �ٲ�� ��  " + median + "    " + slotList_Sorting[i]);
            MusicSlots[median++].SetMusic(slotList_Sorting[i++]);
            // Debug.Log(i + "   �ٲ� ��  " + median);
        }





        //123456
        //561234


        //�߾ӿ��� ���� ���� ���� �����͸� ��������
        //�ϴ� ������ �������� ��(���� ��) ������ 1�� �����͸� �߾����� ����� ��


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




    public void DecreaseValue()
    {
        NowSelect--;
        if (NowSelect < 0)
        {
            NowSelect = musicCount - 1;
        }

        SortingMusic(NowSelect);
        AudioManager.Instance.SetValue(MusicSlots[medianValue].musicInfo);
        Debug.Log(NowSelect);
    }

    public void IncreaseValue()
    {
        NowSelect++;
        if (NowSelect > musicCount - 1)
        {
            NowSelect = 0;
        }
        SortingMusic(NowSelect);
        AudioManager.Instance.SetValue(MusicSlots[medianValue].musicInfo);
        Debug.Log(NowSelect);
    }



}
