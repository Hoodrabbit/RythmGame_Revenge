using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public static ButtonController btnController;

    //ButtonContext buttonClick = new ButtonContext();

    BtnController btC = new BtnController();


    private void Awake()
    {
        btnController = this;
    }

    public void Onclick(string name)
    {
        Debug.Log(name);
        btC.SetButton(name);
        //buttonClick.SetButtonInfo()
    }



}
