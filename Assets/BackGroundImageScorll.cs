using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackGroundImageScorll : MonoBehaviour
{
    [Header("배경 이미지")]
    public GameObject BackGround;
    public GameObject BackGround2;
    public float Width_Back;
    //메인 뒷배경은 안움직이나??



    [Space(10f)]
    public GameObject BackGround_Infront;
    RectTransform Background_infrontRect;


    public GameObject BackGround_Infront2;
    RectTransform Background_infrontRect2;

    public float Width_Infront;
    
    [Space(10f)]
    public GameObject BackGround_Platform;
    RectTransform BackGround_PlatformRect;

    public GameObject BackGround_Platform2;
    RectTransform BackGround_PlatformRect2;

    public float Width_Platform;

    [Space(10f)]
    public GameObject Infront_Platform;
    RectTransform Infront_PlatformRect;

    public GameObject Infront_Platform2;
    RectTransform Infront_PlatformRect2;

    public float Width_Infront_Platform;

   

    [Space(10f)]
    [Header("피버 이미지")]

    public GameObject Fever_BackGround;
    public GameObject Fever_BackGround2;

    public GameObject Fever_Infront;
    public GameObject Fever_Infront2;

    float BackSpeed = 5;
    float InfrontSpeed = 400;
    float PlatformSpeed = 700;
    float Infront_PlatformSpeed = 600;

    float scrollOffset = 15;



    void InitializingRect()
    {
        Background_infrontRect = BackGround_Infront.GetComponent<RectTransform>();
        Background_infrontRect2 = BackGround_Infront2.GetComponent<RectTransform>();
    
        BackGround_PlatformRect = BackGround_Platform.GetComponent<RectTransform>();
        BackGround_PlatformRect2 = BackGround_Platform2.GetComponent<RectTransform>();


        Infront_PlatformRect = Infront_Platform.GetComponent<RectTransform>();
        Infront_PlatformRect2 = Infront_Platform2.GetComponent<RectTransform>();

    }




    private void Start()
    {

        //여기나 다른 곳에 이미지 스프라이트 심어주는 관련 기능 추가 예정

        InitializingRect();


        Width_Back = BackGround.GetComponent<RectTransform>().rect.width;
        Width_Infront = Background_infrontRect.rect.width;
        Width_Platform = BackGround_PlatformRect.rect.width;
        Width_Infront_Platform = Infront_PlatformRect.rect.width;


        BackGround2.transform.position = new Vector2(BackGround2.transform.position.x+ Width_Back,0);
        Background_infrontRect2.anchoredPosition = new Vector2(Background_infrontRect.anchoredPosition.x+ Width_Infront, 0);
        BackGround_PlatformRect2.anchoredPosition = new Vector2(BackGround_PlatformRect.anchoredPosition.x + Width_Platform, 0);
        Infront_PlatformRect2.anchoredPosition = new Vector2(Infront_PlatformRect.anchoredPosition.x + Width_Infront_Platform, 0);



    }

    private void FixedUpdate()
    {
        //UI로 맵스크롤 하는게 맞나?

        MoveInfrontBackground(BackGround_Infront);
        MoveInfrontBackground(BackGround_Infront2);

        MovePlatform(BackGround_Platform);
        MovePlatform(BackGround_Platform2);

        MoveInfrontPlatform(Infront_Platform);
        MoveInfrontPlatform(Infront_Platform2);




    }





    //약간의 딜레이가 있어서 오프셋 정도로 10 더 빼줌


    void MoveBackground(GameObject back)
    {
        RectTransform backRect = back.GetComponent<RectTransform>();
        if(backRect.anchoredPosition.x > -Width_Back)
        {
            //아직 수정 안됨 배경이 스크롤 되는지 안되는지 판단 못함
            backRect.anchoredPosition = new Vector2(backRect.anchoredPosition.x - Width_Back * Time.deltaTime, 0);
        }
        
    }    

    void MoveInfrontBackground(GameObject InfrontBack)
    {
        RectTransform InfrontBackRect = InfrontBack.GetComponent<RectTransform>();

        if (InfrontBackRect.anchoredPosition.x > -Width_Infront)
        {
            InfrontBackRect.anchoredPosition = new Vector2(InfrontBackRect.anchoredPosition.x - InfrontSpeed * Time.deltaTime, 0);
        }
        else
        {
            if(InfrontBack == BackGround_Infront)
            {
                InfrontBackRect.anchoredPosition = new Vector2(Background_infrontRect2.anchoredPosition.x + Width_Infront - scrollOffset, 0);
            }
            else
            {
                InfrontBackRect.anchoredPosition = new Vector2(Background_infrontRect.anchoredPosition.x + Width_Infront - scrollOffset, 0);
            }
        }
    }

    void MovePlatform(GameObject Platform)
    {
        RectTransform PlatformRect = Platform.GetComponent<RectTransform>();

        if (PlatformRect.anchoredPosition.x > -Width_Platform)
        {
            PlatformRect.anchoredPosition = new Vector2(PlatformRect.anchoredPosition.x - PlatformSpeed * Time.deltaTime, 0);
        }
        else
        {
            if(Platform == BackGround_Platform)
            {
                PlatformRect.anchoredPosition = new Vector2(BackGround_PlatformRect2.anchoredPosition.x + Width_Platform - scrollOffset, 0);
            }
            else
            {
                PlatformRect.anchoredPosition = new Vector2(BackGround_PlatformRect.anchoredPosition.x + Width_Platform - scrollOffset, 0);
            }
        }
    }

    void MoveInfrontPlatform(GameObject InfrontPlatform)
    {
        RectTransform InfrontPlatformRect = InfrontPlatform.GetComponent<RectTransform>();
        if (InfrontPlatformRect.anchoredPosition.x > -Width_Infront_Platform)
        {
            InfrontPlatformRect.anchoredPosition = new Vector2(InfrontPlatformRect.anchoredPosition.x - Infront_PlatformSpeed * Time.deltaTime, 0);
        }
        else
        {
            if(InfrontPlatform == Infront_Platform)
            {
                InfrontPlatformRect.anchoredPosition = new Vector2(Infront_PlatformRect2.anchoredPosition.x + Width_Infront_Platform - scrollOffset, 0);
            }
            else
            {
                InfrontPlatformRect.anchoredPosition = new Vector2(Infront_PlatformRect.anchoredPosition.x + Width_Infront_Platform - scrollOffset, 0);
            }
        }
    }




}
