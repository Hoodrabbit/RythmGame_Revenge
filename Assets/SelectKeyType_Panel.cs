using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectKeyType_Panel : MonoBehaviour
{

    [Header("현재 사용 가능 키 배열 버튼 리스트")]
    [SerializeField]private List<Button> buttonList;
    private List<Image> ButtonImage = new List<Image>();
    Color SelectedColor;
    Color UnSelectedColor;



    void Start()
    {

        foreach(var buttonImg in buttonList)
        {
            ButtonImage.Add(buttonImg.GetComponent<Image>());
        }
        SelectedColor = Color.white;
        UnSelectedColor = Color.gray;


        InitalizeKeyTypeButton();
    
        
        
       
    
    }


    public void PressAType()
    {
        GameManager.Instance.keyPressType = KeyPressType.A_Type;
        PressKeyTypeButton();
    }

    public void PressBType()
    {
        GameManager.Instance.keyPressType = KeyPressType.B_Type;
        PressKeyTypeButton();
    }


    private void PressKeyTypeButton()
    {
        InitalizeKeyTypeButton();
    }



    private void InitalizeKeyTypeButton()
    {
        if (GameManager.Instance.keyPressType == KeyPressType.A_Type)
        {


            ButtonImage[0].color = SelectedColor;
            ButtonImage[1].color = UnSelectedColor;
            GameManager.Instance.keyPressType = KeyPressType.A_Type;
        }
        else
        {
            ButtonImage[1].color = SelectedColor;
            ButtonImage[0].color = UnSelectedColor;
            GameManager.Instance.keyPressType = KeyPressType.B_Type;
        }
    }

}
