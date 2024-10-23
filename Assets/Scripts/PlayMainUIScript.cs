using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayMainUIScript : Singleton<PlayMainUIScript>
{
    [Header("카운트다운 텍스트")]
    public TMP_Text CountdownText;



    [Header("체력")]
    public Image HpGaugeImg;
    public int StartHP;
    int NowHP;

    int HP_BeforeChange;

    [Header("피버")]
    public Image FeverGaugeImg;
    public int StartFever;
    int NowFever;

    int Fever_BeforeChange;



    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.ChangeHPAction += ChangingHPMethod;
        FeverSystem.Instance.FeverUPAction += ChangingFeverMethod;

        StartHP = PlayerController.Instance.hp;
        HP_BeforeChange = StartHP;
        StartFever = FeverSystem.Instance.FeverGaugeValue;
        Fever_BeforeChange = StartFever;
    }

    private void OnDisable()
    {
        PlayerController.Instance.ChangeHPAction -= ChangingHPMethod;
        FeverSystem.Instance.FeverUPAction -= ChangingFeverMethod;
    }


    // Update is called once per frame
    void Update()
    {
        NowHP = PlayerController.Instance.hp;
        //HpGaugeImg.fillAmount = (float)NowHP / StartHP;


        NowFever = FeverSystem.Instance.FeverGaugeValue;
        //FeverGaugeImg.fillAmount = (float)NowFever / FeverSystem.Instance.FeverGaugeMaxValue;
    }

    void ChangingHPMethod()
    {
        StopCoroutine(ChangingHpRoutine());
        StartCoroutine(ChangingHpRoutine());
    }

    void ChangingFeverMethod()
    {
        StopCoroutine(ChangingFeverRoutine());
        StartCoroutine(ChangingFeverRoutine());
    }


    IEnumerator ChangingHpRoutine()
    {
       
        float changingValue = 0;
        float elapsedTime = 0f;
        float duration = 1f; // 애니메이션 지속 시간 (초)

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            changingValue = Mathf.Lerp(HP_BeforeChange, NowHP, t);
            HpGaugeImg.fillAmount = changingValue / StartHP;
            yield return null;
        }
        HP_BeforeChange = NowHP;
        HpGaugeImg.fillAmount = (float)NowHP / StartHP;
    }

    IEnumerator ChangingFeverRoutine()
    {

        float changingValue = 0;
        float elapsedTime = 0f;
        float duration = 1f; // 애니메이션 지속 시간 (초)

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            changingValue = Mathf.Lerp(Fever_BeforeChange, NowFever, t);
            FeverGaugeImg.fillAmount = (float)changingValue / FeverSystem.Instance.FeverGaugeMaxValue;
            yield return null;
        }
        Fever_BeforeChange = NowFever;
        FeverGaugeImg.fillAmount = (float)NowFever / FeverSystem.Instance.FeverGaugeMaxValue;
    }


}
