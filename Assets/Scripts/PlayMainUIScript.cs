using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMainUIScript : MonoBehaviour
{

    [Header("ü��")]
    public Image HpGaugeImg;
    public int StartHP;
    int NowHP;
    

    [Header("�ǹ�")]
    public Image FeverGaugeImg;
    public int StartFever;
    int NowFever;
    




    // Start is called before the first frame update
    void Start()
    {
        StartHP = PlayerController.Instance.hp;

        StartFever = FeverSystem.Instance.FeverGaugeValue;
    }

    // Update is called once per frame
    void Update()
    {
        NowHP = PlayerController.Instance.hp;
        HpGaugeImg.fillAmount = (float)NowHP / StartHP;


        NowFever = FeverSystem.Instance.FeverGaugeValue;
        FeverGaugeImg.fillAmount = (float)NowFever / FeverSystem.Instance.FeverGaugeMaxValue;
    }
}
