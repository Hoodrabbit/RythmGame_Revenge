using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodMode : MonoBehaviour
{
    [SerializeField] Sprite UnActivateSprite;
    [SerializeField] Sprite ActivateSprite;

    Image ButtonImage;
    Button button;
    bool GodModeActive = false;


    private void Start()
    {    
        button = GetComponent<Button>();
        ButtonImage =GetComponent<Image>();
        SetSprite();
        button.onClick.AddListener(pressbutton);
    }


    public void pressbutton()
    {
        SetMode();
        SetSprite();
    }


    public void SetSprite()
    {
        if(GameManager.Instance.GodMode == false)
        {
            ButtonImage.sprite = UnActivateSprite;
           // GameManager.Instance.GodMode = false;
        }
        else
        {
            ButtonImage.sprite = ActivateSprite;
           // GameManager.Instance.GodMode = true;
        }
    }

    public void SetMode()
    {
        if (GameManager.Instance.GodMode == false)
        {
           // ButtonImage.sprite = ActivateSprite;
             GameManager.Instance.GodMode = true;
        }
        else
        {
            //ButtonImage.sprite = ActivateSprite;
            GameManager.Instance.GodMode = false;
        }
    }





}
