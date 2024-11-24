using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScrolling : MonoBehaviour
{
    [SerializeField] private List<string> FlowText = new List<string>();
    [SerializeField] private List<TMP_Text> FlowTextTMPList = new List<TMP_Text>();


    public GameObject TextObj;

    public float StartXpos;
    public float EndXpos;

    public float MaxYpos;
    public float MinYpos;

    int UseCount = 0;


    private void Start()
    {
        for (int i= 0; i < FlowText.Count; i++)
        {
            TMP_Text obj = Instantiate(TextObj,transform).GetComponent<TMP_Text>();
            FlowTextTMPList.Add(obj);
            obj.gameObject.SetActive(false);
        }


        StartCoroutine(MakeFlowText());
    }

    IEnumerator MakeFlowText()
    {
        SetFlowText();

        yield return new WaitForSeconds(3f);
    
        StartCoroutine(MakeFlowText());
    }

    private void SetFlowText()
    {
        int randomNum = Random.Range(1, 6);
        int case_ = 0;
        while(case_ <randomNum)
        {
            if (FlowTextTMPList[UseCount].gameObject.activeSelf == false)
            {
                FlowTextTMPList[UseCount].gameObject.SetActive(true);
                FlowTextTMPList[UseCount].text = FlowText[Random.Range(0, FlowText.Count)];
                TextFlowing tF = FlowTextTMPList[UseCount].GetComponent<TextFlowing>();
                float value = Random.Range(MinYpos, MaxYpos);
                tF.Init_TextPos(new Vector2(StartXpos, value));
            }

          

            if(UseCount <FlowText.Count-1)
            {
                UseCount++;
            }
            
            if(UseCount == FlowText.Count-1) 
            {
                UseCount = 0;
            }
            
            case_++;
        }
            
        


    }

    //count 증가 시키기 



}
