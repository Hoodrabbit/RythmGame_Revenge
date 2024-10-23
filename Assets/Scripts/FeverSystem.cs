using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverSystem : Singleton<FeverSystem>
{
    public int FeverGaugeValue = 0;
    public int FeverGaugeMaxValue = 120;

    public float FeverTimeValue = 5f;


    public GameObject FeverBackGround;


    bool IsFever = false;

    public Action FeverUPAction;




    /// <summary>
    /// 실제로 늘어나야 하는 값보다 더 늘도록 만들고 줄게 만들어줘야 함
    /// 그렇다면 ui 오브젝트 하나를 추가로 더 만들어줘야 함
    /// </summary>


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FeverGaugeValue == FeverGaugeMaxValue)
        {
            IsFever = true;
            //코루틴 작동
            StartCoroutine(ActivateFeverTime());

        }
    }

    public void GetFeverGauge(int FeverValue)
    {
        if(!IsFever)
        FeverGaugeValue += FeverValue;
        FeverUPAction?.Invoke();
    }

    public bool FeverCheck()
    {
        return IsFever;
    }

    IEnumerator ActivateFeverTime()
    {


        //피버 오브젝트 ON
        FeverBackGround.SetActive(true);
        //FeverBackGround.GetComponent<Animator>().SetTrigger(0);
        //지금 당장은 SetActive로 했는데 투명했다가 점점 선명해지게 만들어줘야 함


        //피버 값 서서히 감소 시키기
        float FeverValue = FeverGaugeValue;
        float TTime = 0;
        float MaxTime = 5f;


        while(TTime < MaxTime)
        {
            FeverGaugeValue = (int)Mathf.Lerp(FeverValue, 0, TTime/MaxTime);
            TTime += Time.deltaTime;
            yield return null;
        }
        FeverGaugeValue = 0;




        //yield return new WaitForSecondsRealtime(FeverTimeValue);

        //피버 오브젝트 OFF


        FeverBackGround.GetComponent<Animator>().SetTrigger(1);
        FeverBackGround.SetActive(false);
        IsFever = false;



    }





}
