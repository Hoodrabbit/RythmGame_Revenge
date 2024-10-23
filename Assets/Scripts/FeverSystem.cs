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
    /// ������ �þ�� �ϴ� ������ �� �õ��� ����� �ٰ� �������� ��
    /// �׷��ٸ� ui ������Ʈ �ϳ��� �߰��� �� �������� ��
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
            //�ڷ�ƾ �۵�
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


        //�ǹ� ������Ʈ ON
        FeverBackGround.SetActive(true);
        //FeverBackGround.GetComponent<Animator>().SetTrigger(0);
        //���� ������ SetActive�� �ߴµ� �����ߴٰ� ���� ���������� �������� ��


        //�ǹ� �� ������ ���� ��Ű��
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

        //�ǹ� ������Ʈ OFF


        FeverBackGround.GetComponent<Animator>().SetTrigger(1);
        FeverBackGround.SetActive(false);
        IsFever = false;



    }





}
